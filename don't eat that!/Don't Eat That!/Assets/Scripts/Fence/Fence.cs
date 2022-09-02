using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fence : MonoBehaviour
{
    [SerializeField] int life = 4;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite bFence;
    [SerializeField] Zombie zum;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (life < 3)
        {
            spriteRenderer.sprite = bFence;
        }
        if (life == 0)
        {
            Destroy(gameObject);
            Zombie.instance.fenceP.Remove(Zombie.instance.fenceP[0]);
        }
    }
}

