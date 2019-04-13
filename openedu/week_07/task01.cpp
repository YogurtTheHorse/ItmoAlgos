#include <iostream>
#include <string>
#include <queue>
#include <deque>

using namespace std;

#ifdef LOCAL

#define cin std::cin
#define cout std::cout

#else

#include "edx-io.hpp"
#define cin io
#define cout io

#endif

#define nl "\n"

struct t_node {
	int left, right, key;
} *tree;

int *keys, *h, *b;
int sz;

int cnt(int i) {
	int d = b[i] = 0;

	if (tree[i].left) {
		d = max(cnt(tree[i].left - 1), d);
		b[i] -= h[tree[i].left - 1];
	}

	if (tree[i].right) {
		d = max(cnt(tree[i].right - 1), d);
		b[i] += h[tree[i].right - 1];
	}


	return h[i] = (d + 1);
}

int find(int x) {
	int i = 0;

	while (tree[i].key != x) {
		if (x < tree[i].key) {
			if (tree[i].left) {
				i = tree[i].left - 1;
			}
			else {
				return -1;
			}
		}
		else {
			if (tree[i].right) {
				i = tree[i].right - 1;
			}
			else {
				return -1;
			}
		}
	}

	return i;
}


int main() {
	int n, k, m, x;
	cin >> n;

	tree = new t_node[sz = n];
	h = new int[n];
	b = new int[n];

	for (int i = 0; i < n; ++i) {
		cin >> tree[i].key >> tree[i].left >> tree[i].right;

		h[i] = 0;
	}

	cnt(0);

	for (int i = 0; i < n; ++i) {
		cout << b[i] << nl;
	}


	return 0;
}
