# i thought that oneline solution would be fon
from collections import Counter
print(*sum(([v] * c for (v, c) in sorted(Counter(map(int, [input() for i in [0,0]][1].split())).most_common(), key=lambda v:v[0])), []), end=' ')
