using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatBomb : MonoBehaviour
{
    public GameObject explosion;

    void Start()
    {

    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            GameObject exp = Instantiate(explosion);
            exp.transform.position = transform.position;
            Destroy(gameObject);
        }
    }
}
