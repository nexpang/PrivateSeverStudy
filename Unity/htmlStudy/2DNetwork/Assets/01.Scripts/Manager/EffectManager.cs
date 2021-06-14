using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public GameObject explosionPrefab;

    private void Awake()
    {
        PoolManager.CreatePool<Explosion>(explosionPrefab, transform, 10);
    }

    public static Explosion GetExplosion()
    {
        return PoolManager.GetItem<Explosion>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
