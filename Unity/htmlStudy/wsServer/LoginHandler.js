const SocketState = require('./SocketState.js');
const Vector3 = require('./Vector3');

function LoginHandler(data, socket)
{
    data = JSON.parse(data);
    const {tank, name} = data;

    //console.log(tank, name);
    // 랜덤 위치 등장
    socket.state = SocketState.IN_GAME;

    let sendData = {
        position:Vector3.zero,
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