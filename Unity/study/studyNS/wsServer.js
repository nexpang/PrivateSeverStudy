const WebSocket = require('ws');
const port = 32000;


let socketIdx = 0;
let connectedSocket = {};
let userList = {};

const wsService = new WebSocket.Server({ port }, () => {
    console.log("웹소켓 서버 스타트");
});

const getPayload = str => {
    let idx = str.indexOf(":");
    let op = str.substr(0, idx);
    let payload = str.substr(idx + 1);
    return { op, payload };
};


wsService.on("connection", socket => {
    socket.send(JSON.stringify({ payload: "Welcome", type: "Chat" }));
    socket.id = socketIdx;
    connectedSocket[socketIdx] = socket;
    socketIdx++;

    socket.on("close", () => {
        console.log(`${socket.id} 접속 해제`);
        delete connectedSocket[socket.id];

        let keys = Object.keys(userList);
        userList[keys[socket.id]]
    })

    socket.on("message", msg => {
        const data = JSON.parse(msg);
        if (data.type === "CHAT") {
            wsService.clients.forEach(x => x.send(msg));
        }
    });
});

wsService.on('listening', () => {
    console.log(`server listen on ${port}`);
});