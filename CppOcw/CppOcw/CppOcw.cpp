#include <iostream>
#include <stdio.h>
#include <list>
#include <iterator>
#include <limits>

extern int g1;
//extern int g2;
int g3 = 3;

//int s1 = 0;
//bool s1bool = false;

//_declspec(thread) int g4 = 4; // 쓰레드 로컬 : 변수가 각각의 쓰레드 마다 하나씩 선언되도록 붙이는 선언변경자
thread_local int g4 = 4; // thread local variable

void Test() {
	static int s1 = 0;		// static 변수는 한번 선언 된뒤 다시 선언되지 않는다.
	/*if (s1bool == false) {
		s1 = 0;
		s1bool = true;
	}*/
	printf("%i\r\n", s1);
	s1 += 1;
}

void main() {
	printf("%i,%i\r\n", g1, g3);
	Test();
	Test();
	Test();
}