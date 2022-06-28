using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Client client;
    public LineRenderer lr;

    public int countsPerRevolution = 360;
    public float robotWidth = 0.161f;
    public float wheelRadius = 0.0345f;
    public float heading = Mathf.PI / 2;
    private int lastLeft = 0;
    private int lastRight = 0;
    private int posIndex = 0;

    void Start()
    {
        client = Client.singleton;
        lastLeft = client.leftCounter;
        lastRight = client.rightCounter;
        lr.positionCount = 1;
        lr.SetPosition(posIndex, transform.position);
    }

    void Update()
    {
        float deltaLeft = GetWheelDistance(client.leftCounter - lastLeft);
        float deltaRight = GetWheelDistance(client.rightCounter - lastRight);

        Vector2 pos = GetNewPos(deltaLeft, deltaRight);
        transform.position = new Vector3(pos.x, pos.y, 0);
        transform.rotation = Quaternion.Euler(0,0,heading * Mathf.Rad2Deg -90);

        lastLeft = client.leftCounter;
        lastRight = client.rightCounter;

        if(deltaLeft != 0 || deltaRight != 0){
            lr.positionCount++;
            posIndex++;
            Debug.Log("Line update : " + transform.position);
            lr.SetPosition(posIndex, transform.position);
        }
    }

    float GetNewAngle(float left, float right){
        return (right - left) / robotWidth;
    }
    Vector2 GetNewPos(float left, float right){
        if(left != 0 && right != 0){
            lr.positionCount++;
            posIndex++;
        }
        if(left == right){
            return transform.position + new Vector3(Mathf.Cos(heading), Mathf.Sin(heading), 0) * left;
        }

        float angle = GetNewAngle(left, right);
        heading += angle;
        float radius = left / angle;

        Vector2 currentPos = new Vector2(transform.position.x, transform.position.y);
        Vector2 direction = (new Vector2(Mathf.Sin(heading+angle), -Mathf.Cos(heading+angle)) - new Vector2(Mathf.Sin(heading), -Mathf.Cos(heading)));
        Vector2 newPos = currentPos + (radius + robotWidth * 0.5f) * direction;
        return newPos;
    }
    float GetWheelDistance(int counts){
        return(wheelRadius * 2 * Mathf.PI * counts / (countsPerRevolution));
    }
}
