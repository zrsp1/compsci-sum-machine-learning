using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scenario : MonoBehaviour
{

    public GameObject agent;
    public NN nn;
    public TrainingManager trainingManager;
    public Agent agentScript;
    public target tar;
    // Start is called before the first frame update
    void Start()
    {
        trainingManager = GameObject.FindGameObjectWithTag("training manager").GetComponent<TrainingManager>();
        trainingManager.scenarios.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Reset()
    {
        agent.transform.position = transform.position;
        agent.GetComponent<SpriteRenderer>().color = Color.white;
        tar.newPos();
    }
}
