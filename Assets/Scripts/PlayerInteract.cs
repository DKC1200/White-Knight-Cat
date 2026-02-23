using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class PlayerInteract : MonoBehaviour, Idamageable
{
    [SerializeField] private PlayerMain main;
    [SerializeField] private Transform hand3D;
    [SerializeField] private LayerMask mask;
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private Text lifeText;
    [SerializeField] private Text enemysText;
    [SerializeField] private float weaponRange;
    [SerializeField] private float weaponDamage;
    [SerializeField] private float weaponAttackDuration;
    [SerializeField] private float weaponCooldown;
    [SerializeField] private float life;

    private Collider2D hit;
    private Vector2 hand;
    private float mouseL;
    private bool canAttack = true;

    void Start()
    {
        
    }

    void Update()
    {
        lifeText.text = life.ToString();
        enemysText.text = main.getEnemyCount().ToString();

        if(life <= 0)
        {
            main.getMove().Interrupt(true);
            anim.SetBool("Dead", true);
            gameOverScreen.SetActive(true);
        }

        mouseL = main.getInput().getMouseL();
        hand = new Vector2(hand3D.position.x, hand3D.position.y);

        if(mouseL == 1 & life > 0)
        {   
            hit = Physics2D.OverlapCircle(hand, weaponRange, mask, -Mathf.Infinity, Mathf.Infinity);

            if(canAttack == true)
            {
                StartCoroutine(attack());
            }
        }
    }  

    private IEnumerator attack()
    {
        canAttack = false;
        main.getMove().Interrupt(true);
        anim.SetBool("Scratch", true);
        if(hit != null)
        {
            hit.gameObject.GetComponent<Idamageable>()?.Damage(weaponDamage);
        }
        yield return new WaitForSeconds(weaponAttackDuration);
        main.getMove().Interrupt(false);
        anim.SetBool("Scratch", false);
        yield return new WaitForSeconds(weaponCooldown);
        canAttack = true;
    }

    public void Damage(float damageAmount)
    {
        life -= damageAmount;
    }
}