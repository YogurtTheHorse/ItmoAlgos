from bisect import bisect

inp = open('input.txt', 'r')
outp = open('output.txt', 'w')

ml = lambda x, y: list(map(x, y))
pr = lambda s: outp.write(s + '\n')

inp.readline() # read n
arr = ml(float, inp.readline().split())
arr = list(((i + 1), v) for i, v in enumerate(arr))

arr = sorted(arr, key=lambda a: a[1])
print(arr)

pr(f'{arr[0][0]} {arr[len(arr) // 2][0]} {arr[-1][0]}')

outp.close()
