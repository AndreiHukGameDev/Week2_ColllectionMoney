using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsSpawn : MonoBehaviour
{
    public GameObject Coin;
    public GameObject Parent;
    public int CounterCoins; 


    private void Awake()
    {
        CreateCoins();
    }
    private void CreateCoins()
    {
        for (int i = 0; i <= CounterCoins; i++)
        {
            Instantiate(Coin, new Vector3(Random.Range(-50, 50), 0, Random.Range(-50, 50)), Coin.transform.rotation, Parent.transform );
        }

    }
}
 