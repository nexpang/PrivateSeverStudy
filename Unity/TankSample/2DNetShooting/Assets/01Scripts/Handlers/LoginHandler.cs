using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LoginHandler : MonoBehaviour, IMsgHandler
{
    private TankCategory tank;
    public Image[] selectImageHolders; //선택 이미지 저장공간

    public Sprite[] selectImages;
    public Text nameInput;
    
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        tank = TankCategory.BlueTank;
    }

    public void HandleMsg(string payload)
    {
        TransformVO vo = JsonUtility.FromJson<TransformVO>(payload);
        
        GameManager.instance.ChangeToGame(vo.point, vo.rotation, vo.socketId, tank); //게임매니저를 호출해서 넘긴다.
    }

    public void SendMsg(){
        //여기서 nameInput에 입력값이 똑바로 입력되었는지 확인해야한다.
        if(nameInput.text.Trim() == ""){
            Debug.Log("칸을 채워주세요");
            return;
        }
        LoginVO vo = new LoginVO("Login", tank, nameInput.text);
        string json = JsonUtility.ToJson(vo);
        SocketClient.instance.SendData(json);
    }

    public void SetBlueTank(){
        tank = TankCategory.BlueTank;
        foreach(Image image in selectImageHolders){
            image.sprite = selectImages[1]; //선택안함으로 덮고
        }
        selectImageHolders[(int)TankCategory.BlueTank].sprite = selectImages[0];
    }

    public void SetRedTank(){
        tank = TankCategory.RedTank;
        foreach(Image image in selectImageHolders){
            image.sprite = selectImages[1]; //선택안함으로 덮고
        }
        selectImageHolders[(int)TankCategory.RedTank].sprite = selectImages[0];
    }
}
