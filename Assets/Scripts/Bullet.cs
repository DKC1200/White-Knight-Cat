using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    private string ignoreTag;
    private float damageAmount;

    void Start()
    {
        
    }

    void Update()
    {
        rb.linearVelocity = transform.up * speed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag != ignoreTag)
        {
            collision.gameObject.GetComponent<Idamageable>()?.Damage(damageAmount);
            Destroy(gameObject);
        }
    }

    public void setDamage(float dmg)
    {
        damageAmount = dmg;
    }

    public void setTag(string tag)
    {
        ignoreTag = tag;
    }
}