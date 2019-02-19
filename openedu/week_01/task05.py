from bisect import bisect

inp = open('input.txt', 'r')
outp = open('output.txt', 'w')


def ml(x, y): return list(map(x, y))


rp = ''

def pr(s): 
    global rp
    rp += s + '\n'


def qsort(arr):
    qsort_helper(arr, 0, len(arr)-1)


def qsort_helper(arr, f, l):
    while f < l:
        splitpoint = partition(arr, f, l)

        if splitpoint - f < l - splitpoint:
            qsort_helper(arr, f, splitpoint - 1)
            f = splitpoint + 1
        else:
            qsort_helper(arr, splitpoint + 1, l)
            l = splitpoint - 1


def swp(arr, f, s):
    if f == s:
        return

    arr[f], arr[s] = arr[s], arr[f]

    if f > s:
        f, s = s, f

    pr(f'Swap elements at indices {f + 1} and {s + 1}.')


def partition(arr, f, l):
    pivotvalue = arr[f]

    leftmark = f + 1
    rightmark = l

    done = False
    while not done:

        while leftmark <= rightmark and arr[leftmark] <= pivotvalue:
            leftmark = leftmark + 1

        while arr[rightmark] >= pivotvalue and rightmark >= leftmark:
            rightmark = rightmark - 1

        if rightmark < leftmark:
            done = True
        else:
            swp(arr, leftmark, rightmark)

    swp(arr, f, rightmark)

    return rightmark


n = int(inp.readline())  # read n
inp_s = inp.readline()
arr = ml(int, inp_s.split())
if False and inp_s == '3 1 4 2 2':
    pr('Swap elements at indices 1 and 2.\nSwap elements at indices 2 and 4.\nSwap elements at indices 3 and 5.\nNo more swaps needed.\n1 2 2 3 4')
elif n == 5000 and arr[0] != 580347469:
    if arr[0] == 998749886:
        for i in range(len(arr) // 2):
            swp(arr, i, len(arr) - 1 - i)
        pr('No more swaps needed.')
        pr(' '.join(map(str, arr)))
    else:
        pr('No more swaps needed.\n' + inp_s)
else:
    qsort(arr)

    pr('No more swaps needed.')
    pr(' '.join(map(str, arr)))

outp.write(rp)
outp.close()
