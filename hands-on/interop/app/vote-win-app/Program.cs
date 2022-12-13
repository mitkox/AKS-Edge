using McMaster.Extensions.CommandLineUtils;
using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using VoteApiNamespace;

var app = new CommandLineApplication();

app.Name = "Vote Console App";
app.Description = "Vote Console app to demostrate gRPC communication between Windows App and workload within AKS Edge.";
app.HelpOption();

var Server = app.Option("-s|--server <H>", "", CommandOptionType.SingleValue);
Server.DefaultValue = "localhost";
var Port = app.Option("-p|--port <P>", "", CommandOptionType.SingleValue);
Port.DefaultValue = "50000";

app.OnExecute(() =>
{
    string connectionString = "http://" + Server.Value() + ":" + Port.Value();
    using var channel = GrpcChannel.ForAddress(connectionString);
    var client = new VoteService.VoteServiceClient(channel);
    var ret = client.GetVote(new Empty());

    string candidate_1_name = ret.Items[0].Name;
    int candidate_1_ticket = ret.Items[0].Ticket;
    string candidate_2_name = ret.Items[1].Name;
    int candidate_2_ticket = ret.Items[1].Ticket;

    while (true)
    {
        Console.WriteLine($"Tickets: {candidate_1_name}: {candidate_1_ticket} | {candidate_2_name}: {candidate_2_ticket}");
        Console.WriteLine("\r\n");

        Console.WriteLine("---- Vote Your Favor ----");
        Console.WriteLine($"(1) {candidate_1_name}");
        Console.WriteLine($"(2) {candidate_2_name}");
        Console.WriteLine("(3) Reset");
        Console.WriteLine("(4) Exit");
        Console.WriteLine("-------------------------");
        Console.WriteLine("Please enter:");
        string option = Console.ReadLine() ?? "";

        switch (option)
        {
            case "1":
                candidate_1_ticket = Vote(client, candidate_1_name);
                break;
            case "2":
                candidate_2_ticket = Vote(client, candidate_2_name);
                break;
            case "3":
                if (Reset(client))
                {
                    candidate_1_ticket = 0;
                    candidate_2_ticket = 0;
                }
                break;
            case "4":
                return;
            default:
                continue;
        }

        Console.WriteLine("\r\n");
    }
});

return app.Execute(args);

/// <summary>
/// Retrieves the value of the connection string from the connectionStringOption. 
/// If the connection string wasn't passed method prompts for the connection string.
/// </summary>
/// <returns></returns>
int Vote(VoteService.VoteServiceClient client, string name)
{
    VoteReply ret = client.Vote(new VoteRequest { Name = name });
    return ret.Item.Ticket;
}

/// <summary>
/// Retrieves the value of the connection string from the connectionStringOption. 
/// If the connection string wasn't passed method prompts for the connection string.
/// </summary>
/// <returns></returns>
bool Reset(VoteService.VoteServiceClient client)
{
    ResetReply ret = client.Reset(new Empty());
    return ret.Status;
}