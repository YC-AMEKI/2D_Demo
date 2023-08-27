using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DirectionScript : MonoBehaviour
{
    private Transform playerTransform;
    private Vector2 dashDirection;
    // Start is called before the first frame update
    void OnEnable()
    {
        playerTransform = transform.parent.GetComponent<Transform>();
        Direction();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Direction();
    }
    private void Direction()
    {
        dashDirection = InputManager.Instance.dashDirection-(Vector2)transform.position;
        dashDirection.Normalize();
        float angle = Vector2.SignedAngle(Vector2.right, dashDirection);
        transform.rotation = Quaternion.Euler(0f, 0f, angle - 45f);
        if(playerTransform.localScale.x < 0) transform.rotation = Quaternion.Euler(0f, 0f, angle -135);
        else transform.rotation = Quaternion.Euler(0f, 0f, angle - 45f);
    }
}
