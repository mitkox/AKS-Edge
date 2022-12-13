const grpc = require('@grpc/grpc-js');
const protoLoader = require('@grpc/proto-loader');
const redis = require('@redis/client');

var PROTO_PATH = __dirname + '/proto/vote.proto';
const Candidate1Name = process.env.CANDIDATE_1_NAME;
const Candidate2Name = process.env.CANDIDATE_2_NAME;

const packageDefinition = protoLoader.loadSync(PROTO_PATH, {
  keepCase: true,
  longs: String,
  enums: String,
  defaults: true,
  oneofs: true
});

const vote = grpc.loadPackageDefinition(packageDefinition).vote;

const grpcServer = new grpc.Server();

const redisClient = redis.createClient({
    socket: {
        host: process.env.REDIS_HOST,
        port: process.env.REDIS_PORT
    }
});

grpcServer.addService(vote.VoteService.service, {
  GetVote: GetVote,
  Vote: Vote,
  Reset: Reset
});

async function GetVote(call, callback) {

  console.log("GetVote API is called");

  var result = await redisClient.mGet([Candidate1Name, Candidate2Name])
  if ((result[0] == null) || (result[1] == null)) {
    if (await redisClient.mSet([Candidate1Name, '0', Candidate2Name, '0']) == 'OK') {
      result = ['0', '0'];
    }
  }

  callback(null, {items: [{name: Candidate1Name, ticket: Number(result[0])}, {name: Candidate2Name, ticket: Number(result[1])}]});
}

async function Vote(call, callback) {
  var result;

  console.log("Vote API is called");

  if ((call.request.name == Candidate1Name) || (call.request.name == Candidate2Name)) {
    await redisClient.incr(call.request.name);
    result = await redisClient.get(call.request.name);
  } else {
    call.request.name = "invalid";
    result = '0';
  }
  
  callback(null, { item: {name: call.request.name, ticket: Number(result)} });
}

async function Reset(call, callback) {
  console.log("Rest API is called");
  var result = await redisClient.mSet([Candidate1Name, '0', Candidate2Name, '0']);
  callback(null, {status: result == 'OK' ? true : false});
}

async function redisConnect() {

  redisClient.on('connect', () => console.log("Connected to Redis server"));
  redisClient.on('error', (err) => console.log("Connection to Redis error:", err.code));

  await redisClient.connect();
}

redisConnect();

grpcServer.bindAsync('0.0.0.0'.concat(':').concat(process.env.VOTESERVER_PORT), grpc.ServerCredentials.createInsecure(), (error, port) => {
  if (!error) {
    console.log("gRPC Server started on port", port);
    grpcServer.start();
  } else {
    console.log("gRPC Server bind failed");
  }
});
