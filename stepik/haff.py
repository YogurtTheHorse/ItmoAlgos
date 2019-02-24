from bisect import bisect
from collections import Counter

class N:
    def __init__(self, p, v=None, l=None, r=None):
        self.v = v
        self.p = p
        self.l = l
        self.r = r

    def __str__(self):
        return f'N({self.p}, {self.v or ("(" + str(self.l) + ", " + str(self.r) + ")")})'
    

s = input()
p = Counter(s)
nodes = []
letters = []
a = {}

class NL:
    def __getitem__(self, i):
        return nodes[i].p

    def __len__(self):
        return len(nodes)

    def __repr__(self):
        return '[ ' + ', '.join(str(nodes[i]) for i in range(len(self))) + ' ]'

for l, c in p.most_common()[::-1]:
    nodes.append(N(c, l))
    a[l] = ''
    letters.append(l)

k = len(nodes)

while len(nodes) > 1:
    l = nodes.pop(0)
    r = nodes.pop(0)
    n = N(l.p + r.p, l=l, r=r)
    nodes.insert(bisect(NL(), n.p), n)

nodes_stack = [('', nodes[0])]

while len(nodes_stack):
    cs, n = nodes_stack.pop()

    if n.v:
        if cs == '':
            cs = '0'
            
        a[n.v] = cs
    else:
        nodes_stack.append((cs + '0', n.l))
        nodes_stack.append((cs + '1', n.r))
            
s = ''.join(a[c] for c in s)
print(f'{k} {len(s)}')
for l in letters:
    print(f'{l}: {a[l]}')
print(s)
#print(nodes[0])
