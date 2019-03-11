#include <iostream>
#include <string>

using namespace std;

#ifdef LOCAL

#define cin std::cin
#define cout std::cout

#else

#include "edx-io.hpp"
#define cin io
#define cout io

#endif


int main() {
	int n;
	long t;
	char cmd;
	long *stack = new long[1e6];
	long ptr = 0;

	cin >> n;

	for (int i = 0; i < n; i++) {
		cin >> cmd;
		if (cmd == '+') {
			cin >> stack[ptr++];
		}
		else {
			cout << stack[--ptr] << '\n';
		}
	}

	return 0;
}
