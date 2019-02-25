from bisect import bisect_left
from math import *

try:
    inp = open('input.txt', 'r')
    outp = open('output.txt', 'w')

    input = inp.readline
    print = lambda *args: outp.write(' '.join(map(str, args)) + '\n')
except:
    pass

n = int(input())
d = [i for i in range(n)]
res = [0] * n

for i in range(n -1, -1, -1):
    res[d[i // 2]] = i + 1
    d[i // 2], d[i] = d[i], d[i // 2]

if n > 1:
    i1 = res.index(1)
    i2 = res.index(2)

    res[i1], res[i2] = res[i2], res[i1]
    
print(*res)
