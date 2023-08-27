using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    public bool isGround,isWallLeft, isWallRight;
    public float checkRadius, wallCheckRadius;
    public LayerMask groundLayer;
    public Vector3 offset = new Vector3(0, 1, 0);
    public Vector3 checkWallOffsetLeft, checkWallOffsetRight;
    //public Vector3 pointALeft, pointBLeft, pointARight, pointBRight;
    // Start is called before the first frame update
    void Start()
    {
        isGround = false;
        isWallLeft = false;
        isWallRight = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Check();
    }

    private void Check()
    {
        isGround = Physics2D.OverlapCircle(transform.position + offset, checkRadius, groundLayer);
        isWallLeft = Physics2D.OverlapCircle(transform.position + checkWallOffsetLeft, wallCheckRadius, groundLayer);
        isWallRight = Physics2D.OverlapCircle(transform.position + checkWallOffsetRight, wallCheckRadius, groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position + offset, checkRadius);
        Gizmos.DrawWireSphere(transform.position+checkWallOffsetLeft, wallCheckRadius);
        Gizmos.DrawWireSphere(transform.position+checkWallOffsetRight, wallCheckRadius);
    }
}
