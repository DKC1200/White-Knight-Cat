using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour, Idamageable
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform gun1;
    [SerializeField] private Transform gun2;
    [SerializeField] private GameObject rayOrigin;
    [SerializeField] private Transform player;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask layer;
    [SerializeField] private float life;
    [SerializeField] private float cooldown;
    [SerializeField] private float damage;
    [SerializeField] private float spd;
    [SerializeField] private Collider2D collider;

    private GameObject bullet1;
    private GameObject bullet2;
    private bool canShootRight = true;
    private bool canShootLeft = true;
    private Vector2 dir;
    private float vel;
    private RaycastHit2D hit;

    [SerializeField] private AudioSource die_source;
    [SerializeField] private AudioSource laser_source;
    [SerializeField] private AudioClip die_sound;
    [SerializeField] private AudioClip laser;

    [SerializeField] private Animator anim;

    void Update()
    {
        if(life <= 0)
        {
            anim.SetBool("Dead", true);
            StartCoroutine(Die());
        }

        hit = Physics2D.Raycast(rayOrigin.transform.position, rayOrigin.transform.up, 1.5f, layer, -Mathf.Infinity, Mathf.Infinity);

        dir = player.position - transform.position;

        if((dir.x < 1 & dir.x > -1) | (hit.collider == null))
        {
            vel = 0;
        }
        else
        {
            vel = spd;
        }

        dir = new Vector2 (dir.x, 0).normalized;

        rayOrigin.transform.rotation = Quaternion.Euler(0, 0, 130 * -dir.x);

        if(canShootRight == true & dir.x > 0)
        {
            laser_source.PlayOneShot(laser, 1f);
            StartCoroutine(shootRight());
        }
        else if(canShootLeft == true & dir.x < 0)
        {
            laser_source.PlayOneShot(laser, 1f);
            StartCoroutine(shootLeft());
        }

        rb.velocity = new Vector2 (dir.x * vel, 0);
    }

    public void Damage(float damageAmount)
    {
        life -= damageAmount;
    }

    private IEnumerator shootLeft()
    {
        canShootLeft = false;
        bullet1 = Instantiate(bulletPrefab, gun1.position, gun1.rotation);
        bullet1.GetComponent<Bullet>().setTag(gameObject.tag);
        bullet1.GetComponent<Bullet>().setDamage(damage);

        yield return new WaitForSeconds(cooldown);
        canShootLeft = true;
    }

    private IEnumerator shootRight()
    {
        canShootRight = false;

        bullet2 = Instantiate(bulletPrefab, gun2.position, gun2.rotation);
        bullet2.GetComponent<Bullet>().setTag(gameObject.tag);
        bullet2.GetComponent<Bullet>().setDamage(damage);

        yield return new WaitForSeconds(cooldown);
        canShootRight = true;
    }

    private IEnumerator Die(){
        die_source.PlayOneShot(die_sound, 1f);
        collider.enabled = false;
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}