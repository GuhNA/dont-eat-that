using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn : MonoBehaviour
{
    [SerializeField] float timer;
    [SerializeField] GameObject zombie;
    float initialT;
    void Start()
    {
        initialT = timer;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer < 0)
        {
            Instantiate(zombie, transform.position, transform.rotation);
            timer = initialT;
        }

    }
}
