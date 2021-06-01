using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginHandler : MonoBehaviour, IMsgHandler
{
    private TankCategory tank;
    public Image[] selectImgHolders; // ������ �̹������� �����ϴ� ����
    public Sprite[] selectImgs; // ������ �̹������� �����ϴ� ����
    public InputField nameInput;

    public Button redTankBtn;
    public Button blueTankBtn;

    public Button loginBtn;
    // �α��� �ϸ� ������ ����
    // �߸��� ���� �Է��ϸ� �˾� ����ֱ� Dotween ��Ӥ���?
    // �α��� ������ ���� �������� ����
    // ���� �������� Ÿ�ϸ����� �����

    private void Awake()
    {
        tank = TankCategory.Red; // �⺻�� Red
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
                PopupManager.OpenPopup(IconCategory.SYSTEM, "�̸��� �ݵ�� �Է��ϼž� �մϴ�.");
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
