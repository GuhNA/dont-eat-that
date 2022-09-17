using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Para o tiro seguir a rotacao do personagem
        transform.Translate(Vector2.up * 10 * Time.deltaTime);
        StartCoroutine(destroyBullet());
    }

    IEnumerator destroyBullet()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
