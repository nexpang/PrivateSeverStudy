const http = require("http");

const server = http.createServer((req, res) => {
    if (res.url === "/") {
        res.write("<h1>Hello from nodejs</h1>");
    } else {
        res.write(`<h1>You have entered ${req.url}</h1>`);
    }

    res.end();
});

server.listen(3000, () => {
    console.log("server is listening from port 3000");
});
// localhost:3000 < 주소