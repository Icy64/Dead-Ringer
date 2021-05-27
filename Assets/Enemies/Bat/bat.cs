﻿
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bat : MonoBehaviour
{
    public Sprite batA;
    public Sprite batB;
    GameObject player;
    Vector3 batplaceholder;
    Rigidbody2D self;
    int modifier;
    bool dashstart, dashing, idle, idletime;
    int sprite;
    float angle;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        self = GetComponent<Rigidbody2D>();
        dashing = false;
        dashstart = false;
        idle = true;
        idletime = true;
        modifier = 0;
        sprite = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //print(player.transform.position.x);
        if (idletime && idle == true)
        {
            StartCoroutine(idleing());
        }
        if (dashing == true)
        {
            print("go");
            idle = false;
            dashing = false;
            StartCoroutine(dashtest());
        }
    }

    void OnTriggerStay2D(Collider2D otherObject)
    {
        if (otherObject.tag == "Player")
        {
            if (dashstart == false)
            {
                dashing = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D otherObject)
    {
        if (otherObject.tag == "Player")
        {
            dashing = false;
        }
    }

    void spriteflip()
    {
        sprite += 1;
        if (sprite > 2)
        {
            sprite = 1;
        }
    }

    IEnumerator dashtest()
    {
        batplaceholder = transform.position;
        dashstart = true;
        self.velocity = new Vector2(0, 7f);
        yield return new WaitForSeconds(0.25f);
        Vector3 point = player.transform.position - transform.position;
        angle = Mathf.Atan2(point.y, point.x);
        if(Mathf.Cos(angle) < 0)
        {
            modifier = -2;
        }
        if (Mathf.Cos(angle) > 0)
        {
            modifier = 2;
        }

        self.velocity = new Vector2(Mathf.Cos(angle) * 12, Mathf.Sin(angle) * 12 + modifier);
        
        print(Mathf.Cos(angle));
        yield return new WaitForSeconds(0.5f);
        self.velocity = new Vector2(2f, 0);
        idle = true;
        yield return new WaitForSeconds(1f);
        transform.position = new Vector3(transform.position.x, batplaceholder.y, transform.position.z);
        dashstart = false;
    }

    IEnumerator idleing()
    {
        idletime = false;
        self.velocity = new Vector2(Random.Range(-2f, 2f), Random.Range(-2f, 2f));
        spriteflip();
        if (sprite == 1)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = batA;
        }

        if (sprite == 2)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = batB;
        }
        yield return new WaitForSeconds(.1f);
        idletime = true;
    }
}
