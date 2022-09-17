using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Fence : MonoBehaviour
{
    [SerializeField] float life;
    SpriteRenderer spriteRenderer;
    [SerializeField] Sprite bFence;
    NavMeshModifier mod;
    [SerializeField] GameController controller;
    float iniLife;

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

    private void Start()
    {
        iniLife = life;
    }

    private void Awake()
    {
        instance = this;
        spriteRenderer = GetComponent<SpriteRenderer>();
        mod = GetComponent<NavMeshModifier>();
    }

    private void Update()
    {
        if (life <= (iniLife/3))
        {
            spriteRenderer.sprite = bFence;
        }
        if(life <= 0)
        {
            controller.fences.Remove(this);
            Destroy(gameObject);
        }
    }
}


