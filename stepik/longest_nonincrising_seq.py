n = int(input())
x = [int(i) for i in input().split(' ')][::-1]
p = [0] * n
d = [0] * (n + 1)
pl = 0

for i in range(n):
    lo = 1
    hi = pl
    while lo <= hi:
        mid = (lo + hi) // 2
        if x[d[mid]] <= x[i]:
            lo = mid + 1
        else:
            hi = mid - 1

    new_l = lo
    p[i] = d[new_l - 1]
    d[new_l] = i

    if new_l > pl:
        pl = new_l

re = [0] * pl
k = d[pl]
for i in range(pl - 1, -1, -1):
    re[i] = n - k
    k = p[k]

print(len(re))
print(*re[::-1])
