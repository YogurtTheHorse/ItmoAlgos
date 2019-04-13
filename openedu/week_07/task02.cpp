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

void big_left_rotation(int root_index) {
	t_node
		a = tree[root_index],
		b = tree[a.right - 1],
		c = tree[b.left - 1];

	int x_ind = c.left - 1,
		y_ind = c.right - 1,
		old_c_ind = b.left - 1,
		b_ind = a.right - 1;

	c.left = old_c_ind + 1;
	c.right = b_ind + 1;

	b.left = y_ind + 1;
	a.right = x_ind + 1;

	tree[root_index] = c;
	tree[old_c_ind] = a;
	tree[b_ind] = b;
}

void left_rotation(int root_index) {
	if (b[tree[root_index].right - 1] < 0) {
		big_left_rotation(root_index);
		return;
	}

	t_node
		a = tree[root_index],
		b = tree[a.right - 1];

	int y_ind = b.left - 1,
		old_b_index = a.right - 1;

	b.left = old_b_index + 1;
	a.right = y_ind + 1;

	tree[root_index] = b;
	tree[old_b_index] = a;
}

int *indexes;
int curr_index = 1;

void calc_index(int i) {
	if (!i) { return; }

	t_node n = tree[i - 1];
	indexes[i] = curr_index++;

	calc_index(n.left);
	calc_index(n.right);
}

void print_node(int i) {
	if (!i) { return; }

	t_node n = tree[i - 1];
	cout << n.key << " " << indexes[n.left] << " " << indexes[n.right] << nl;

	print_node(n.left);
	print_node(n.right);
}


int main() {
	int n, k, m, x;
	cin >> n;

	tree = new t_node[sz = n];
	h = new int[n];
	b = new int[n];
	indexes = new int[n + 1];
	indexes[0] = 0;

	for (int i = 0; i < n; ++i) {
		cin >> tree[i].key >> tree[i].left >> tree[i].right;

		h[i] = 0;
	}

	cnt(0);
	left_rotation(0);

	calc_index(1);
	cout << n << nl;
	print_node(1);


	return 0;
}
