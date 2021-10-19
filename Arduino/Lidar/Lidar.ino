#include <Wire.h>
#include <LIDARLite.h>
#include <Servo.h>

#define SERVOPIN 7

//Globals
Servo servo;
LIDARLite lidarLite;
int cal_cnt = 0; //Call count
int servoAngle = 0;
int dist = 0;

void setup() {
  Serial.begin(115200);
  servo.attach(SERVOPIN);

  lidarLite.begin(0, true);
  lidarLite.configure(0);
}


bool isDecreasing = false;

void loop() {
  servo.write(servoAngle);
  delay(2);
  if(Serial.available() > 0){
    Serial.read();

    if(cal_cnt == 0){
      dist = lidarLite.distance();
    }
    else{
      dist = lidarLite.distance(false);
    }

    Serial.println(servoAngle);
    Serial.println(dist);

    //Increment reading counter
    cal_cnt++;
    cal_cnt = cal_cnt % 100;
    
    if(!isDecreasing) servoAngle++;
    else  servoAngle--;

    if(servoAngle >= 180) isDecreasing = true;
    else if(servoAngle <= 0) isDecreasing = false;
  }
}
