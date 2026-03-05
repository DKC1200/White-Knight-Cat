using System.Collections;
using UnityEngine;

public class Enemy3 : MonoBehaviour, Idamageable
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform gun;
    [SerializeField] private Transform player;
    [SerializeField] private LayerMask layer;
    [SerializeField] private float life;
    [SerializeField] private float cooldown;
    [SerializeField] private float damage;
    [SerializeField] private float spd;
    [SerializeField] private float rayLength = 2f;
    [SerializeField] private float avoidStrength = 3f;
    [SerializeField] private int rayCount = 16;
    [SerializeField] private Collider2D collider;

    private float rot;
    private float side;
    private Vector2 moveDir;
    private GameObject bullet;
    private bool canShoot = true;

    [SerializeField] private AudioSource die_source;
    [SerializeField] private AudioSource laser_source;
    [SerializeField] private AudioClip die_sound;
    [SerializeField] private AudioClip laser;

    [SerializeField] private Animator anim;

    void Update()
    {
        if (life <= 0)
        {
            anim.SetBool("Dead", true);
            StartCoroutine(Die());
            return;
        }

        if (canShoot){
            laser_source.PlayOneShot(laser, 1f);
            StartCoroutine(shoot());
        }

        Vector2 toPlayer = (player.position - transform.position).normalized;
        rot = Mathf.Atan2(toPlayer.x, toPlayer.y) * Mathf.Rad2Deg;

        side = toPlayer.x > 0 ? 0 : 180;
        transform.rotation = Quaternion.Euler(0, side, 0);

        // Start with direction toward player
        Vector2 desiredDir = toPlayer;

        // Cast rays in a full circle and accumulate avoidance forces
        Vector2 avoidance = Vector2.zero;
        float angleStep = 360f / rayCount;

        for (int i = 0; i < rayCount; i++)
        {
            float angle = i * angleStep;
            Vector2 rayDir = Rotate(Vector2.right, angle);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDir, rayLength, layer);

            Debug.DrawRay(transform.position, rayDir * rayLength, hit.collider != null ? Color.red : Color.green);

            if (hit.collider != null)
            {
                // The closer the wall, the stronger the push
                float proximity = 1f - (hit.distance / rayLength);
                avoidance += hit.normal * proximity;
            }
        }

        // Blend player direction with avoidance
        desiredDir = (toPlayer + avoidance * avoidStrength).normalized;

        if (Vector2.Distance(transform.position, player.position) < 2f)
            rb.velocity = Vector2.zero;
        else
            rb.velocity = desiredDir * spd;
    }

    private Vector2 Rotate(Vector2 v, float degrees)
    {
        float rad = degrees * Mathf.Deg2Rad;
        float cos = Mathf.Cos(rad);
        float sin = Mathf.Sin(rad);
        return new Vector2(cos * v.x - sin * v.y, sin * v.x + cos * v.y);
    }

    public void Damage(float damageAmount)
    {
        life -= damageAmount;
        }

    private IEnumerator shoot()
    {
        canShoot = false;
        bullet = Instantiate(bulletPrefab, gun.position, Quaternion.Euler(0, 0, -rot));
        bullet.GetComponent<Bullet>().setTag(gameObject.tag);
        bullet.GetComponent<Bullet>().setDamage(damage);

        yield return new WaitForSeconds(cooldown);
        canShoot = true;
    }

    private IEnumerator Die(){
        die_source.PlayOneShot(die_sound, 1f);
        collider.enabled = false;
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}