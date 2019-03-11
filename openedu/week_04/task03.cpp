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
	string cmd;
	char *queue = new char[1e4];


	cin >> n;
	long ptr;

	for (int i = 0; i < n; i++) {
		long ptr = 0;
		cin >> cmd;

		for (int j = 0; j < cmd.size(); j++) {
			switch (cmd[j])
			{
			case '(': case '[':
				queue[ptr++] = cmd[j];
				break;

			case ')': case ']':
				if (ptr <= 0 || queue[--ptr] != (cmd[j] == ')' ? '(' : '[')) {
					//cout << ptr << " " << queue[ptr] << " " << cmd[j] << " ";
					cout << "NO\n";
					goto end;
				}
				break;
			}
		}

		cout << (ptr ? "NO\n" : "YES\n");

	end:;
	}

	return 0;
}
