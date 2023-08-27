using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowPool : Singleton<ShadowPool>
{
    public GameObject shadowPrefab;
    public int shadowCount;
    private Queue<GameObject> availableObjects = new Queue<GameObject>();

    protected override void Awake(){
        base.Awake();
    }

    public void FillPool()
    {
        for (int i = 0; i<shadowCount; i++)
        {
            var newShadow = Instantiate(shadowPrefab);
            newShadow.transform.SetParent(transform);
            //取消启用
            ReturnPool(newShadow);
        }
    }

    public void ReturnPool(GameObject gameObject)
    {
        gameObject.SetActive(false);
        availableObjects.Enqueue(gameObject);
    }

    public GameObject GetFromPool()
    {
        if (availableObjects.Count == 0)
        {
            FillPool();
        }
        var outShadow = availableObjects.Dequeue();
        outShadow.SetActive(true);
        return outShadow;
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
