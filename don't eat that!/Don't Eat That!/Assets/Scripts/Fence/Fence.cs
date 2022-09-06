using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fence : MonoBehaviour
{
    [SerializeField] float life = 1;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite bFence;
    private bool destroyed = false;

    public float Life
    {
        get { return life; }
        set { life = value; }
    }

    public bool Destroyed
    {
        get { return destroyed; }
        set { destroyed = value; }
    }


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    private void Update()
    {
        if (life < 1)
        {
            spriteRenderer.sprite = bFence;
        }
        if (life <= 0)
        {
            //destroyed = true;
            Destroy(gameObject);
        }
    }
}


