using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private PlayerMain Main;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;

    private float walk;
    private float jump;

    [SerializeField] private float playerSpeed;
    [SerializeField] private Transform groundPosition;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float jumpHeight;

    private bool groundCheck;
    private float currentJumpValue;
    private float lastJumpValue;
    private bool interrupted = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        walk = Main.getInput().getWalk();
        jump = Main.getInput().getJump();

        if(interrupted == true)
        {
            rb.velocity = new Vector2(0,0);
            return;
        }

        int walkInt = (int)walk;
        anim.SetInteger("Walking", walkInt);

        rb.velocity = new Vector2((playerSpeed * walk), rb.velocity.y);

        if (walk != 0)
        {
            transform.localScale = new Vector3(walk, 1, 1);
        }

        groundCheck = Physics2D.OverlapCircle(groundPosition.position, 0.2f, groundLayer);
        anim.SetBool("Jump", !groundCheck);

        currentJumpValue = jump;

        if (currentJumpValue == 1 & lastJumpValue == 0 & groundCheck) 
        {
            rb.velocity = new Vector2 (rb.velocity.x, jumpHeight);
        }
        lastJumpValue = currentJumpValue;
    }

    public void Interrupt(bool inputBool)
    {
        interrupted = inputBool;
    }
}
