import serial
import cv2
import time 
import numpy as np
from math import pi, sin, cos
from pylab import *

    
    
    
    

width1, height1 = 1000, 1000

def create_blank(width, height, rgb_color=(0, 0, 0)):
    """Create new image(numpy array) filled with certain color in RGB"""
    # Create black blank image
    image = np.zeros((height, width, 3), np.uint8)

    # Since OpenCV uses BGR, convert the color first
    color = tuple(reversed(rgb_color))
    # Fill image with color
    image[:] = color

    return image

def getPoints(lidarData):
    sp = lidarData.split()
    if len(sp) == 0:
        return

    sp.pop(0)

    
    coords = []
    
    for p in sp:
        filt = p.strip('()').split(',')
        coords.append((float(filt[0]), float(filt[1]) * 10))
    return coords

def computeCartesian(polar):
    cartesian = []
    #Checking derivative
    angles = []
    distances = []
    for p in polar:
        angle = p[0] % (2 * pi)
        dist = p[1]
        
        angles.append(angle)
        distances.append(dist)
        
        cartesian.append(   (cos(angle) * dist, sin(angle) * dist)    )
    plot(angles, distances)
    show()
    return cartesian
        


def main():
    ser = serial.Serial(
        port='COM15', 
        baudrate = 115200,
        parity=serial.PARITY_NONE,
        stopbits=serial.STOPBITS_ONE,
        bytesize=serial.EIGHTBITS,
        timeout=1
    )    
    
    
    
    scaleFact = 0.1

    color = (0, 0, 0)

    def getSerialLine():
        data = ser.readline()
        data = str(data[0:len(data)].decode("utf-8"))
        return data
        
    log = open("pointsLog.txt", 'w')

    while True:
        img = create_blank(width1, height1, rgb_color=color)
        cv2.circle(img, (int(width1*0.5), int(height1*0.5)), 10, (0,0,255), cv2.FILLED)
        
        data = getSerialLine()
        if data[0] == 'L':
            polarCoords = getPoints(data)
            cartesianCoords = computeCartesian(polarCoords)
            for c in cartesianCoords:
                newC = (round(c[0]), round(c[1]))
                
                #Draw circle on image
                cv2.circle(img, (int(newC[0] * scaleFact + width1*0.5), int(newC[1] * -scaleFact + height1*0.5)), 2, (255,255,255), cv2.FILLED)
                
                #Log coordinates
                print(str(newC), file=log, end="")
                print("   ", file=log, end="")
            print("", file=log)
        
        cv2.imshow("Lidar render", img)
        
        
        if cv2.waitKey(1) & 0xFF == ord('q'):
            break # Breaks while-loop
    
    file.close()
    cv2.destroyAllWindows()
    
    

if __name__ == '__main__':
    main()