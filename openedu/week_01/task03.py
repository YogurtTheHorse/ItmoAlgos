from bisect import bisect

inp = open('input.txt', 'r')
outp = open('output.txt', 'w')

ml = lambda x, y: list(map(x, y))
pr = lambda s: outp.write(s + '\n')

inp.readline()
arr = ml(int, inp.readline().split())
steps = [0 for i in arr]
res = []

while len(res) < len(arr):
    steps[len(res)] = bisect(res, arr[len(res)]) + 1
    res.insert(steps[len(res)] - 1, arr[len(res)])

pr(' '.join(ml(str, steps)))
pr(' '.join(ml(str, res)))
outp.close()
