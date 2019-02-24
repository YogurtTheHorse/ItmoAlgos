# This solution contains a lot of necessary variables\fields because of changed way of solution in the middle of writing

from bisect import bisect

n, m = map(int, input().split())
bb = []
ee = []
tn = {'b': 0, 'c': 1, 'e': 2}
r = [0] * m

class D:
    def __init__(self, v, t, i=None):
        self.v = v
        self.t = t
        self.p = v * 3 + tn[t]
        self.i = i

class DW:
    def __init__(self, c):
        self._c = c
    
    def __getitem__(self, i):
        return self._c[i].p

    def __len__(self):
        return len(self._c)
    
dwb = DW(bb)
dwe = DW(ee)

for i in range(n):
    a, b = map(int, input().split())
    da = D(a, 'b')
    db = D(b, 'e')

    
    bb.insert(bisect(dwb, da.p), da)
    ee.insert(bisect(dwe, db.p), db)

for i, d in enumerate(input().split()):
    dd = D(int(d), 'c', i)
    print(bisect(dwb, dd.p) - bisect(dwe, dd.p), end=' ')

