const express = require("express");
const hbs = require("express-handlebars");

const app = express();

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
})
app.get("/chat1", (req, res) => {
    res.render("chat", {
        msg: "Hello from node.js"
    });
})
app.use((req, res) => {
    res.render("404");
})

app.listen(3000, (err) => {
    if (err)
        return console.log(err);
    console.log("server is listening from port 3000");
})