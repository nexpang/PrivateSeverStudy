const WebSocket = require('ws');
const port = 32000;
const wsService = new WebSocket.Server({ port }, () => {
    console.log("웹소켓 서버 스타트");
});

//데이터 전송간 명령어와 페이로드 구분해주는 함수
const getPayload = str => {
    let idx = str.indexOf(":");
    let op = str.substr(0, idx);
    let payload = str.substr(idx + 1);
    return { op, payload };
};

//wsService는 내부에 clients라는 속성을 가지고 있고 해당 
//속성을 이용해서 브로드캐스트등을 구현할 수 있다.

wsService.on("connection", socket => {
    socket.send("CHAT:System:Welcome To my Chat server");
    socket.on("message", msg => {

        let data = getPayload(msg);

        if (data.op === "CHAT") {
            wsService.clients.forEach(x => x.send(msg));
        }
    });
});

wsService.on('listening', () => {
    console.log(`server listen on ${port}`);
});




/* 우앱 주석

wsService = new WebSocket.Server(포트);
하면 서버가 열리는 듯 함

on 은 socket.io 와 같은 역할을 하는 듯
send 는 emit 과 같은 역할을 하는 듯

wsService.clients 는 아마도 리스트 비슷한 것이 아닐까
clients 를 위한 forEach 도 있네 머야 이거

socket 은 클라 하나


궁금한것:
wsService.send(); 이 함수가 있는데 io.emit(); 과 비슷한지
getPayload(); 로 메세지를 나누는 듯 함

*/