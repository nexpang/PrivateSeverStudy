const express = require("express");

const server = express();

// GET      www.facebook.com/
// POST     ID: nexpang, Pass: 1234 값 전송 데이터 확인
// DELETE   데이터 삭제
// PUT      업데이트?

server.use(express.static(__dirname + "/public"));

// server.use((req, res, next) => {
//     req.user = {
//         id: "1234",
//     }
//     next();
// })
server.get("/", (req, res) => {
    res.sendFile(__dirname + "/html/index.html")
})
server.get("/about", (req, res) => {
    res.sendFile(__dirname + "/html/about.html")
})
server.use((req, res) => {
    res.sendFile(__dirname + "/html/404.html")
})

server.listen(3000, (err) => {
    if (err)
        return console.log(err);
    console.log("server is listening from port 3000");
})