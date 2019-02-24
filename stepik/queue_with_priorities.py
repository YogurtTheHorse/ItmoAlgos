# BTW: i didn't use orders with priorities to solve this problem
from bisect import insort_left

n = int(input())
l = list()

for i in range(n):
    command, v, *_ = input().split() + ['0']
    {'Insert': lambda: insort_left(l, int(v)), 'ExtractMax': lambda: print(l.pop())}[command]()
