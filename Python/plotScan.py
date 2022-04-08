from pylab import *

file = open("pointsLog.txt")

for l in file:
    sp = l.split("   ")
    x = []
    y = []
    
    sp.pop(len(sp) - 1)
    
    for c in sp:
        coord = list(map(float, c.strip("()").split(",")))
        x.append(coord[0])
        y.append(coord[1])
    
        
    
    scatter(x,y, c = 'r', s = 50)
    
    show()
    