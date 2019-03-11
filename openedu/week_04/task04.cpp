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


int main() {
    int n;
    long t;
    char cmd;
    queue<long> q;
    deque<long> d;

    cin >> n;

    for (int i = 0; i < n; i++) {
        cin >> cmd;

        switch (cmd)
        {
            case '+':
                cin >> t;
                q.push(t);
                while (!d.empty() && d.back() > t) {
                    d.pop_back();
                }
                d.push_back(t);
                break;

            case '-':
                if (d.front() == q.front()) { d.pop_front(); }
                q.pop();
                break;

            case '?':
                cout << d.front() << '\n';
                break;
        }
    }

    return 0;
}
