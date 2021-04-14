using WebSocketSharp;
using UnityEngine;

public class SocketClient : MonoBehaviour
{
    public static SocketClient instance;

    public string url = "ws://localhost";
    public int port = 32000;
    public string user = "Gondr";
    private WebSocket webSocket;


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        instance = this;
    }

    private void Start()
    {
        webSocket = new WebSocket(url + ":" + port);
        webSocket.Connect();

        webSocket.OnMessage += (sender, e) => {
            ReceiveData((WebSocket)sender, e);
        };
    }

    public void SendData(string msg){
        webSocket.Send("CHAT:"+ user + ":"+msg);
    }

    //CHAT:Gondr:안녕하세요 이런식으로 메시지 날라옴
    private void ReceiveData(WebSocket sender, MessageEventArgs e){
        DataObject obj = new DataObject(e.Data);
        if(obj.op == "CHAT"){
            ChatManager.instance.AddMessage(obj.payload);
        }
    }

}
