n = int(input())
arr = [int(x) for x in input().split()]
d = [0] * n

for i in range(n-1, -1, -1):
    d[i] = max([0] + [1 + d[j] for j in range(i + 1, n) if arr[j] % arr[i] == 0])

print(max(d) + 1)
