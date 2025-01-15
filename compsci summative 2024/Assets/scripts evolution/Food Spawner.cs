using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{

    public GameObject food;
    float cd;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cd += Time.deltaTime;
        if (cd > 0.25f)
        {
            SpawnFood();
            cd = 0;
        }
    }

    void SpawnFood()
    {
        Instantiate(food, new Vector3(Random.Range(-40,40),Random.Range(-40,40),0), Quaternion.identity);
    }
}
