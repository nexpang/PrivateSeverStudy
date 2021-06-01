using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginHandler : MonoBehaviour, IMsgHandler
{
    private TankCategory tank;
    public Image[] selectImgHolders; // 선택한 이미지들을 저장하는 공간
    public Sprite[] selectImgs; // 선택한 이미지들을 저장하는 공간
    public InputField nameInput;

    public Button redTankBtn;
    public Button blueTankBtn;

    public Button loginBtn;
    // 로그인 하면 데이터 전송
    // 잘못된 값을 입력하면 팝업 띄워주기 Dotween 깔쌈ㅇㅋ?
    // 로그인 성공시 게임 스테이지 들어가기
    // 게임 스테이지 타일맵으로 만들기

    private void Awake()
    {
        tank = TankCategory.Red; // 기본값 Red
        redTankBtn.onClick.AddListener(() =>
        {
            tank = TankCategory.Red;
            foreach(Image img in selectImgHolders)
            {
                img.sprite = selectImgs[1];
            }
            selectImgHolders[(int)TankCategory.Red].sprite = selectImgs[0];
        });

        blueTankBtn.onClick.AddListener(() =>
        {
            tank = TankCategory.Blue;
            foreach (Image img in selectImgHolders)
            {
                img.sprite = selectImgs[1];
            }
            selectImgHolders[(int)TankCategory.Blue].sprite = selectImgs[0];
        });

        loginBtn.onClick.AddListener(() =>
        {
            string name = nameInput.text;

            if(name == "")
            {
                PopupManager.OpenPopup(IconCategory.SYSTEM, "이름은 반드시 입력하셔야 합니다.");
                return;
            }
            LoginVO loginVO = new LoginVO(tank, name);
            string payload = JsonUtility.ToJson(loginVO);
            DataVO dataVO = new DataVO();
            dataVO.type = "LOGIN";
            dataVO.payload = payload;

            string json = JsonUtility.ToJson(dataVO);

            SocketClient.SendDataToSocket(json);
        });
    }

    public void HandleMsg(string payload)
    {
        TransformVo vo = JsonUtility.FromJson<TransformVo>(payload);

        
    }
}
