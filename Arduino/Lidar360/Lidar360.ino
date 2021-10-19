#include <Wire.h>
#include <LIDARLite.h>

#define HALLEFCT 2
#define MOTORPIN 9 //Needs to be PWM pin
const int minLowHallTime = 30;
const float motorPercent = 100; //between 100 and 0

//Globals
LIDARLite lidarLite;
int cal_cnt = 0; //Call count
int angle = 0;
int dist = 0;
bool sameRot = false;

unsigned long prevTime = 0;
unsigned long deltaTime = 0;

void setup() {
  Serial.begin(115200);
  pinMode(HALLEFCT, INPUT);

  lidarLite.begin(0, true);
  lidarLite.configure(0);

  analogWrite(MOTORPIN, map(motorPercent, 0, 100, 0, 1023));
}




void loop() {
  if(digitalRead(HALLEFCT) == LOW && sameRot == false){
    sameRot = true;
    setDeltaTime();  
  }
      
  if(Serial.available() > 0){
    Serial.read();

    if(cal_cnt == 0){
      dist = lidarLite.distance();
    }
    else{
      dist = lidarLite.distance(false);
    }

    angle = getAngle();

    angle %= 360;

    Serial.print(angle);
    Serial.print(",");
    Serial.println(dist);

    //Increment reading counter
    cal_cnt++;
    cal_cnt = cal_cnt % 100;
  }

  //reset samerot
  if((millis() - prevTime) >= minLowHallTime){
    sameRot = false;
  }
}

float getAngle(){
  //Get rotationspeed

  unsigned long revolutionTime = millis() - prevTime;

  float revolutionPercentage = (float)revolutionTime / (float)deltaTime;

  return(revolutionPercentage * 360);
}

void setDeltaTime(){
  unsigned long currentTime = millis();
  
  deltaTime = currentTime - prevTime;
  prevTime = currentTime;
}
