FROM node:18.12.1-alpine3.15

ENV REDIS_HOST=localhost
ENV REDIS_PORT=6379
ENV CANDIDATE_1_NAME=Cat
ENV CANDIDATE_2_NAME=Dog

EXPOSE 50001

WORKDIR /app

COPY [ "./", "/app"]

RUN npm install

CMD [ "node", "app.js" ]