// https://mawile.tistory.com/24

#include <iostream>
#include <winsock2.h>
#include <thread>

using namespace std;

#define PACKET_SIZE 1024

SOCKET skt;

void proc_recv() {
	char buffer[PACKET_SIZE] = {};
	string cmd;
	while (!WSAGetLastError()) {
		ZeroMemory(&buffer, PACKET_SIZE);
		recv(skt, buffer, PACKET_SIZE, 0);
		cmd = buffer;
		if (cmd == "hi")
			break;
		cout << "받은 메세지: " << buffer < endl;
	}
}

int main() {
	WSADATA wsa;
	WSAStartup(MAKEWORD(2, 2), &wsa);

	skt = socket(PF_INET, SOCK_STREAM, IPPROTO_TCP);

	SOCKADDR_IN addr = {};
	addr.sin_family = AF_INET;
	addr.sin_port = htons(4444);
	addr.sin_addr.s_addr = inet_addr("127.0.0.1");

	while (1) {
		if (!connect(skt, (SOCKADDR*)&addr, sizeof(addr))) break;
	}
	thread proc1(proc_recv);

	char buffer[PACKET_SIZE] = { 0 };

	while (!WSAGetLastError()) { //메세지전송
		cin >> buffer;
		send(skt, msg, strlen(buffer), 0);
	}
	proc1.join();

	closesocket(skt);
	WSACleanup();
}