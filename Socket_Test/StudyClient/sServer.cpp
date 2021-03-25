#include "stdafx.h"
#include <WS2tcpip.h>
#include <iostream>

#pragma comment(lib, "ws2_32")

using namespace std;

DWORD WINAPI makeThread(void *data);

int _tmain(int argc, _TCHAR* argv[])
{
	WSADATA wasData;
	if (WSAStartup(MAKEWORD(2, 2), &wasData) != 0) 
	{
		cout << "에러 - \'winsock.dll\' 파일을 불러올 수 없습니다" << endl;
		return 1;
	}
	SOCKET sListening = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);
	if (sListening == INVALID_SOCKET) 
	{
		cout << "에러 - Invalid socket" << endl;
		return 1;
	}
	SOCKADDR_IN serverAddr;
	memset(&serverAddr, 0, sizeof(SOCKADDR_IN));
	serverAddr.sin_port = htons(1234);
	serverAddr.sin_family = AF_INET;
	serverAddr.sin_addr.s_addr = INADDR_ANY;

	if (bind(sListening, (struct sockaddr*)&serverAddr, sizeof(SOCKADDR_IN)) == SOCKET_ERROR) 
	{
		cout << "Error - bind에 실패했습니다." << endl;
		closesocket(sListening);
		WSACleanup();
		return 1;
	}

	if(listen(sListening, 5) == SOCKET_ERROR)
	{
		cout << "Error - listen에 실패했습니다." << endl;
		closesocket(sListening);
		WSACleanup();
		return 1;
	}
	
	SOCKADDR_IN clientAddr;
	int addrLen = sizeof(SOCKADDR_IN);
	memset(&clientAddr, 0, addrLen);
	SOCKET clientSocket;

	HANDLE hTread;

	while(1)
	{
		clientSocket = accept(sListening, (struct sockaddr*)&clientAddr, &addrLen);
		hTread = CreateThread(NULL, 0, makeThread, (void*)clientSocket, 0, NULL);
		CloseHandle(hTread);
	}
	closesocket(sListening);

	WSACleanup();

	return 0;
}

DWORD WINAPI makeThread(void* data)
{
	SOCKET socket = (SOCKET)data;

	char messageBuffer[1024];
	int receiveBytes;
	while (receiveBytes = recv(socket, messageBuffer, 1024, 0)) 
	{
		if (receiveBytes > 0) 
		{
			cout << "TRACE - 받은 메세지 : " << messageBuffer << "(" << receiveBytes << " bytes)" << endl;
			int sendBytes = send(socket, messageBuffer, strlen(messageBuffer), 0);
			if(sendBytes>0)
			{
				cout << "TRACE - 보낸 메세지 : " << messageBuffer << "(" << sendBytes << " bytes)" << endl;
			}
		}
		else 
		{
			break;
		}
	}
	closesocket(socket);

	return 0;
}