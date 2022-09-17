using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieWM : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int Life;
    [SerializeField] private float speed;

    [SerializeField] private NavMeshAgent agent;
    private GameController controller;
    [SerializeField] bool dmg = false;
    private Animator anim;
    [SerializeField] int cont;

    [Header("position change")]
    [SerializeField] int pos;
    [SerializeField] int pos2;

    #region encapsulamento
    public int life
    {
        get { return Life; }
        set { Life = value; }
    }
    #endregion

    private void Awake()
    {
        anim = GetComponent<Animator>();
        controller = FindObjectOfType<GameController>();
    }

    private void Start()
    {
        cont = controller.fences.Count;
        searchDestroy(controller.fences);
        searchSunflora(controller.sunfloras);
        agent.updateRotation = false;
        agent.updateUpAxis = false; 

    }

    private void Update()
    {

        if(controller.end)
        {
            if (agent.speed > 0)
            {
                anim.SetInteger("select", 1);
            }

            if (controller.fences.Count > cont - controller.surfEvent)
            {
                agent.speed = speed;
                searchDestroy(controller.fences);

                agent.SetDestination(controller.fences[pos].transform.position);
                direction(controller.fences[pos]);
            }
            else
            {

                agent.speed = speed;
                searchSunflora(controller.sunfloras);
                
                agent.SetDestination(controller.sunfloras[pos2].transform.position);
                direction(controller.sunfloras[pos2]);
            }
            damage();

        }

        if (controller.sunfloras.Count <= 0)
        {
            agent.speed = 0;
            anim.SetInteger("select", 0);
        }

        if(Life <= 0)
        {
            Destroy(gameObject);
        }

        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

     public void searchDestroy(List<Fence> list)
    {
        float mag = 0;
        for (int i = 0; i < list.Count; i++)
        {
            if (i == 0)
            {
                mag = (transform.position - list[i].transform.position).sqrMagnitude;
                pos = i;
            }
            else if (mag > (transform.position - list[i].transform.position).sqrMagnitude)
            {
                mag = (transform.position - list[i].transform.position).sqrMagnitude;
                pos = i;
            }
        }
    }

    void searchSunflora(List<Sunflora> list)
    {
        float mag = 0;
        for (int i = 0; i < list.Count; i++)
        {
            if (i == 0)
            {
                mag = (transform.position - list[i].transform.position).sqrMagnitude;
                pos2 = i;
            }
            else if (mag > (transform.position - list[i].transform.position).sqrMagnitude)
            {
                mag = (transform.position - list[i].transform.position).sqrMagnitude;
                pos2 = i;
            }
        }
    }


    void direction(MonoBehaviour obj)
    {
        if (transform.position.x < obj.transform.position.x)
        {
            transform.eulerAngles = new Vector2(0, 0);
        }
        if (transform.position.x > obj.transform.position.x)
        {
            transform.eulerAngles = new Vector2(0, 180);
        }
    }

    void damage()
    {
        if (controller.fences.Count > cont - controller.surfEvent)
        {
            if (agent.stoppingDistance >= Vector2.Distance(controller.fences[pos].transform.position, transform.position))
            {
                agent.speed = 0;
                anim.SetInteger("select", 2);
            }
            if (dmg)
            {
                controller.fences[pos].Life -= Time.deltaTime;
            }
        }
        else
        {
            if (agent.stoppingDistance >= Vector2.Distance(controller.sunfloras[pos2].transform.position, transform.position))
            {
                agent.speed = 0;
                anim.SetInteger("select", 2);
            }
            if (dmg)
            {
                controller.sunfloras[pos2].Life -= Time.deltaTime;
                if(controller.sunfloras[pos2].Life > 0)
                {
                    controller.sunfloras[pos2].Anim.SetTrigger("dmg");
                }
            }
        }
    }
}
