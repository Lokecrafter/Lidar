const int joyX = A0;
const int joyY = A1;

void setup(){
    pinMode(joyX, INPUT);
    pinMode(joyY, INPUT);

    Serial.begin(9600);
}

void loop(){

    float xVal = (analogRead(joyX) - 502) / (float)502;
    float yVal = (analogRead(joyY) - 502) / (float)502;

    xVal = clamp(xVal, -1, 1);
    yVal = clamp(yVal, -1, 1);

    float leftMotor = yVal + xVal;
    float rightMotor = yVal - xVal;

    leftMotor = clamp(leftMotor, -1, 1);
    rightMotor = clamp(rightMotor, -1, 1);

    Serial.print(leftMotor);
    Serial.print(" , ");
    Serial.println(rightMotor);
}

float clamp(float val, float minVal, float maxVal){
    val = max(val, minVal);
    val = min(val, maxVal);

    return(val);
}