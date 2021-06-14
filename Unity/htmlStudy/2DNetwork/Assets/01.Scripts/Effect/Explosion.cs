using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private ParticleSystem particle;

    private void Awake()
    {
        particle = GetComponent<ParticleSystem>();
    }

    /// <summary>
    /// ��ƼŬ�� ��ǥ�� ���Ӱ� �������ִ� �ż���
    /// </summary>
    /// <param name="position">��ƼŬ�� ��ġ ���� Vector3 Ÿ��</param>
    public void ResetPos(Vector3 position)
    {
        transform.position = position;
        particle.Play();
        Invoke("SetDisable", 3f);
    }

    private void SetDisable()
    {
        gameObject.SetActive(false);
    }
}
