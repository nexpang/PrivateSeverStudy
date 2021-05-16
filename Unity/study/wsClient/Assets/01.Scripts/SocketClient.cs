using WebSocketSharp;
using UnityEngine;
using System.Collections.Generic;

public class SocketClient : MonoBehaviour
{
    public GameObject gameManager;
    public static SocketClient instance;

    public string url = "ws://localhost";
    public int port = 32000;

    private Dictionary<string, IMsgHandler> handlerDictionary;

    public string user = "Gondr";
    private WebSocket webSocket;


    private void Awake()
    {
        handlerDictionary = new Dictionary<string, IMsgHandler>();
        DontDestroyOnLoad(gameObject);
        instance = this;
    }

    private void Start()
    {
        handlerDictionary.Add("Chat", gameManager.GetComponent<ChatHandler>());

        webSocket = new WebSocket($"{url}:{port}");
        webSocket.Connect();

        webSocket.OnMessage += (sender, e) => {
            ReceiveData((WebSocket)sender, e);
        };
    }

    public void SendData(string msg)
    {
        webSocket.Send(msg);
    }

    //CHAT:Gondr:안녕하세요 이런식으로 메시지 날라옴
    private void ReceiveData(WebSocket sender, MessageEventArgs e)
    {
        DataVO vo = JsonUtility.FromJson<DataVO>(e.Data);

        IMsgHandler handler = handlerDictionary[vo.type];

        handler.HandleMsg(vo.payload);

    }
}
