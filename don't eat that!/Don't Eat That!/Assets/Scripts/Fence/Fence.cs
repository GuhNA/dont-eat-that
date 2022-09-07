using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Fence : MonoBehaviour
{
    [SerializeField] float life;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite bFence;
    NavMeshModifier mod;

    public static Fence instance;

    public float Life
    {
        get { return life; }
        set { life = value; }
    }

    public NavMeshModifier Mod
    {
        get { return mod; }
        set { mod = value; }
    }


    private void Awake()
    {
        instance = this;
        spriteRenderer = GetComponent<SpriteRenderer>();
        mod = GetComponent<NavMeshModifier>();
    }

    private void Start()
    {

    }

    private void Update()
    {
        if (life < life/4)
        {
            spriteRenderer.sprite = bFence;
        }
        if(life <= 0)
        {
            Destroy(gameObject);
        }
    }
}


