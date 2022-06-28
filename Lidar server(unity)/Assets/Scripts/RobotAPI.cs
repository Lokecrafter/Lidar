using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class RobotAPI : MonoBehaviour
{
    public bool isServer = false;
    Server server;
    Client client;
    public TMP_Text text;
    float x = 0;
    float y = 0;

    // Start is called before the first frame update
    void Start()
    {
        if(isServer){

            server = new Server();
            server.Start();
        }
        client = Client.singleton;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isServer){
            if(Input.GetKeyDown(KeyCode.E)){
                client.Send("toggleLED");
                client.Send("getLEDState");
            }
            if(Input.GetKeyDown(KeyCode.C)){
                client.Send("hej");
            }
            SendRemoteControl();
        }
    }

    void SendRemoteControl(){
        if(Input.GetKeyDown(KeyCode.W)){
            client.Send("y+");
            y = 1;
        }
        else if(Input.GetKeyDown(KeyCode.S)){
            client.Send("y-");
            y = -1;
        }
        else if(Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S)){
            client.Send("y0");
            y = 0;
        }
        
        if(Input.GetKeyDown(KeyCode.D)){
            client.Send("x+");
            x = 1;
        }
        else if(Input.GetKeyDown(KeyCode.A)){
            client.Send("x-");
            x = -1;
        }
        else if(Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A)){
            client.Send("x0");
            x = 0;
        }
    }
}
