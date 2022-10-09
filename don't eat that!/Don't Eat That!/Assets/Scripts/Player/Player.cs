using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rig;
    [Header("Parameters")]
    [SerializeField] private float speed;
    [SerializeField] private float runspeed;
    [SerializeField] private float radius;
    private bool lookingRight;
    private float tempSpeed;
    private Vector2 direction_;
    private bool isRunning_;
    private bool isRolling_;
    private bool isAttacking_;
    private int handlingOBJ_;
    [Header("References")]
    [SerializeField] private GameObject shotPosition;
    [SerializeField] private GameObject bullet;
    [SerializeField] private LayerMask enemyLayer;

    #region encapsulamento
    public Vector2 direction
    {
        get { return direction_; }
        set { direction_ = value; }
    }

    public bool isRunning
    {
        get { return isRunning_; }
        set { isRunning_ = value; }
    }

    public bool isRolling
    {
        get { return isRolling_; }
        set { isRolling_ = value; }
    }

    public bool isAttacking
    {
        get { return isAttacking_; }
        set { isAttacking_ = value; }
    }
    public int handlingOBJ
    {
        get { return handlingOBJ_; }
        set { handlingOBJ_ = value; }
    }
    #endregion

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        tempSpeed = speed;
        handlingOBJ = 1;
        lookingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        Running();
        Rolling();
        onInput();
        toolSelect();
        //Axe();

    }

    private void FixedUpdate()
    {
        onMove();
    }

    #region attack

    void onAttack()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.Find("melee").position, radius, enemyLayer);

        if (hits != null)
        {
            foreach(Collider2D hit in hits)
            {
                hit.GetComponent<ZombieWM>().life--;
            }
        }

    }

    void Axe()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isAttacking_ = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isAttacking_ = false;
        }
    }

    #endregion

    #region moviment

    void onInput()
    {
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if((direction.x < 0 && lookingRight) || (direction.x > 0 && !lookingRight))
        {
            lookingRight = !lookingRight;
        }
    }

    void onMove()
    {
        rig.MovePosition(rig.position + direction_ * speed * Time.fixedDeltaTime);
    }

    void Running()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = runspeed;
            isRunning_ = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = tempSpeed;
            isRunning_ = false;
        }
    }

    void Rolling()
    {
        if (Input.GetMouseButtonDown(1))
        {
            isRolling_ = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            isRolling_ = false;
        }
    }

    void Shooting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject projectile = Instantiate(bullet, shotPosition.transform.position, transform.rotation);

            if(lookingRight)
            {
                projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(10, 0);
            }
            else
            {
                projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(-10, 0);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {

        Gizmos.DrawWireSphere(transform.Find("melee").position, radius);
    }

    void toolSelect()
    {
        for (int i = 0; i < 9; i++)
        {
            //lembrando q cada letra é um int então sabendo q alpha 0
            //é 48, 48+1 = 49 resultando em Alpha1
            if (Input.GetKeyDown(KeyCode.Alpha0 + i))
            {
                {
                    handlingOBJ = i;

                }
            }
        }

        switch (handlingOBJ)
        {
            case 1:
                Axe();
                break;
            default:
                Shooting();
                break;
        }
    }
}
    #endregion
