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

    // Start is called before the first frame update
    void Start()
    {
        if(isServer){

            server = new Server();
            server.Start();
        }
        else{
            client = new Client();
            client.Start();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!isServer){
            if(Input.GetKeyDown(KeyCode.E)){
                client.Send("toggleLED");
                client.Send("getLEDState");
            }
            text.text = client.GetLed();
        }
    }
}
