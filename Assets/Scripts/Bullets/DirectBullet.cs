﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectBullet : MonoBehaviour {
    //총알의 파괴력
    public int damage;
    //총알 초기 위치
    public Vector3 first;
    //총알 발사 속도
    public float speed = 1000.0f;
    public float distance;
    public GameObject ExpEffect;
    public float hitCount;
    private int hitCountDown;
    public int bulletType;
    GameObject attacker;

    void Start()
    {
        hitCountDown = 0;
        first = gameObject.transform.position;
        GetComponent<Rigidbody>().AddForce(transform.forward * speed);
    }

    void Update()
    {
        if (Vector3.Distance(first, transform.position) >= distance)
        {
            GameObject exp = Instantiate(ExpEffect, transform.position, transform.rotation);
            Destroy(exp, 1.0f);
            Destroy(gameObject);
        }
    }

    public void GetDamageType(int damage, int hitCount, GameObject attacker, float distance)
    {
        this.damage = damage;
        this.hitCount = hitCount;
        this.attacker = attacker;
        this.distance = distance;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        //충돌한 게임오브젝트의 태그값 비교
        if (other.transform.tag == "Tank")
        {
            Debug.Log(other.name);
            hitCountDown++;
            GameObject exp = Instantiate(ExpEffect, transform.position, transform.rotation);
            Destroy(exp, 1.0f);
            if (hitCountDown == hitCount)
            {
                Destroy(gameObject);
            }
        }
        else if (other.transform.tag == "DestroyObject")
        {
            Destroy(gameObject);
        }
        else if (other.transform.tag == "Soldier")
        {
            hitCountDown++;
            if (hitCountDown == hitCount)
            {
                Destroy(gameObject);
            }
        }
    }
}
