#include <WS2tcpip.h>
#include <stdio.h>

#pragma comment(lib, "ws2_32")

#define PORT	4578
#define PACKET_SIZE 1024

void main() {
	WSADATA wsaData;
	// MAKEWORD : ,뒤의 숫자를 실수로 만들어주는 매크로
	// WSA
	WSAStartup(MAKEWORD(2, 2), &wsaData);

	WSACleanup();
}