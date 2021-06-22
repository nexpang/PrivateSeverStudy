using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class InfoUI : MonoBehaviour
{
    public Transform playerTrm;
    public float followSpeed = 8f;
    public Text txtName;
    public Transform hpBar;

    private CanvasGroup cg;
    private bool on = true;

    private void Awake()
    {
        cg = GetComponent<CanvasGroup>();
    }

    public void SetVisible(bool on)
    {
        this.on = on;
        cg.alpha = on ? 1 : 0;
    }    

    public void SetTarget(Transform playerTrm, string name)
    {
        this.playerTrm = playerTrm;
        txtName.text = name;
        gameObject.SetActive(true);
    }

    private void LateUpdate()
    {
        if (!on)
            return;
        Vector3 pos = Camera.main.WorldToScreenPoint(playerTrm.position);
        Vector3 nextPos = Vector3.Lerp(transform.position, pos, Time.deltaTime * followSpeed);
        transform.position = nextPos;
    }

    public void UpdateHPBar(float ratio)
    {
        ratio = Mathf.Clamp(ratio, 0, 1);
        hpBar.DOScaleX(ratio, 0.3f);
    }
}
