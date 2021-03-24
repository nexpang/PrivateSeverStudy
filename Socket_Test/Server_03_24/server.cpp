#include <WS2tcpip.h>
#include <iostream>
#include <conio.h>

#pragma comment(lib, "ws2_32")

int main() 
{
	WSADATA wasData;

	SOCKET sListening;
	SOCKADDR_IN serverAddr;
	{
		serverAddr.sin_port = htons(5678);
		serverAddr.sin_family = AF_INET;
		//InetPton(AF_INET, TEXT("127.0.0.1"), &serverAddr.sin_addr);
		serverAddr.sin_addr.s_addr = INADDR_ANY;
	}

	SOCKET sClient;
	SOCKADDR_IN clientAddr;
	int clientAddrSize = sizeof(clientAddr);
	char chMsg[1024] = "더블유님 대단해요";

	WSAStartup(MAKEWORD(2, 2), &wasData);

	sListening = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);
	bind(sListening, (SOCKADDR*)&serverAddr, sizeof(serverAddr));

	listen(sListening, SOMAXCONN);
	
	sClient = accept(sListening, (SOCKADDR*)&clientAddr, &clientAddrSize);
	closesocket(sListening);

	while (recv(sClient, chMsg, 1024, 0) != -1)
	{
		std::cout << "클라이언트: " << chMsg << std::endl;
		std::cin.getline(chMsg, 1024);

		send(sClient, chMsg, 1024, 0);
	}

	_getch();

	closesocket(sClient);
	WSACleanup();

	return 0;
}