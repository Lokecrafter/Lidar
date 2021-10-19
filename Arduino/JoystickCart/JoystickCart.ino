class Motor{
  public:
    Motor(byte enablePin, byte forwardPin, byte reversePin);

    int setSpeed(float motorSpeed);
    float getSpeed();
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
  analogWrite(enable, 255 * abs(motorSpeed));

  return 0;
}
float Motor::getSpeed(){
    return speed;
}
//End of motor class stuff---------------------------------------------------------------------------------------------------

Motor right(6, 10, 11);
Motor left(5, 9, 8);

const int joyX = A0;
const int joyY = A1;

float speedFactor = 1;

void setup(){
  pinMode(joyX, INPUT);
  pinMode(joyY, INPUT);

  Serial.begin(115200);
}

void loop(){
  float xVal = (analogRead(joyX) - 502) / (float)502;
  float yVal = (analogRead(joyY) - 502) / (float)502;

  //calcSpeed(xVal, yVal);

  if(Serial.available() > 0){
    if(char(Serial.read()) == '#'){
      determineDataType();
    }
  }
}

void calcSpeed(float joyX, float joyY){
  joyX = clamp(joyX, -1, 1);
  joyY = clamp(joyY, -1, 1);

  if(joyY < 0)  joyX = joyX * -1; //Handles steering so it feels natural

  float leftMotor = joyY + joyX;
  float rightMotor = joyY - joyX;

  leftMotor = clamp(leftMotor, -1, 1);
  rightMotor = clamp(rightMotor, -1, 1);

  right.setSpeed(rightMotor * speedFactor);
  left.setSpeed(leftMotor * speedFactor);

  Serial.print(left.getSpeed());
  Serial.print(" , ");
  Serial.println(right.getSpeed());
}

//Reads controller data from serial. Will always contain 7 bytes
void readControllerData(){
  const byte byteAmnt = 9;
  String dataString = "";

  for (byte i = 0; i < byteAmnt; i = i)
  {
    if(Serial.available() > 0){
      dataString += char(Serial.read());
      i++;
    }
  }

  String xString = "";
  String yString = "";
  bool parsingX = true;

  for (byte i = 0; i < byteAmnt; i++)
  {
    if(dataString[i] == ','){
      parsingX = false;
      continue;
    }
    if(parsingX){
      xString += dataString[i];
    }
    else{
      yString += dataString[i];
    }
  }
  
  float xVal = xString.toFloat();
  float yVal = yString.toFloat();
  Serial.println(dataString);
  Serial.print("Parsade värden");
  Serial.print(xVal);
  Serial.print(", ");
  Serial.println(yVal);

  calcSpeed(xVal, yVal);
}


float clamp(float val, float minVal, float maxVal){
    val = max(val, minVal);
    val = min(val, maxVal);

    return(val);
}

void determineDataType(){
  while(true){
    if(Serial.available() > 0){
      char dataType = char(Serial.read());
      Serial.println(dataType);

      switch (dataType)
      {
      case 'j':
        readControllerData();
        break;
      
      default:
        break;
      }
      break;
    }
  }
}