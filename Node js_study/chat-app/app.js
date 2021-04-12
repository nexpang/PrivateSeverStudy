const express = require("express");
const http = require("http");
const app = express();
const server = http.createServer(app);
const fs = require("fs");
const io = require("socket.io")(server);

app.use(express.static("html"));

app.get('/', (req, res) => {
    fs.readFile(__dirname + "/html/index.html", (err, data) => {
        if (err)
            throw err;
        res.writeHead(200, {
            "Content-Type": "text/html"
        }).write(data).end();
    })
})
io.sockets.on("connection", (socket) => {
    socket.on("newUserConnet", (name) => {
        socket.name = name;

        let msg = name + "님이 접속했습니다.";

        io.sockets.emit("updateMsg", {
            name: "SERVER",
            message: msg,
        })
    });
    socket.on("disconnect", () => {
        let msg = socket.name + "님이 퇴장하셨습니다.";
        socket.broadcast.emit("updateMsg", {
            name: "SERVER",
            message: msg,
        })
    });
    socket.on("sendMsg", (data) => {
        data.name = socket.name;
        io.sockets.emit('updateMsg', data);
    })
})

server.listen(1234, () => {
    console.log("서버 실행중...");
})