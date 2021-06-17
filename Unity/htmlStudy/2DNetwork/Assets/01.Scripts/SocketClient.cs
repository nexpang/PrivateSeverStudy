using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;

public class SocketClient : MonoBehaviour
{
    public string url = "ws://localhost";
    public int port = 36589;
    public GameObject handers;

    private WebSocket webSocket; // 웹 소켓 인스턴스

    private static SocketClient instance;

    public static void SendDataToSocket(string json)
    {
        instance.SendData(json);
    }

    private Dictionary<string, IMsgHandler> handlerDictionary;

    private void Awake()
    {
        instance = this;
        handlerDictionary = new Dictionary<string, IMsgHandler>();
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        handlerDictionary.Add("CHAT", handers.GetComponent<ChatHandler>());
        handlerDictionary.Add("LOGIN", handers.GetComponent<LoginHandler>());
        handlerDictionary.Add("REFRESH", handers.GetComponent<RefreshHandler>());
        handlerDictionary.Add("DISCONNECT", handers.GetComponent<DisconnectHandler>());
        handlerDictionary.Add("INITDATA", handers.GetComponent<InitHandler>());
        handlerDictionary.Add("FIRE", handers.GetComponent<FireHandler>());

        webSocket = new WebSocket($"{url}:{port}");
        webSocket.Connect();

        webSocket.OnMessage += (sender, e) =>
        {
            ReceiveData((WebSocket)sender, e);
        };

        webSocket.Send("CHAT:Hello WebServer");
    }

    private void ReceiveData(WebSocket sender, MessageEventArgs e)
    {
        DataVO vo = JsonUtility.FromJson<DataVO>(e.Data);

        //if (handlerDictionary.ContainsKey(vo.type))
        //{
        //    IMsgHandler handler = handlerDictionary[vo.type];
        //    handler.HandleMsg(vo.payload);
        //}
        //else
        //{
        //    Debug.LogWarning("존재하지 않은 프로토콜 요청" + vo.type);
        //}

        IMsgHandler handler = null;
        if (handlerDictionary.TryGetValue(vo.type, out handler))
        {
            handler.HandleMsg(vo.payload);
        }
        else
        {
            Debug.LogWarning("존재하지 않은 프로토콜 요청" + vo.type);
        }
    }

    private void SendData(string json)
    {
        webSocket.Send(json);
    }    

    void Update()
    {
        
    }

    private void OnDestroy()
    {
        webSocket.Close();
    }
}
