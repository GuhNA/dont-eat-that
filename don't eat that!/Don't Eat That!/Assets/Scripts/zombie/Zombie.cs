using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    [SerializeField] private int life, damage, speed, pos;
    [SerializeField] private List<Transform> sunPosition = new List<Transform>();
    private sunflora[] sun;
    private float ver;
    Animator anim;

    private void Start()
    {
        sun = GameObject.FindObjectsOfType<sunflora>();

        anim = GetComponent<Animator>();

        for (int i = 0; i < sun.Length; i++)
        {

            sunPosition.Add(sun[i].GetComponent<Transform>());

            if (i == 0)
            {
                ver = (transform.position - sun[i].GetComponent<Transform>().position).sqrMagnitude;
            }
            else if (ver > (transform.position - sun[i].GetComponent<Transform>().position).sqrMagnitude)
            {
                ver = (transform.position - sun[i].GetComponent<Transform>().position).sqrMagnitude;
                pos = i;
            }
        }

    }

    private void Update()
    {
        attack();
    }

    void attack()
    {
        if(Vector2.Distance(transform.position, sunPosition[pos].position) < 0.5)
        {
            anim.SetInteger("select", 2);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, sunPosition[pos].position, speed * Time.deltaTime);
            anim.SetInteger("select", 1);
        }
    }
    
}
