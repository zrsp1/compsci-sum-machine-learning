using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creatureSpawner : MonoBehaviour
{

    public GameObject creaturePrefab;
    float cd = 20;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cd += Time.deltaTime;
        if (cd > 20)
        {
            GameObject creature  = Instantiate(creaturePrefab);
            creature.GetComponent<NN>().MutateNetwork(1, 1);
            cd = 0;
        }
    }

    [ContextMenu("Spawn")]
    void Spawn()
    {
        GameObject creature = Instantiate(creaturePrefab);
        creature.GetComponent<NN>().MutateNetwork(1, 1);
    }
}
