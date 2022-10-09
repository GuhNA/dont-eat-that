using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
          
        StartCoroutine(destroyBullet());
    }

    IEnumerator destroyBullet()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
