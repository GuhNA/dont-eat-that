using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sunflora : MonoBehaviour
{
    [SerializeField] float life;
    Animator anim;
    float iniLife;
    [SerializeField] bool died;
    bool once;
    [SerializeField] GameController controller;


    #region encapsulamento
    public float Life
    {
        get { return life; }
        set { life = value; }
    }

    public Animator Anim
    {
        get { return anim; }
        set { anim = value; }
    }
    #endregion

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        iniLife = life;
        once = true;
    }

    private void Update()
    {

        if (life <= 0)
        {
            teste();
            anim.SetTrigger("die");

        }
        else if (life > iniLife / 2)
        {
            anim.SetInteger("select", 0);
        }
        else if (life < iniLife / 2 && life > 0)
        {
            anim.SetInteger("select", 1);
        }
        if (died)
        {
            controller.sunfloras.Remove(this);
            Destroy(gameObject);
        }

    }

    void teste()
    {
        if (once)
        {
            anim.SetTrigger("die");
            once = false;
        }
    }
}
