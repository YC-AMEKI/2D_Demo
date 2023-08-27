using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ShadowUnityPool : Singleton<ShadowUnityPool>
{
    public GameObject Shadow;

    public ObjectPool<GameObject> pool;

    protected override void Awake()
    {
        base.Awake();
        pool = new ObjectPool<GameObject>(CreateFunc, ActionOnGet, ActionOnRelease, ActionOnDestroy, true, 10, 20);
    }

    private void ActionOnDestroy(GameObject obj)
    {
        Destroy(obj);
    }

    private void ActionOnRelease(GameObject obj)
    {
        obj.gameObject.SetActive(false);
    }

    private void ActionOnGet(GameObject obj)
    {
        obj.gameObject.SetActive(true);
    }

    private GameObject CreateFunc()
    {
        var obj = Instantiate(Shadow, transform);
        return obj;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
