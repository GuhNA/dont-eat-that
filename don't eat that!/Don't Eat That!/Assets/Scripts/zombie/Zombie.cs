using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int life;
    [SerializeField] private int damage;
    [SerializeField] private int speed;
    [SerializeField] private Sprite sprite;


    [Header("Others")]
    private SpriteRenderer spriteR;
    private List<Transform> sunPosition = new List<Transform>();
    private sunflora[] sun;
    [SerializeField] private List<Transform> FenceP = new List<Transform>();
    private Fence[] fences;
    [SerializeField] private float[] mag;
    Animator anim;

    public static Zombie instance;


    #region Encapsulamento
    public List<Transform> fenceP
    {
        get { return FenceP; }
        set { FenceP = value; }
    }

    #endregion

    private void Awake()
    {
        instance = this;

        sun = GameObject.FindObjectsOfType<sunflora>();
        fences = GameObject.FindObjectsOfType<Fence>();
        anim = GetComponent<Animator>();
        spriteR = GetComponent<SpriteRenderer>();

    }
    private void Start()
    {
        mag = new float[fences.Length];
        FenceP = searchDestroy(fences, ref mag);
        FenceP = listSort(mag, fenceP);
    }

    private void Update()
    {
        if(FenceP.Count > 0)
        {
            attack(FenceP);
        }
        else
        {

            //Enquanto n tem animacao iddle
            anim.SetInteger("select", 3);
            spriteR.sprite = sprite;
        }
    }

    void attack(List<Transform> list)
    {
        if(Vector2.Distance(transform.position, list[0].position) < 0.7)
        {
            anim.SetInteger("select", 2);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, list[0].position , speed * Time.deltaTime);
            /*if(Mathf.Abs(transform.position.x - list[0].position.x) > Mathf.Abs(transform.position.y - list[0].position.y))
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, list[0].position.y), speed * Time.deltaTime);
                if(Mathf.Approximately(transform.position.y, list[0].position.y))
                {
                    transform.position = Vector2.MoveTowards(transform.position, new Vector2(list[0].position.x, transform.position.y), speed * Time.deltaTime);
                }
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(list[0].position.x, transform.position.y), speed * Time.deltaTime);
                if (Mathf.Approximately(transform.position.y, list[0].position.y))
                {
                    transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, list[0].position.y), speed * Time.deltaTime);
                }
            }*/

            anim.SetInteger("select", 1);
        }

    }
    List<Transform> searchDestroy(MonoBehaviour[] script,ref float[] mag)
    {
        List<Transform> ret = new List<Transform>();

        for (int i = 0; i < script.Length; i++)
        {
            ret.Add(script[i].GetComponent<Transform>());
            mag[i] = (transform.position - script[i].GetComponent<Transform>().position).sqrMagnitude;
        }
        return (ret);
    }


    public void insertionSort (float[] mag)
    {
        int i, j;
        float key;
        for (i = 1; i < mag.Length; i++)
        {
            key = mag[i];
            j = i - 1;
            while (j >= 0 && mag[j] > key)
            {
                mag[j + 1] = mag[j];
                j = j - 1;
            }
            mag[j + 1] = key;
        }

    }

     List<Transform> listSort(float[] mag, List<Transform> ret)
    {

        insertionSort(mag);
        Transform temp;
        for (int i = 0; i < mag.Length; i++)
        {
            for (int j = 0; j < mag.Length; j++)
            {
                if (mag[i] == (transform.position - ret[j].GetComponent<Transform>().position).sqrMagnitude)
                {
                    temp = ret[i];
                    ret[i] = ret[j];
                    ret[j] = temp;
                    break;
                }
            }
        }

        return (ret);
    }

}
