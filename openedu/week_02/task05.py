try:
    inp = open('input.txt', 'r')
    outp = open('output.txt', 'w')

    input = inp.readline
    print = lambda *args: outp.write(' '.join(map(str, args)) + '\n')
except:
    pass

def arr(t=int):
    return list(map(t, input().split()))

n, k = arr()
array = arr()
    
arrays = [
    sorted(array[i] for i in range(j, n, k))
    for j in range(k)
]

def get_p(i):
    return i % k, i // k

res = True
for i in range(1, n):
    if arrays[i % k][i // k] < arrays[(i - 1) % k][(i - 1) // k]:
        res = False
        break

print('YES' if res else 'NO')
