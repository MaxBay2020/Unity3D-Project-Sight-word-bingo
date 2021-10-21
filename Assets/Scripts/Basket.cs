using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveBasket();
    }

    private void MoveBasket()
    {
        Vector3 mousePos = Input.mousePosition;
        //mousePos.z = -Camera.main.transform.position.z;

        mousePos.z = Camera.main.transform.position.z;
        Vector2 mousePosWorld = Camera.main.ScreenToWorldPoint(mousePos);

        Vector3 pos = transform.position;
        pos.x = mousePosWorld.x;
        transform.position = pos;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Apple"))
        {
            Destroy(collision.gameObject);
        }
    }
}
