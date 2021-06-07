const WebSocket = require('ws');
const port = 32000;

const LoginHandler = require('./LoginHandler.js');
const SocketState = require('./SocketState.js');

let socketIdx = 0;
let userList = {}; //로그인한 유저들을 관리하는 리스트
let connectedSocket = {}; //연결된 소켓들을 관리

const wsService = new WebSocket.Server({port}, ()=>{
    console.log(`웹 소켓이 ${port}에서 구동중`);
});

const getPayLoad = str => {
    let idx = str.indexOf(":");
    let op = str.substr(0, idx);
    let payload = str.substr(idx+1);
    return {op, payload};
}

wsService.on("connection", socket => {
    console.log("소켓 연결");

    socket.state = SocketState.IN_LOGIN; //로그인 대기 상태
    socket.id = socketIdx; 
    connectedSocket[socketIdx] = socket;
    socketIdx++;
    
    socket.on("close", ()=>{
        console.log("소켓 끊김");
        delete connectedSocket[socket.id];
        delete userList[socket.id];
        //이외에 연결 끊겼을 때 해줄 일을 여기다 적어줘야 한다.
    });
    // MSG:안녕하세요  //OP , Payload
    // MOVE:{x:2, y:3, rotate:30}
    socket.on("message", msg => {
        try {
            const data = JSON.parse(msg); //json파싱

            if(data.type === "LOGIN"){
                let userData = LoginHandler(data.payload, socket); 
                userList[socket.id] = userData; // 로그인한 유저의 데이터를 관리한다.
                return;
            }
        }catch(err){
            console.log(`잘못된 요청 발생 : ${msg}`);
            //console.log(err);
        }        
    });
});
