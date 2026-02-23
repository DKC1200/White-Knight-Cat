using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour, Idamageable
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform gun1;
    [SerializeField] private Transform gun2;
    [SerializeField] private float life;
    [SerializeField] private float cooldown;
    [SerializeField] private float damage;

    private GameObject bullet1;
    private GameObject bullet2;
    private bool canShoot = true;

    void Update()
    {
        if(life <= 0)
        {
            Destroy(gameObject);
        }

        if(canShoot == true)
        {
            StartCoroutine(shoot());
        }
    }

    public void Damage(float damageAmount)
    {
        life -= damageAmount;
    }

    private IEnumerator shoot()
    {
        canShoot = false;
        bullet1 = Instantiate(bulletPrefab, gun1.position, gun1.rotation);
        bullet1.GetComponent<Bullet>().setTag(gameObject.tag);
        bullet1.GetComponent<Bullet>().setDamage(damage);

        bullet2 = Instantiate(bulletPrefab, gun2.position, gun2.rotation);
        bullet2.GetComponent<Bullet>().setTag(gameObject.tag);
        bullet2.GetComponent<Bullet>().setDamage(damage);

        yield return new WaitForSeconds(cooldown);
        canShoot = true;
    }
}