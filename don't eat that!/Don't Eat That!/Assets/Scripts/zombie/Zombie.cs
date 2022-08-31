using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    [SerializeField] private int life, damage, speed, pos;
    [SerializeField] private List<Transform> sunPosition = new List<Transform>();
    [SerializeField] private sunflora[] sun;
    [SerializeField] private float ver;
    public float[] mag;


    private void Start()
    {
        sun = GameObject.FindObjectsOfType<sunflora>();

        
        for(int i = 0; i < sun.Length; i++)
        {
            sunPosition.Add(sun[i].GetComponent<Transform>());

            mag[i] = sun[i].GetComponent<Transform>().position.sqrMagnitude;

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
        follow();
    }

    void follow()
    {
        transform.position = Vector2.MoveTowards(transform.position, sunPosition[pos].position, speed * Time.deltaTime);
    }
}
