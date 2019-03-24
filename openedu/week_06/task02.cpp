#include <iostream>
#include <fstream> 
#include <iomanip>
#include <string>
#include <queue>
#include <deque>

using namespace std;

#ifdef LOCAL

#define cin std::cin
#define cout std::cout

#else

#endif


int main() {
	ifstream in;
	ofstream out;
	in.open("input.txt");
	out.open("output.txt");

#define cin in
#define cout out

	int n;
	bool ok;
	cin >> n;
	double *h = new double[n];
	cin >> h[0];
	
	double l = 0, r = h[0];

	while (r - l > 0.000000000001) {
		h[1] = (r + l) / 2;
		ok = true;

		for (int i = 2; i < n; ++i) {
			h[i] = 2 * h[i - 1] - h[i - 2] + 2;

			if (h[i] < 0) {
				ok = false;
				break;
			}
		}

		if (ok) {
			r = h[1];
		}
		else {
			l = h[1];
		}
	}

	cout << fixed;
	cout << setprecision(7);
	cout << h[n - 1];


	in.close();
	out.close();

	return 0;
}
