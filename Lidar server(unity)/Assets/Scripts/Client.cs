using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;

public class Client : MonoBehaviour
{
    const string url = "ws://192.168.4.1:1337/";
    WebSocket ws = null;
    public static Client singleton;
    public int leftCounter = 0;
    public int rightCounter = 0;

    public void Awake(){
        ws = new WebSocket(url);
        ws.Connect();

        ws.OnMessage += Ws_OnMessage;

        singleton = this;
    }

    private void  Ws_OnMessage(object sender, MessageEventArgs e){
        //Debug.Log("Recieved from server : " + e.Data);
        string[] counters = e.Data.Split(',');
        leftCounter = int.Parse(counters[0]);
        rightCounter = int.Parse(counters[1]);
    }

    public void Send(string msg){
        ws.Send(msg);
    }
}
