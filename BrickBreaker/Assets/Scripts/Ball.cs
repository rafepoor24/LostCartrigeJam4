using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [Header("Variables")]
    [Space(5)]
    private Rigidbody rbBall;
    private float _speed=20f;
    private Vector3 _velocity;


    void Start()
    {
        rbBall=GetComponent<Rigidbody>();   
        rbBall.velocity = Vector3.down*_speed;
        
    }
    private void FixedUpdate()
    {
        rbBall.velocity = rbBall.velocity.normalized * _speed;
        _velocity = rbBall.velocity;
    }
    private void OnCollisionEnter(Collision collision)
    {
        rbBall.velocity = Vector3.Reflect(_velocity, collision.contacts[0].normal);
    }
}
