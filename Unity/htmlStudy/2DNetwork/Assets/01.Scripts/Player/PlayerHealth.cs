using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public int maxHP;
    public int currentHP;

    public bool isEnemy = false;
    public InfoUI ui;

    public void SetHealthScript(TankDataVO data, bool isEnemy, InfoUI ui)
    {
        maxHP = data.maxHP;
        currentHP = maxHP;
        this.isEnemy = isEnemy;
        this.ui = ui;
    }

    public void OnDamage(int damage, Vector2 powerDir, bool isEnemy)
    {
        currentHP -= damage;
        UpdateUI();
        if(currentHP <= 0)
        {
            Die();
        }
    }
    /// <summary>
    /// 체력 게이지 업데이트 해주는 메서드.
    /// </summary>
    private void UpdateUI()
    {

    }

    public void Die()
    {
        gameObject.SetActive(false);


    }
}
