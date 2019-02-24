class Item:
    def __init__(self, c, w):
        self.c = c
        self.w = w
        self.p = self.c / self.w

    def __str__(self):
        return f'({self.p, self.c, self.w})'

n, w = map(int, input().split())
items = sorted([Item(*[int(x) for x in input().split()]) for i in range(n) ], key=lambda i: i.p)

s = 0
while w > 0 and len(items) > 0:
    i = items.pop()
    cw = min(w, i.w)
    s += cw * i.p
    w -= cw

print(s)
