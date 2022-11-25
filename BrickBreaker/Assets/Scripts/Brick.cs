using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [Header("Variables")]
    [Space(5)]
    public int hits = -1;
    public int points = 100;
    public Vector3 rotator;
    public Material materialHits;
    Material _orgMaterial;
    Renderer _render;
    void Start()
    {
        transform.Rotate(rotator * (transform.position.x + transform.position.y)*0.1f);
        _render = GetComponent<Renderer>();
        _orgMaterial= _render.sharedMaterial;
    }

    void Update()
    {
        transform.Rotate(rotator * Time.deltaTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        hits--;
        // score points
        if (hits < 0)
        {
            Destroy(gameObject);    
        }
        _render.sharedMaterial=materialHits;
        Invoke("RestoreMaterial",0.05f);
    }

    void RestoreMaterial()
    {
        _render.sharedMaterial = _orgMaterial;
    }
}
