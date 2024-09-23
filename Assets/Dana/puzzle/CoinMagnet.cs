using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CoinMagnet : MonoBehaviour
{
    public List<GameObject> coins = new List<GameObject>();
    public float magnetForce;
    public float speed;

    void Start()
    {
        foreach (var coin in GameObject.FindGameObjectsWithTag("coin"))
        {
            coins.Add(coin);
        }
    }

    void Update()
    {
        foreach(var coin in coins)
        {
            if(coin != null)
            {
                float distance = Vector2.Distance(transform.position, coin.transform.position);
                if (distance < magnetForce)
                {
                    coin.transform.position = Vector2.Lerp(coin.transform.position, transform.position, speed);
                }
            }
            
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "coin")
        {
            Destroy(collision.gameObject);
            coins.Remove(collision.gameObject);
        }
    }
}
