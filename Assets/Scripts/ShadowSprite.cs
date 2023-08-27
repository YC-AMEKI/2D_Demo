using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShadowSprite : MonoBehaviour
{
    private Transform player;

    private SpriteRenderer thisSprite;
    private SpriteRenderer playerSprite;

    private Color color;

    [Header("时间参数")]
    public float activeTime;
    public float activeStart;

    [Header("不透明度")]
    private float alpha;
    public float alphaSet;
    public float alphaMultiplier;

    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        thisSprite = GetComponent<SpriteRenderer>();
        playerSprite = player.GetComponent<SpriteRenderer>();

        alpha = alphaSet;

        thisSprite.sprite = playerSprite.sprite;
        transform.position = player.position;
        transform.rotation = player.rotation;
        transform.localScale = player.localScale;

        activeStart = Time.time;
    }
    

    // Update is called once per frame
    void FixedUpdate()
    {
        alpha *= alphaMultiplier;
        color = new Color(0.5f, 0.5f, 1f, alpha);
        thisSprite.color = color;

        if(Time.time>=activeStart + activeTime)
        {
            //
            //ShadowPool.Instance.ReturnPool(this.gameObject);
            ShadowUnityPool.Instance.pool.Release(this.gameObject);
        }
    }
}
