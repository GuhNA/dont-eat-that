using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rig;
    [SerializeField] private float speed;
    [SerializeField] private float runspeed;
    private float tempSpeed;
    private Vector2 direction_;
    private bool isRunning_;
    private bool isRolling_;

    public static Player instance;

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
    #endregion


    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        tempSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        onInput();
        Running();
        Rolling();
    }

    private void FixedUpdate()
    {
        onMove();
    }



    #region moviment

    void onInput()
    {
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    void onMove()
    {
        rig.MovePosition(rig.position + direction * speed * Time.fixedDeltaTime);
    }

    void Running ()
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
    #endregion
}
