using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sunflora : MonoBehaviour
{
    [SerializeField] float life;
    [SerializeField] Animator anim;


    #region encapsulamento
    public float Life
    {
        get { return life; }
        set { life = value; }
    }
    #endregion

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {

    }

    private void Update()
    {
        float timer = 0.8f;
        if(life < life/2)
        {
            anim.SetBool("broking", true);
        }
        if(life >= 0)
        {
            anim.SetTrigger("broke");
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
