from bisect import bisect_left

n = int(input())
a = list(map(int, input().split()))
cnt = 0

def merge(l1, l2):
    global cnt
    l, r = 0, 0
    m = []

    for i in range(len(l1) + len(l2)):
        if l < len(l1) and r < len(l2):
            if l1[l] > l2[r]:
                cnt += len(l1) - l
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
            

def msort(a):
    if len(a) == 1:
        return a

    mp = len(a) // 2

    return merge(msort(a[:mp]), msort(a[mp:]))
    

msort(a)
print(cnt)
