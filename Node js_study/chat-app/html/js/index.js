'use strict';
let socket = io();
socket.on('connect', () => {
    let name = prompt("대화명을 입력해주세요. ', '");
    socket.emit("newUserConnet", name);
    console.log('connect');
});
let chatWindow = document.getElementById("chatWindow");
socket.on("updateMsg", (data) => {
    if (data.name === "SERVER") {
        let info = document.getElementById("info");
        info.innerHTML = data.message;

        setTimeout(() => {
            info.innerText = "";
        }, 1000);
    }
    else {
        let chatMsgEl = drawChatMessage(data);
        chatWindow.appendChild(chatMsgEl);
    }
});
function drawChatMessage(data) {
    let wrap = document.createElement('p');
    let message = document.createElement('span');
    let name = document.createElement('span');

    name.innerText = data.name;
    message.innerText = data.message;

    name.classList.add('output__user__name');
    message.classList.add('output__user__message');

    wrap.classList.add('output__user');
    wrap.dataset.id = socket.id;

    wrap.appendChild(name);
    wrap.appendChild(message);

    return wrap;
}


let sendBtn = document.getElementById("chatMsgSendBtn");
let chatInput = document.getElementById("chatInput");

sendBtn.addEventListener("click", () => {
    iEnter();
})
function iEnter() {
    let message = chatInput.value;

    if (!message)
        return false;
    socket.emit("sendMsg", {
        message
    })

    chatInput.value = "";
}