#include <WS2tcpip.h>
#include <stdio.h>

#pragma comment(lib, "ws2_32")

#define PORT	4578
#define PACKET_SIZE 1024

void main() {
	WSADATA wsaData;
	// MAKEWORD : ,���� ���ڸ� �Ǽ��� ������ִ� ��ũ��
	// WSA
	WSAStartup(MAKEWORD(2, 2), &wsaData);

	WSACleanup();
}