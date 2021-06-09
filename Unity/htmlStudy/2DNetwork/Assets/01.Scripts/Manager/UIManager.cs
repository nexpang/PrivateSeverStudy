using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject infoPrefab;
    public CanvasGroup cvsLogin;

    private void Awake()
    {
        instance = this;
        PoolManager.CreatePool<InfoUI>(infoPrefab, transform, 8);
        cvsLogin.alpha = 1;
        cvsLogin.interactable = true;
        cvsLogin.blocksRaycasts = true;
    }

    public static InfoUI SetInfoUI(Transform player, string name)
    {
        InfoUI ui = PoolManager.GetItem<InfoUI>();
        ui.SetTarget(player, name);
        return ui;
    }

    public static void CloseLoginPanel()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(DOTween.To(() => instance.cvsLogin.alpha, value => instance.cvsLogin.alpha = value, 0f, 1f));
        seq.AppendCallback(() =>
        {
            instance.cvsLogin.interactable = false;
            instance.cvsLogin.blocksRaycasts = false;
        });
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
