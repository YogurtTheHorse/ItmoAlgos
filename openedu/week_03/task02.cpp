#include <iostream>
#include <string>


#ifdef LOCAL

#define cin std::cin
#define cout std::cout

#else

#include "edx-io.hpp"
#define cin io
#define cout io

#endif


int main() {
	int n, m, k;
	cin >> n >> m >> k;

	auto *strings = new std::string[m];
	auto 
		*tmp1 = new long[n], 
		*tmp2 = new long[n];

	for (int i = 0; i < n; i++) {
		tmp1[i] = i;
	}

	for (int i = 0; i < m; i++) {
		cin >> strings[i];
	}

#define DV 200
	auto *counter = new long[DV];
	long *ptr[2] = { tmp1, tmp2 };
	int ptr_ind = 0;

	for (int pow = m - 1; pow >= m - k; --pow) {
#define CURR_CHAR (strings[pow][ptr[ptr_ind][i]])
		memset(counter, 0, sizeof(long) * DV);

		for (int i = 0; i < n; i++) {
			counter[CURR_CHAR]++;
		}

		for (int i = 1; i < DV; i++) {
			counter[i] += counter[i - 1];
		}

		for (int i = n - 1; i >= 0; i--) {
			ptr[1 - ptr_ind][--counter[CURR_CHAR]] = ptr[ptr_ind][i];
		}

		ptr_ind = 1 & ++ptr_ind;
	}

	for (int i = 0; i < n; i++) {
		cout << (ptr[ptr_ind][i] + 1);
		cout << ' ';
	}

	return 0;
}
