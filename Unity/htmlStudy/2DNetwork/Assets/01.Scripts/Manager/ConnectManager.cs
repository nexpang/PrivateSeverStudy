using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConnectManager : MonoBehaviour
{
    public InputField txtIp;
    public InputField txtport;

    public Button btnConnect;
    public CanvasGroup connetPanel;

    bool isConnected = false;
    private void Start()
    {
        btnConnect.onClick.AddListener(() =>
        {
            if (isConnected) return;
            if(txtport.text == ""|| txtIp.text == "")
            {
                PopupManager.OpenPopup(IconCategory.ERR, "필수값이 빠져 있습니다.");
                return;
            }
        
            SocketClient.instance.ConnectSocket(txtIp.text, txtport.text);
            connetPanel.alpha = 0;
            connetPanel.interactable = false;
            connetPanel.blocksRaycasts = false;
            UIManager.OpenLoginPanel();
        });
    }
}
