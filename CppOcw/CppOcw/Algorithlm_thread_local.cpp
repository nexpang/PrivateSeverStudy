 // thread exaple
#include <iostream>
#include <thread>
#include <windows.h>

//_declspec(thread) int* g_pData = nullptr;
thread_local int* g_pData = nullptr;

void foo(int i) {
	if (g_pData == nullptr) {
		g_pData = new int[11];
		printf("g_pData allocated %i\r\n", i);
	}
	Sleep(500);
	printf("%i\r\n", i);
	if (g_pData != nullptr) {
		delete[] g_pData;
		printf("g_pData destroyed %i\r\n", i);
	}
}

int main() {
	std::thread first(foo, 1);
	std::thread second(foo, 3);

	std::cout << "main, foo and foo now execute conurrently...\n";

	first.join();
	second.join();

	std::cout << "foo and bar completed.\n";

	return 0;
}