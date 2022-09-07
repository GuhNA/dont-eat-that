using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sunflora : MonoBehaviour
{
    [SerializeField] float life;
    [SerializeField] Animator anim;
    float iniLife;
    [SerializeField] bool died;
    bool once;


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
            //anim.SetInteger("select",3);
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
            Destroy(gameObject);
        }

    }

    void teste()
    {
        if (once)
        {
            Debug.Log("a");
            anim.SetTrigger("die");
            once = false;
        }
    }
}
