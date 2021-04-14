const express = require("express");
const http = require("http");
const hbs = require("express-handlebars");
const app = express();
const server = http.createServer(app);
//const fs = require("fs");
const io = require("socket.io")(server);

app.engine("hbs", hbs({
    extname: "hbs",
    defaultLayout: "layout.hbs",
    partialsDir: "partials",
}));

app.set("view engine", "hbs");
app.use(express.static(__dirname + "/public"));

app.get("/", (req, res) => {
    res.render("home", {
        msg: "Hello from node.js"
    });
});
app.get("/chat1", (req, res) => {
    res.render("chat", {
        msg: "Hello from node.js"
    });
});
app.all((req, res) => {
    res.render("404");
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

server.listen(61848, (err) => {
    if (err)
        return console.log(err);
    console.log("server is listening from port 1234");
})