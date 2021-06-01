using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public enum IconCategory
{
    ERR = 0,
    SYSTEM = 1
}

public class PopupManager : MonoBehaviour
{
    private static PopupManager instance = null;

    public Sprite[] icons;

    [Header("�˾� ���� ���� ���ӿ�����Ʈ")]
    public GameObject popupPanel;
    public Transform popupTrm;
    public Image popupIcon;
    public Text popupText;
    public Button btnClose;
    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("�ټ��� PopManager�� ����ǰ� �ֽ��ϴ�.");
        }
        instance = this;
    }

    public static void OpenPopup(IconCategory icon, string text)
    {
        instance.popupPanel.SetActive(true);
        instance.popupIcon.sprite = instance.icons[(int)icon];
        instance.popupText.text = text;
        instance.btnClose.interactable = false;

        instance.popupTrm.localScale = Vector3.zero;
        instance.popupTrm.DOScale(Vector3.one, 1f);
        instance.btnClose.interactable = false;
        instance.Invoke("SetBtn", 1f);
    }
    void SetBtn()
    {
        instance.btnClose.interactable = true;
    }

    void Start()
    {
        popupPanel.SetActive(false);
        btnClose.onClick.AddListener(() =>
        {
            popupPanel.SetActive(false);
        });
    }
    void Update()
    {
        
    }
}
