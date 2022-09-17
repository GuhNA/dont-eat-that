using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{ /*
    [Header("Attributes")]
    [SerializeField] private int life;
    [SerializeField] private int damage;
    [SerializeField] private int speed;
    [SerializeField] private Sprite sprite;
    [SerializeField] private float radius;
    [SerializeField] private bool hit;

    float BPosition;


    [Header("Others")]
    private SpriteRenderer spriteR;
    private List<Transform> sunPosition = new List<Transform>();
    private sunflora[] sun;
    [SerializeField] private List<Fence> fences = new List<Fence>();
    [SerializeField] LayerMask mask;
    [SerializeField] private int Pos;
    private int cont = 0;
    private Animator ZombieAnim;
    private bool Walk;

    public static Zombie instance;


    #region Encapsulamento

    public bool walk
    {
        get { return Walk; }
        set { Walk = value; }
    }

    public Animator zombieAnim
    {
        get { return ZombieAnim; }
        set { ZombieAnim = value; }
    }

    public float bPosition
    {
        get { return BPosition; }
        set { BPosition = value; }
    }

    public int pos
    {
        get { return Pos; }
        set { Pos = value; }
    }
    #endregion

    private void Awake()
    {
        instance = this;

        sun = GameObject.FindObjectsOfType<sunflora>();
        
        ZombieAnim = GetComponent<Animator>();
        spriteR = GetComponent<SpriteRenderer>();

    }
    private void Start()
    {
       searchDestroy();
    }

    private void Update()
    {
        direction();

        if(fences.Count > 0)
        {
            attack(mask);
            moviment(fences);
        }
        else
        {
            //Enquanto n tem animacao iddle
            ZombieAnim.SetInteger("select", 3);
            spriteR.sprite = sprite;
        }
    }

    void moviment(List<Fence> script)
    {
        if (Vector2.Distance(transform.position, script[pos].transform.position) > radius)
        {
            transform.position = Vector2.MoveTowards(transform.position, script[pos].transform.position, speed * Time.deltaTime);

            ZombieAnim.SetInteger("select", 1);
        }
        else { }
        
    }

    public void searchDestroy()
    {
        float mag = 0;
        if (cont < 1)
        {
            cont = GameObject.FindObjectsOfType<Fence>().Length;
            for (int i = 0; i < cont; i++)
            {
                fences.Add(GameObject.FindObjectsOfType<Fence>()[i]);
                if (i == 0)
                {
                    mag = (transform.position - GameObject.FindObjectsOfType<Fence>()[i].transform.position).sqrMagnitude;
                    Pos = i;
                }
                else if (mag > (transform.position - GameObject.FindObjectsOfType<Fence>()[i].transform.position).sqrMagnitude)
                {
                    mag = (transform.position - GameObject.FindObjectsOfType<Fence>()[i].transform.position).sqrMagnitude;
                    Pos = i;
                }
            }
        }
        else
        {
            for (int i = 0; i < fences.Count; i++)
            {
                if (i == 0)
                {
                    mag = (transform.position - fences[i].transform.position).sqrMagnitude;
                    Pos = i;
                }
                else if (mag > (transform.position - fences[i].transform.position).sqrMagnitude)
                {
                    mag = (transform.position - fences[i].transform.position).sqrMagnitude;
                    Pos = i;
                }
            }
        }
        bPosition = fences[pos].transform.position.x;
    }
    void direction()
    {
        if (transform.position.x < bPosition)
        {
            transform.eulerAngles = new Vector2(0, 0);
        }
        if (transform.position.x > bPosition)
        {
            transform.eulerAngles = new Vector2(0, 180);
        }
    }

    public void attack(LayerMask obj /*, LayerMask obj2)
    {
        Collider2D hitFence = Physics2D.OverlapCircle(transform.position, radius, obj);
        //Collider2D hitPlayer = Physics2D.OverlapCircle(transform.position, radius, obj2);
        if (hitFence)
        {
            zombieAnim.SetInteger("select", 2);

            if (hit)
            {
                fences[pos].Life -= Time.deltaTime;
            }
        }
        if(fences[pos] == null)
        {
            fences.Remove(fences[pos]);
            searchDestroy();
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
    */
}
