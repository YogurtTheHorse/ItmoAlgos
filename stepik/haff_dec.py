from bisect import bisect
from collections import Counter

class N:
    def __init__(self, v=None, l=None, r=None):
        self.v = v
        self.l = l
        self.r = r

    def __getitem__(self, i):
        r = None
        if i == '0':
            r = self.l
        elif i == '1':
            r = self.r

        r = r or N()
        self[i] = r
        return r

    def __setitem__(self, i, item):
        if i == '0':
            self.l = item
        elif i == '1':
            self.r = item

    def __str__(self):
        return f'N("{self.v}", 0: {self.l}, 1: {self.r})'
    
root = N()
k, l = map(int, input().split())

for i in range(k):
    l, c = input().split()
    l = l[0]
    c = list(c)
    n = root

    while len(c):
        n = n[c.pop(0)]

    n.v = l

n = root
s = input()
r = ''

for c in s:
    n = n[c]
    if n.v:
        r += n.v
        n = root


print(r)

