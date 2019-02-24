from bisect import bisect_left

l = list(map(int, input().split()[1:]))
ss = list(map(int, input().split()[1:]))

for s in ss:
    i = bisect_left(l, s)
    print((i + 1) if i < len(l) and l[i] == s else -1, end=' ')
    

