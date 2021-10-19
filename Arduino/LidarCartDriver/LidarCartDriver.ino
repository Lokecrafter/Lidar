class Motor{
  public:
    Motor(byte enablePin, byte forwardPin, byte reversePin);

    int setSpeed(float motorSpeed);
  private:
    byte enable;
    byte forward;
    byte reverse;

    float speed; 
};

Motor::Motor(byte enablePin, byte forwardPin, byte reversePin){
  enable = enablePin;
  forward = forwardPin;
  reverse = reversePin;
  pinMode(enable, OUTPUT);
  pinMode(forward, OUTPUT);
  pinMode(reverse, OUTPUT);
}

int Motor::setSpeed(float motorSpeed){
  speed = motorSpeed;

  if(motorSpeed == 0){
    analogWrite(enable, 0);
    return 0;
  }
  else if(motorSpeed > 0){
    digitalWrite(forward, HIGH);
    digitalWrite(reverse, LOW);
  }
  else{
    digitalWrite(forward, LOW);
    digitalWrite(reverse, HIGH);
  }
  analogWrite(enable, 1024 * motorSpeed);

  Serial.println("Motor drives!");

  return 0;
}
//End of motor class stuff---------------------------------------------------------------------------------------------------

Motor right(5, 10, 11);
Motor left(6, 9, 9);

void setup() {
  Serial.begin(9600);
}

void loop() {
  if(Serial.available() > 0){
    Serial.read();

    right.setSpeed(1);
  }
}














/*
  if(Serial.available() > 0){
    char val = Serial.read();
    Serial.println(val);

    if(val == 'f'){
      digitalWrite(leftForward, HIGH);
      digitalWrite(leftReverse, LOW);
      digitalWrite(rightForward, HIGH);
      digitalWrite(rightReverse, LOW);

      digitalWrite(leftEnable, HIGH);
      digitalWrite(rightEnable, HIGH);
    }
    else if(val == 'r'){
      digitalWrite(leftForward, LOW);
      digitalWrite(leftReverse, HIGH);
      digitalWrite(rightForward, LOW);
      digitalWrite(rightReverse, HIGH);

      digitalWrite(leftEnable, HIGH);
      digitalWrite(rightEnable, HIGH);
    }
    delay(500);
  }
  else{
    digitalWrite(leftForward, LOW);
    digitalWrite(leftReverse, LOW);
    digitalWrite(rightForward, LOW);
    digitalWrite(rightReverse, LOW);
    
    digitalWrite(leftEnable, LOW);
    digitalWrite(rightEnable, LOW);
  }
   delay(10);
   */