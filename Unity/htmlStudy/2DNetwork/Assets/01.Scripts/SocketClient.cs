using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;

public class SocketClient : MonoBehaviour
{
    public string url = "ws://localhost";
    public int port = 36589;
    public GameObject handers;

    private WebSocket webSocket; // �� ���� �ν��Ͻ�

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
        //    Debug.LogWarning("�������� ���� �������� ��û" + vo.type);
        //}

        IMsgHandler handler = null;
        if (handlerDictionary.TryGetValue(vo.type, out handler))
        {
            handler.HandleMsg(vo.payload);
        }
        else
        {
            Debug.LogWarning("�������� ���� �������� ��û" + vo.type);
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
