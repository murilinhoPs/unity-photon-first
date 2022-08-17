using WebSocketSharp;
using UnityEngine;

public class WebSocketClient : MonoBehaviour
{
    private WebSocket _webSocket;

    private void Start()
    {
        _webSocket = new WebSocket("ws://localhost:8000");
        
        _webSocket.OnOpen += (sender, args) =>
        {
            Debug.Log("Client Connected");
        };
        _webSocket.OnMessage += (sender, args) =>
        {
            Debug.Log("Message received from " + 
                      ((WebSocket)sender).Url + ", Data: " + args.Data);
        };
        
        _webSocket.Connect();
    }


    private void Update()
    {
        if (_webSocket == null) return;

        if (Input.GetMouseButtonDown(0))
        {
            _webSocket.Send("Hello");
        }
    }
}
