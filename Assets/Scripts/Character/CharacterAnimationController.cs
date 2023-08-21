using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    Rigidbody2D rb;
    public Animator animator;
    Vector2 movement;
    string lastDir;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        idleHandler();

    }

    void idleHandler()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("run_Down") && movement.sqrMagnitude < 0.01f)
        {
            animator.SetBool("Dowm", true);
        }
    }
}
