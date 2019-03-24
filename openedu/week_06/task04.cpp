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

struct t_node {
	int left, right, key;
} *tree;

int *keys, *parents;
int sz;

int cnt(int i) {
	int d = 1;

	if (tree[i].left) {
		d += cnt(tree[i].left - 1);
	}

	if (tree[i].right) {
		d += cnt(tree[i].right - 1);
	}

	return d;
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

#define KEY(a) ((a) + 100000000)


int main() {
	int n, k, m, x;
	cin >> n;

	tree = new t_node[sz = n];
	parents = new int[n];

	for (int i = 0; i < n; ++i) {
		cin >> tree[i].key >> tree[i].left >> tree[i].right;

		if (tree[i].left) {
			parents[tree[i].left - 1] = i;
		}

		if (tree[i].right) {
			parents[tree[i].right - 1] = i;
		}
	}

	cin >> m;
	for (int i = 0; i < m; ++i) {
		cin >> x;
		x = find(x);

		if (x >= 0) {
			if (x) {
				if (tree[parents[x]].left - 1 == x) {
					tree[parents[x]].left = 0;
				}
				else {
					tree[parents[x]].right = 0;
				}
			}

			sz -= cnt(x);
		}

		cout << sz << "\n";
	}


	return 0;
}
