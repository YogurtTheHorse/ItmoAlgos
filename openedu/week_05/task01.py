try:
    inp = open('input.txt', 'r')
    outp = open('output.txt', 'w')

    input = inp.readline
    print = lambda *args: outp.write(' '.join(map(str, args)) + '\n')
except:
    pass

n = input()
arr = [int(x) for x in input().split()]

ok = True
for i, x in enumerate(arr):
    if i > 0 and arr[(i + 1) // 2 - 1] > x:
        ok = False
        break

print('YES' if ok else 'NO')
