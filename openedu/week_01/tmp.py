import random

with open('input.txt', 'w') as f:
    f.write('0\n')
    arr = sorted(random.randint(-10 ** 9, 10 ** 9) for i in range(5000))
    f.write(' '.join(str(x) for x in arr))
