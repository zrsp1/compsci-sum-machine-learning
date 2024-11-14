using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class target : MonoBehaviour
{

    public Transform center;
    // Start is called before the first frame update
    void Start()
    {
        newPos();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void newPos()
    {
        transform.position = new Vector3(Random.Range(-8f, 8f), Random.Range(-4f, 4f)) + center.position;
    }
}
