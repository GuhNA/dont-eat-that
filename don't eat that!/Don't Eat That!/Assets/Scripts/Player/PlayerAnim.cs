using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    private Animator animator;
    private Player player;
    
    void Start()
    {
        player = GetComponent<Player>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        OnMove();
        OnRun();
    }

    void OnMove()
    {
        if (player.direction.sqrMagnitude > 0)
        {
            if(player.isRolling)
            {
                animator.SetTrigger("roll");
            }
            else
            {
                animator.SetInteger("action", 1);
            }
        }
        else
        {
            animator.SetInteger("action", 0);
        }

        if (player.direction.x > 0)
        {
            transform.eulerAngles = new Vector2(0, 0);
        }
        if (player.direction.x < 0)
        {
            transform.eulerAngles = new Vector2(0, 180);
        }
    }

    void OnRun()
    {
        if (player.isRunning)
        {
            animator.SetInteger("action", 2);
        }
    }


}
