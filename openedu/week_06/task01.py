import bisect

try:
    inp = open('input.txt', 'r')
    outp = open('output.txt', 'w')

    input = inp.readline
    print = lambda *args: outp.write(' '.join(map(str, args)) + '\n')
except:
    pass

n = input()
arr = [int(x) for x in input().split()]
m = input()

for x in list(map(int, input().split())):
    l = bisect.bisect_left(arr, x)
    if l >= len(arr) or arr[l] != x:
        print('-1 -1')
    else:
        print(f'{l + 1} {bisect.bisect_right(arr, x)}')
