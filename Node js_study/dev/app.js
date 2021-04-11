const express = require("express");
const hbs = require("express-handlebars");

const server = express();

server.engine("hbs", hbs({
    extname: "hbs",
    defaultLayout: "layout.hbs",
    partialsDir: "partials",
})
);

server.set("view engine", "hbs");
server.use(express.static(__dirname + "/public"));

server.get("/", (req, res) => {
    res.render("home", {
        msg: "Hello from node.js"
    });
})

server.listen(3000, (err) => {
    if (err)
        return console.log(err);
    console.log("server is listening from port 3000");
})