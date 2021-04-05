const http = require('http');
const express = require('express');
const app = express();
const server = http.createServer(app);
const io = require('socket.io')(server);


app.get("/chat", (req, res) => {
    res.sendFile(__dirname + "/index.html");
});


io.on("connection", socket => {
    console.log(socket.id);
    socket.on("chat", data => {
        io.emit("chat-all", {sender:socket.id, msg:data.msg});
    });
});

server.listen(8080, () => {
    console.log("서버 실행중");
});
