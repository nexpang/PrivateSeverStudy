using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    /// <summary>
    /// 피격 처리 매서드
    /// </summary>
    /// <param name="damage"> 피격시 들어갈 데미지</param>
    public void OnDamage(int damage, Vector2 powerDir, bool isEnemy, int shooterId);
}
