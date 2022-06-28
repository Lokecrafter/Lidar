using System;
using System.Text;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class Server
{
    Socket serverSocket = null;
    int port = 99;
    int backlog = 5;
    List<Socket> clients = new List<Socket>();
    byte[] buffer = new byte[1024];

    public Server(){
        serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    }
    public void Start()
    {
        serverSocket.Bind(new IPEndPoint(IPAddress.Any, port));
        serverSocket.Listen(backlog);
        Accept();
    }
    //Allow to accept new clients
    public void Accept(){
        serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);
    }
    private void AcceptCallback(IAsyncResult ar){
        Debug.Log("Accepted");
        Socket client = serverSocket.EndAccept(ar);
        clients.Add(client);
        client.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), client);
        Accept();
    }
    private void ReceiveCallback(IAsyncResult ar){
        Socket client = (Socket) ar.AsyncState;
        int size = client.EndReceive(ar);
        byte[] receivedBytes = new byte[size];
        Array.Copy(buffer, receivedBytes, size);
        string data = Encoding.ASCII.GetString(receivedBytes);

        //Here we see the data sent from the client
        Debug.Log(data);

        string response = "Hej";
        byte[] responseBytes = Encoding.ASCII.GetBytes(response);
        client.BeginSend(responseBytes, 0, responseBytes.Length, SocketFlags.None, new AsyncCallback(SendCallback), client);
    }
    private void SendCallback(IAsyncResult ar){
        Socket client = (Socket) ar.AsyncState;
        client.Shutdown(SocketShutdown.Send);
        clients.Remove(client);
        Debug.Log("Shutting down connection");
    }
}
