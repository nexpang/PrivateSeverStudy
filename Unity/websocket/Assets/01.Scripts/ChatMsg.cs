using UnityEngine;
using UnityEngine.UI;

public class ChatMsg : MonoBehaviour
{
    public Text txtSender;
    public Text txtMsg;
    
    public void SetData(string sender, string msg)
    {
        txtSender.text = sender;
        txtMsg.text = msg;
    }
}

