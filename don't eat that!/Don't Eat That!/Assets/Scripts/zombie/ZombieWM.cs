using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieWM : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int life;
    [SerializeField] private int dam;
    [SerializeField] private float radius;

    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private NavMeshSurface2d surf;
    [SerializeField] List<Fence> fences = new List<Fence>();
    [SerializeField] List<Sunflora> sum = new List<Sunflora>();
    [SerializeField] bool dmg = false;
    private Animator anim;
    int cont;
    int cont2;
    [Header("position change")]
    [SerializeField] int pos;
    [SerializeField] int pos2;
    bool mov;
    bool once;
    bool onceA;
    bool end;
    [SerializeField]LayerMask playerMask;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        end = true;
        once = false;
        searchDestroy();
        searchSunflora();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

    }

    private void Update()
    {
        if(end)
        {
            if (mov)
            {
                anim.SetInteger("select", 1);
            }

            if (fences.Count > cont - 4)
            {
                if (fences[pos] == null)
                {
                    fences.Remove(fences[pos]);
                    searchDestroy();
                }

                agent.SetDestination(fences[pos].transform.position);
            }
            else
            {
               
                if (!once)
                {  
                    //ativando as fences como obstaculos
                    for (int i = 0; i < fences.Count; i++)
                    {
                        fences[i].Mod.overrideArea = true;
                    }
                    //rebuildando a NavMesh "bake"
                    surf.BuildNavMesh();

                    once = true;
                }

                Debug.Log("pos: " + sum[pos2]);
                if (sum[pos2] == null)
                {
                    Debug.Log("entrei");
                    sum.Remove(sum[pos2]);
                    searchSunflora();
                }

                if (sum.Count <=0)
                {
                    end = false;
                }

                agent.SetDestination(sum[pos2].transform.position);
            }
            direction();
            damage();

        }
        else
        {
            anim.SetInteger("select", 0);
        }
    }

     void searchDestroy()
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
                    pos = i;
                }
                else if (mag > (transform.position - GameObject.FindObjectsOfType<Fence>()[i].transform.position).sqrMagnitude)
                {
                    mag = (transform.position - GameObject.FindObjectsOfType<Fence>()[i].transform.position).sqrMagnitude;
                    pos = i;
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
                    pos = i;
                }
                else if (mag > (transform.position - fences[i].transform.position).sqrMagnitude)
                {
                    mag = (transform.position - fences[i].transform.position).sqrMagnitude;
                    pos = i;
                }
            }
        }
        mov = true;
    }

    void searchSunflora()
    {
        float mag = 0;
        if (sum.Count < 1)
        {
            cont2 = GameObject.FindObjectsOfType<Sunflora>().Length;
            for (int i = 0; i < cont2; i++)
            {
                sum.Add(GameObject.FindObjectsOfType<Sunflora>()[i]);
                if (i == 0)
                {
                    mag = (transform.position - GameObject.FindObjectsOfType<Sunflora>()[i].transform.position).sqrMagnitude;
                    pos2 = i;
                }
                else if (mag > (transform.position - GameObject.FindObjectsOfType<Sunflora>()[i].transform.position).sqrMagnitude)
                {
                    mag = (transform.position - GameObject.FindObjectsOfType<Sunflora>()[i].transform.position).sqrMagnitude;
                    pos2 = i;
                }
            }
        }
        else
        {
            for (int i = 0; i < sum.Count; i++)
            {
                if (i == 0)
                {
                    mag = (transform.position - sum[i].transform.position).sqrMagnitude;
                    pos2 = i;
                }
                else if (mag > (transform.position - sum[i].transform.position).sqrMagnitude)
                {
                    mag = (transform.position - sum[i].transform.position).sqrMagnitude;
                    pos2 = i;
                }
            }
        }
        mov = true;
    }


    void direction()
    {
        if (fences.Count > cont - 4)
        {
            if (transform.position.x < fences[pos].transform.position.x)
            {
                transform.eulerAngles = new Vector2(0, 0);
            }
            if (transform.position.x > fences[pos].transform.position.x)
            {
                transform.eulerAngles = new Vector2(0, 180);
            }
        }
        else
        {
            if (transform.position.x < sum[pos2].transform.position.x)
            {
                transform.eulerAngles = new Vector2(0, 0);
            }
            if (transform.position.x > sum[pos2].transform.position.x)
            {
                transform.eulerAngles = new Vector2(0, 180);
            }
        }
    }

    void damage()
    {
        if (fences.Count > cont - 4)
        {
            if (agent.stoppingDistance >= Vector2.Distance(fences[pos].transform.position, transform.position))
            {
                mov = false;
                anim.SetInteger("select", 2);
            }
            if (dmg)
            {
                fences[pos].Life -= Time.deltaTime;
            }
        }
        else
        {
            if (agent.stoppingDistance >= Vector2.Distance(sum[pos2].transform.position, transform.position))
            {
                mov = false;
                anim.SetInteger("select", 2);
            }
            if (dmg)
            {
                sum[pos2].Life -= Time.deltaTime;
                if(sum[pos2].Life > 0)
                {
                    sum[pos2].Anim.SetTrigger("dmg");
                }
            }
        }
    }

    void range()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, radius, playerMask);

        if(hit)
        {
            agent.SetDestination(Player.instance.transform.position);
        }
    }
}
