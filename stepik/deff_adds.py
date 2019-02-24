from bisect import bisect

n = int(input())

class S:
    def __getitem__(self, i):
        return (i * (i + 1)) // 2

    def __len__(self):
        return n + 1

    def __repr__(self):
        return '[ ' + ', '.join(str(self[i]) for i in range(len(self))) + ' ]'

s = S()
c = bisect(s, n) - 1
print(c)
print(' '.join(str(i if i < c else n - s[i - 1]) for i in range(1, c + 1)))
