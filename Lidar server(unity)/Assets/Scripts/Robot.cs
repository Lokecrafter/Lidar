using UnityEngine;


public class Robot{
    public static Robot singleton;
    public float heading = 0;
    public float x = 0;
    public float y = 0;
    public int countsPerRevolution = 360;
    public float robotWidth = 0.161f;
    public float wheelRadius = 0.0345f;

    public Robot(){
        x = 0;
        y = 0;
        heading = 0;
        singleton = this;
    }
    public void Update(float left, float right){
        float angle = (right - left) / robotWidth;

        if(left == right){
            x += Mathf.Cos(heading) * left;
            y += Mathf.Sin(heading) * left;
        }
        else{
            float radius = left / angle;
            x += (radius + robotWidth * 0.5f) * (Mathf.Sin(heading+angle) - Mathf.Sin(heading));
            y += (radius + robotWidth * 0.5f) * (-Mathf.Cos(heading+angle) + Mathf.Cos(heading));
        }
        heading += angle;
    }

    float GetWheelDistance(int counts){
        return(wheelRadius * 2 * Mathf.PI * counts / (countsPerRevolution));
    }
}

