﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skeletonsword : MonoBehaviour
{
    GameObject god;
    controller controller;
    // Start is called before the first frame update
    void Start()
    {
        god = GameObject.FindGameObjectWithTag("god");
        controller = god.GetComponent<controller>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            controller.health -= 1;
        }
    }

}
