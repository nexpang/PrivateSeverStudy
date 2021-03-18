#include <windows.h>
#include <stdio.h>
#include <conio.h>
#include <iostream>
#include "KCriticalSection.h";

int g_value = 0;
KCriticalSection g_csValue;

DWORD WINAPI ThreadProc(LPVOID);

int main(void)
{
    HANDLE hThread;
    DWORD i, dwThreadID;
    //InitializeCriticalSection(&g_csValue);        include KCritical Section

    // Create a thread

    hThread = CreateThread(
        NULL,      
        0,         
        (LPTHREAD_START_ROUTINE)ThreadProc,
        NULL,      
        0,        
        &dwThreadID);

    while (true) {
        for (int i = 0; i < 1000000; i++) {
            KCriticalSectionLock lock(g_csValue);
            // EnterCriticalSection(&g_csValue);    include KCriticalSectionLock
            g_value--;
            // LeaveCriticalSection(&g_csValue);    include KCriticalSectionLock
        }
        break;
    }

    _getch();
    std::cout << g_value << std::endl;

    //DeleteCriticalSection(&g_csValue);            include KCritical Section

    return 0;
}

DWORD WINAPI ThreadProc(LPVOID ipParam) {
    while (true) {
        for (int i = 0; i < 1000000; i++) {
            KCriticalSectionLock lock(g_csValue);
            g_value++;                          //  critical section
        }
        break;
    }
    return 0;
}
