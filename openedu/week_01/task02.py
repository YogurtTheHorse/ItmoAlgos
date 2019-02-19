inp = open('input.txt', 'r')
outp = open('output.txt', 'w')

a, b = map(int, inp.read().split())
outp.write(str(a + b))
outp.close()
