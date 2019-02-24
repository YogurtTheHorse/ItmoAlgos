n = int(input())
dots = []
tt = ['b', 'e']
ss = [False] * n
result = []

class Dot:
    def __init__(self, v, t, s, segments=[]):
        self.v = v
        self.t = t
        self.s = s

for i in range(n):
    for j, v in enumerate(map(int, input().split())):
        dots.append(Dot(v, tt[j], i))

dots = sorted(dots, key=lambda d: d.v * 1000 + (0 if d.t == 'b' else 1))
cs = []

for i in range(n * 2):  
    if dots[i].t == 'b':
        cs.append(dots[i].s)
    else:
        if ss[dots[i].s]:
            continue
        
        result.append(dots[i].v)
        for s in cs:
            ss[s] = True
        cs = [ ]

print(len(result))
print(' '.join(map(str, result)))
