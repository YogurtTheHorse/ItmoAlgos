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
	long *queue = new long[1 + 1e6];
	long fptr = 0, lptr = 0;

	cin >> n;

	for (int i = 0; i < n; i++) {
		cin >> cmd;
		if (cmd == '+') {
			cin >> queue[fptr++];
		}
		else {
			cout << queue[lptr++] << '\n';
		}
	}

	return 0;
}
