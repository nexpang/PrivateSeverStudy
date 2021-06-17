using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    private PlayerRPC rpc;
    public int maxHP;
    public int currentHP;

    public bool isEnemy = false;
    public InfoUI ui;

    private void Awake()
    {
        rpc = GetComponent<PlayerRPC>();
    }

    public void SetHealthScript(TankDataVO data, bool isEnemy, InfoUI ui)
    {
        maxHP = data.maxHP;
        currentHP = maxHP;
        this.isEnemy = isEnemy;
        this.ui = ui;
    }

    public void OnDamage(int damage, Vector2 powerDir, bool isEnemy)
    {
        if (rpc.isRemote) return;
        currentHP -= damage;
        UpdateUI();
        HitVO vo = new HitVO(GameManager.instance.socketId, currentHP);
        string payload = JsonUtility.ToJson(vo);
        DataVO dataVO = new DataVO();
        dataVO.type = "HIT";
        dataVO.payload = payload;

        SocketClient.SendDataToSocket(JsonUtility.ToJson(dataVO));

        if(currentHP <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// 체력 게이지 업데이트 해주는 메서드.
    /// </summary>
    public void UpdateUI()
    {
        
    }

    public void Die()
    {
        gameObject.SetActive(false);


    }
}
