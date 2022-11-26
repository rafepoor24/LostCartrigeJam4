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
    Renderer _renderer;



    void Start()
    {
        rbBall=GetComponent<Rigidbody>();   
       
        _renderer=GetComponent<Renderer>();
        Invoke("Launch", 0.9f);
        
    }
    void Launch()
    {
        rbBall.velocity = Vector3.up * _speed;
    }

    private void FixedUpdate()
    {
        rbBall.velocity = rbBall.velocity.normalized * _speed;
        _velocity = rbBall.velocity;
        if (!_renderer.isVisible)
        {
            GameManager.Instance.Balls--;
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        rbBall.velocity = Vector3.Reflect(_velocity, collision.contacts[0].normal);
    }
}
