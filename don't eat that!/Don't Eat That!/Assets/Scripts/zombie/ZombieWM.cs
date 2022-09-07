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
    private Animator anim;
    int cont;
    int pos;
    bool dmg = false;
    bool mov;
    bool once;
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

            if (fences.Count > cont - 3)
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
                //ativando as fences como obstaculos
                Fence.instance.Mod.overrideArea = true;

                //rebuildando a NavMesh "bake"
                surf.BuildNavMesh();

                if (!once)
                {
                    searchSunflora();
                    once = true;
                }

                if (sum[pos] == null)
                {
                    sum.Remove(sum[pos]);
                    searchSunflora();
                }
                agent.SetDestination(sum[pos].transform.position);

                if(sum.Count >=0)
                {
                    end = false;
                }
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
        if (cont < 1)
        {
            cont = GameObject.FindObjectsOfType<Sunflora>().Length;
            for (int i = 0; i < cont; i++)
            {
                sum.Add(GameObject.FindObjectsOfType<Sunflora>()[i]);
                if (i == 0)
                {
                    mag = (transform.position - GameObject.FindObjectsOfType<Sunflora>()[i].transform.position).sqrMagnitude;
                    pos = i;
                }
                else if (mag > (transform.position - GameObject.FindObjectsOfType<Sunflora>()[i].transform.position).sqrMagnitude)
                {
                    mag = (transform.position - GameObject.FindObjectsOfType<Sunflora>()[i].transform.position).sqrMagnitude;
                    pos = i;
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
                    pos = i;
                }
                else if (mag > (transform.position - sum[i].transform.position).sqrMagnitude)
                {
                    mag = (transform.position - sum[i].transform.position).sqrMagnitude;
                    pos = i;
                }
            }
        }
        mov = true;
    }


    void direction()
    {
        if (fences.Count > cont - 3)
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
            if (transform.position.x < sum[pos].transform.position.x)
            {
                transform.eulerAngles = new Vector2(0, 0);
            }
            if (transform.position.x > sum[pos].transform.position.x)
            {
                transform.eulerAngles = new Vector2(0, 180);
            }
        }
    }

    void damage()
    {
        if (fences.Count > cont - 3)
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
            if (agent.stoppingDistance >= Vector2.Distance(sum[pos].transform.position, transform.position))
            {
                mov = false;
                anim.SetInteger("select", 2);
            }

            if (dmg)
            {
                sum[pos].Life -= Time.deltaTime;
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
