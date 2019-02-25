from bisect import bisect_left

try:
    inp = open('input.txt', 'r')
    outp = open('output.txt', 'w')

    input = inp.readline
    print = lambda *args: outp.write(' '.join(map(str, args)) + '\n')
except:
    pass

n = int(input())
a = list(map(int, input().split()))

def merge(l1, l2):
    l, r = 0, 0
    m = []

    for i in range(len(l1) + len(l2)):
        if l < len(l1) and r < len(l2):
            if l1[l] > l2[r]:
                m.append(l2[r])
                r += 1
            else:
                m.append(l1[l])
                l += 1
        elif r < len(l2):
            m.append(l2[r])
            r += 1
        else:
            m.append(l1[l])
            l += 1

    return m
            

def msort(a, l=0, r=len(a)-1):
    if len(a) == 1:
        return a

    mp = len(a) // 2

    m = merge(msort(a[:mp], l, l + mp-1), msort(a[mp:], l + mp, r))
    print(l + 1, r + 1, m[0], m[-1])
    return m
    

print(*msort(a))
