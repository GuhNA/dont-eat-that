using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Rigidbody2D rig;
    [Header("Parameters")]
    [SerializeField] private float speed;
    [SerializeField] private float runspeed;
    [SerializeField] private float radius;
    private float tempSpeed;
    private Vector2 direction_;
    private bool isRunning_;
    private bool isRolling_;
    private bool isAttacking_;
    private int handlingOBJ;
    [Header("References")]
    //[SerializeField] private GameObject shotPosition;
    //[SerializeField] private GameObject bullet;
    [SerializeField] private LayerMask enemyLayer;

    private bool isPaused;

    //Paineis e Menu
    public GameObject pausePanel;
    public string cena = "MenuManager";

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
    #endregion

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        tempSpeed = speed;
        handlingOBJ = 1;
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPaused){
            Running();
            Rolling();
            onInput();
            Axe();
        }
        

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseScreen();
        }

    }

    void PauseScreen()
    {
        if (isPaused)
        {
            isPaused = false;
            Time.timeScale = 1f;
            pausePanel.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            isPaused = true;
            Time.timeScale = 0f;
            pausePanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    private void FixedUpdate()
    {
        onMove();
    }

    #region attack

    void onAttack()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.Find("melee").position, radius, enemyLayer);

        if (hit)
        {
            hit.GetComponent<ZombieWM>().life--;
        }

    }

    void Axe()
    {
        if (handlingOBJ == 1)
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
    }

    #endregion

    #region moviment

    void onInput()
    {
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
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

    /*void Shooting()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        direction_ = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
        transform.up = direction_;

        if(Input.GetMouseButtonDown(0))
        {
            Instantiate(bullet, shotPosition.transform.position, transform.rotation);
        }

        if(Input.GetKey(KeyCode.W))
        {
            transform.Translate(0, speed * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(0, -speed * Time.deltaTime, 0);
        }
    }*/

    private void OnDrawGizmosSelected()
    {

        Gizmos.DrawWireSphere(transform.Find("melee").position, radius);
    }
}
    #endregion
