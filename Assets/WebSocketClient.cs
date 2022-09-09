using System;
using System.Threading.Tasks;
using UnityEngine;
using NativeWebSocket;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

[Serializable]
class Jogador
{
    private string _event;
    private string name;
    private Vector2 position;

    public Jogador(string name, Vector2 position)
    {
        this.name = name;
        this.position = position;
        _event = "player_pos";
    }

    public override string ToString() => "Event - " + _event + ", Nome Game Object: " + name + " Posição: " + "x: " + position.x +
                                         ", y: " + position.y;
}

public class WebSocketClient : MonoBehaviour
{
    private WebSocket _webSocket;
    [SerializeField] private string socketUrl = "ws://localhost:3000";

    private void Start()
    {
        SetupSocket();
    }

    private void Update()
    {
        if (_webSocket == null) return;

#if !UNITY_WEBGL || UNITY_EDITOR
        _webSocket.DispatchMessageQueue();
#endif
        
        if (Input.GetMouseButtonDown(0))
        {
            Emit();
        }
    }

    private void Emit()
    {
        var player = new Jogador(gameObject.name, transform.position);

        _webSocket.SendText(player.ToString());
        var playerData = JsonUtility.ToJson(player);
        _webSocket.SendText(playerData);
    }

    private async void SetupSocket()
    {
        _webSocket = new WebSocket(socketUrl);

        _webSocket.OnOpen += () =>
        {
            Debug.Log("Connection open!");
        };

        _webSocket.OnError += (e) => { Debug.Log("Error! " + e); };

        _webSocket.OnClose += (e) =>
        {
            Debug.Log("Connection closed!");
            WaitAndReconnect();
        };

        _webSocket.OnMessage += (bytes) =>
        {
            Debug.Log("OnMessage!");

            // getting the message as a string
            var message = System.Text.Encoding.UTF8.GetString(bytes);
            Debug.Log("OnMessage! " + message);
        };
        
        // waiting for messages
        await _webSocket.Connect();
    }

    private async void WaitAndReconnect()
    {
        await QuitSocket();
        await Task.Delay(1000);

        SetupSocket();
    }

    private async Task QuitSocket()
    {
        try
        {
            await _webSocket.Close();
        }
        catch (Exception e)
        {
            print(e);
            throw;
        }
    }

    private async void OnApplicationQuit()
    {
        await QuitSocket();
    }
}