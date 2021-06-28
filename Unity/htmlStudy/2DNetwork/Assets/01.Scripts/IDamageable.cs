using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    /// <summary>
    /// �ǰ� ó�� �ż���
    /// </summary>
    /// <param name="damage"> �ǰݽ� �� ������</param>
    public void OnDamage(int damage, Vector2 powerDir, bool isEnemy, int shooterId);
}
