using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRacket : MonoBehaviour
{
    public float speed = 30;
    public string axis = "Vertical";

    private Rigidbody2D rb;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D> () ;
    }
    void FixedUpdate()
    {
        float v = Input.GetAxisRaw(axis);
        rb.velocity = new Vector2(0, v) * speed;
    }
}
