using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Motor : MonoBehaviour
{
    public float force;
    public float initialForce;
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.up * initialForce, ForceMode2D.Force);
    }
    private void FixedUpdate()
    {
        rb.AddForce(transform.up * force, ForceMode2D.Force);
    }
}
