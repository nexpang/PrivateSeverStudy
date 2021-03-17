#include <windows.h>
#include <stdio.h>
#include <conio.h>
#include <iostream>

int g_value = 0;
CRITICAL_SECTION g_csValue;

DWORD WINAPI ThreadProc(LPVOID);

int main(void)
{
    HANDLE hThread;
    DWORD i, dwThreadID;
    InitializeCriticalSection(&g_csValue);

    // Create a thread

    hThread = CreateThread(
        NULL,         // default security attributes
        0,            // default stack size
        (LPTHREAD_START_ROUTINE)ThreadProc,
        NULL,         // no thread function arguments
        0,            // default creation flags
        &dwThreadID); // receive thread identifier

    while (true) {
        for (int i = 0; i < 1000000; i++) {
            EnterCriticalSection(&g_csValue);
            g_value--;
            LeaveCriticalSection(&g_csValue);
        }
        break;
    }

    _getch();
    std::cout << g_value << std::endl;

    DeleteCriticalSection(&g_csValue);

    return 0;
}

DWORD WINAPI ThreadProc(LPVOID ipParam) {
    while (true) {
        for (int i = 0; i < 1000000; i++) {
            EnterCriticalSection(&g_csValue);
            g_value++;
            LeaveCriticalSection(&g_csValue);
        }
        break;
    }
    return 0;
}
