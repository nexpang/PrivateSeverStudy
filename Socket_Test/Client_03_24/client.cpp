#include <WS2tcpip.h>
#include <iostream>
#include <conio.h>
#pragma comment(lib, "ws2_32")

DWORD WINAPI ThreadProc(LPVOID proc);

char chSendMsg[1024];
char chRecvMsg[1024];
bool isDisconnected = false;

SOCKET sClient;

int main()
{

	WSADATA wsaData;

	SOCKADDR_IN clientAddr;
	{
		clientAddr.sin_family = AF_INET;
		clientAddr.sin_port = htons(56789);
		InetPton(AF_INET, TEXT("172.31.0.224"), &clientAddr.sin_addr);
	}
	HANDLE hThread;

	//CHAR chNickname[32];

	//while (true)
	//{
	//	std::cout << "닉네임을 입력하세요 (최대 32글자): ";
	//	std::cin >> chNickname;
	//	if (chNickname[31] != '\0')
	//	{
	//		std::cout << "잘못된 닉네임입니다." << std::endl;
	//	}
	//	else
	//		break;
	//}
	//system("cls");

	WSAStartup(MAKEWORD(2, 2), &wsaData);
	sClient = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);

	std::cout << "연결 기다리는 중." << std::endl;
	if (connect(sClient, (SOCKADDR*)&clientAddr, sizeof(clientAddr)) != -1) {
		std::cout << "연결 성공." << std::endl;

	}
	else {
		std::cout << "연결 실패" << std::endl;
		return 0;
	}


	hThread = CreateThread(NULL, 0, ThreadProc, NULL, 0, NULL);

	while (!isDisconnected)
	{
		std::cin.getline(chSendMsg, 1024);
		send(sClient, chSendMsg, 1024, 0);
	}




	_getch();

	closesocket(sClient);
	WSACleanup();
	CloseHandle(hThread);
	return(0);
}


DWORD WINAPI ThreadProc(LPVOID proc)
{
	while (recv(sClient, chRecvMsg, 1024, 0) != -1)
	{
		std::cout << "누군가: " << chRecvMsg << std::endl;
	}
	isDisconnected = true;
	return 0;
}