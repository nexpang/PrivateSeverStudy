const express = require("express");

const server = express();

server.listen(3000, (err) => {
    if (err)
        return console.log(err);
    console.log("server is listening from port 3000");
})