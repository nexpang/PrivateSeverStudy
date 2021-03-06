const SocketState = require('./SocketState.js');
const Vector3 = require('./Vector3');

let respawnPoint = [
    new Vector3(0,0,0),
    new Vector3(15,15,0),
    new Vector3(-15,-18,0),
    new Vector3(15,-18,0)
]

function LoginHandler(data, socket)
{
    data = JSON.parse(data);
    const {tank, name} = data;

    //console.log(tank, name);
    // 랜덤 위치 등장
    let pos = respawnPoint[Math.floor(Math.random()*respawnPoint.length)];
    socket.state = SocketState.IN_GAME;

    let sendData = {
        position:pos,
        rotation:Vector3.zero,
        turretRotation:Vector3.zero,
        socketId:socket.id,
        name,
        tank
    }

    const payload = JSON.stringify(sendData);
    const type = "LOGIN";

    socket.send(JSON.stringify({type, payload}));

    return sendData;
}

module.exports = LoginHandler;