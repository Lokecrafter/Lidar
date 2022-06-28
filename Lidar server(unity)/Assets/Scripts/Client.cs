using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;

public class Client
{
    const string url = "ws://192.168.4.1:1337/";
    WebSocket ws = null;
    private string ledState = "0";

    public void Start(){
        ws = new WebSocket(url);
        ws.Connect();
        ws.Send("getLEDState");

        ws.OnMessage += Ws_OnMessage;
        //doSend("getLEDState");
        //doSend("toggleLED");
    }

    private void  Ws_OnMessage(object sender, MessageEventArgs e){
        Debug.Log("Recieved from server" + e.Data);
        ledState = e.Data;
    }

    public void Send(string msg){
        ws.Send(msg);
    }
    public string GetLed(){
        return ledState;
    }
}
