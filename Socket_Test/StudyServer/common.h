#pragma once

#include <WinSock2.h>
#include <Windows.h>
#include <process.h>
#include <string.h>
#include <stdlib.h>
#include <malloc.h>

//#include <iostream>
#include <process.h>

#pragma comment(lib,"ws2_32")
IN_ADDR GetDefaultMyIP();

//unsigned int _beginthread(void (*_ThreadEntryPoint) (void*), unsigned _stacksize, void* param);