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

//_declspec(thread) int g4 = 4; // ������ ���� : ������ ������ ������ ���� �ϳ��� ����ǵ��� ���̴� ���𺯰���
thread_local int g4 = 4; // thread local variable

void Test() {
	static int s1 = 0;		// static ������ �ѹ� ���� �ȵ� �ٽ� ������� �ʴ´�.
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