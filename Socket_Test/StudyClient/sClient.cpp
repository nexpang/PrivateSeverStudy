#include "common.h"

#define PORT_NUM      10200
#define MAX_MSG_LEN 256
#define SERVER_IP        "192.168.34.50"

int main()
{
    WSADATA wsadata;
    WSAStartup(MAKEWORD(2, 2), &wsadata);//扩加 檬扁拳           


    SOCKET sock;
    sock = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);//家南 积己
    if (sock == -1) { return -1; }


    SOCKADDR_IN servaddr = { 0 };//家南 林家
    servaddr.sin_family = AF_INET;
    servaddr.sin_addr.s_addr = inet_addr(SERVER_IP);
    servaddr.sin_port = htons(PORT_NUM);

    int re = 0;
    re = connect(sock, (struct sockaddr*)&servaddr, sizeof(servaddr));//楷搬 夸没
    if (re == -1) { return -1; }


    char msg[MAX_MSG_LEN] = "";
    while (true)
    {
        gets_s(msg, MAX_MSG_LEN);
        send(sock, msg, sizeof(msg), 0);//价脚
        if (strcmp(msg, "exit") == 0) { break; }
        recv(sock, msg, sizeof(msg), 0);//价脚
        printf("荐脚:%s\n", msg);
    }
    closesocket(sock);//家南 摧扁
    WSACleanup();//扩加 秦力拳

    return 0;
}



免贸: https://ehclub.co.kr/1375 [攫力唱 绒老]