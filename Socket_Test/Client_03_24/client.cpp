#include <WS2tcpip.h>
#include <iostream>
#include <conio.h>

#pragma comment(lib, "ws2_32")

int main() {
	WSADATA wsaData;

	WSAStartup(MAKEWORD(2, 2), &wsaData);

	char chMsg[1024];

	SOCKET sClient;
	SOCKADDR_IN clientAddr;
	{
		clientAddr.sin_family = AF_INET;
		clientAddr.sin_port = htons(5678);
		InetPton(AF_INET, TEXT("172.31.0.184"), &clientAddr.sin_addr);
	}

	sClient = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);

	connect(sClient, (SOCKADDR*)&clientAddr, sizeof(clientAddr));

	while (std::cin.getline(chMsg, 1024))
	{
		send(sClient, chMsg, 1024, 0);

		recv(sClient, chMsg, 1024, 0);
		std::cout << "¼­¹ö: " << chMsg << std::endl;
	}

	_getch();

	closesocket(sClient);
	WSACleanup();


	return 0;
}