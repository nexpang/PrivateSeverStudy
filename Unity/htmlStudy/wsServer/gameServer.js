const WebSocket = require('ws');
const port = 36589;

const LoginHandler = require('./LoginHandler.js');
const SocketState = require('./SocketState.js');
const Vector3 = require('./Vector3.js');
const tankData = require('./GameData.js');

let socketIdx = 0;
let userList = {}; //로그인한 유저들을 관리하는 리스트
let connectedSocket = {};

const wsService = new WebSocket.Server({port}, ()=>{
    console.log(`웹 소켓이 ${port}에서 구동중`);
});

const getPayLoad = str => {
    let idx = str.indexOf(":");
    let op = str.substr(0,idx);
    let payload = str.substr(idx+1);
    return {op, payload};
}

wsService.on("connection", socket=>{
    console.log('소켓 연결');

    socket.state = SocketState.IN_LOGIN;
    socket.id = socketIdx;
    connectedSocket[socketIdx] = socket;
    socketIdx++;

    socket.send(JSON.stringify({type:"INITDATA", payload:JSON.stringify(tankData)}))

    socket.on("close", ()=>{
        console.log('소켓 끊김');
        delete connectedSocket[socket.id];
        delete userList[socket.id];
        wsService.clients.forEach(socket=>{
            if(socket.state != SocketState.IN_GAME || socket.id === socket.id)
                return;
            socket.send(JSON.stringify({type:"DISCONNECT", payload:socket.id}))
        })
    });

    socket.on("message", msg => {
        try{
            const data = JSON.parse(msg);
    
            if(data.type === "LOGIN"){
                let userData = LoginHandler(data.payload, socket);
                userData.kill =0;
                userData.death =0;
                userList[socket.id] = userData;
                return;
            }
            if(data.type === "TRANSFORM"){
                let transformVo = JSON.parse(data.payload);
                if(userList[transformVo.socketId] !== undefined){
                    userList[transformVo.socketId].position = transformVo.position;
                    userList[transformVo.socketId].rotation = transformVo.rotation;
                    userList[transformVo.socketId].turretRotation = transformVo.turretRotation;
                }
                return;
            }
            if(data.type === "FIRE" || data.type === "HIT"||data.type==="RESPAWN"){
                //let fireInfo = JSON.parse(data.payload);
                broadcast(msg, socket);
                return;
            }
            if(data.type==="DEAD"){
                let deadVO = JSON.parse(data.payload);
                userList[deadVO.socketId].death++;
                userList[deadVO.killerId].kill++;
                broadcast(msg, socket);
                return;
            }
        }catch(err){
            console.log('잘못된 요청 발생 : '+msg);
            console.log(err);
        }
    });
});

function broadcast(msg, socket)
{
    wsService.clients.forEach(soc=>{
        if(soc.state != SocketState.IN_GAME || soc.id == socket.id) 
            return;
        soc.send(msg);
    })
}

setInterval(()=>{
    let keys = Object.keys(userList);
    let dataList = []; // 전송할 배열
    for(let i=0; i<keys.length; i++){
        dataList.push(userList[keys[i]]);
    }
    wsService.clients.forEach(socket=>{
        if(socket.state != SocketState.IN_GAME)
            return;
        socket.send(JSON.stringify({type:"REFRESH", payload:JSON.stringify({dataList})}))
    })
}, 100);