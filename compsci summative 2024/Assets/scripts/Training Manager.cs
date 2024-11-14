using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingManager : MonoBehaviour
{

    public List<Scenario> scenarios = new List<Scenario>();
    public List<NN.Layer[]> survivors = new List<NN.Layer[]>();

    float time = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > 10)
        {
            time = 0f;
            newGeneration();
        }
    }

    void newGeneration()
    {
        foreach (var scenario in scenarios)
        {
            try
            {
                scenario.nn.layers = survivors[UnityEngine.Random.Range(0, survivors.Count - 1)];
            }
            catch (ArgumentOutOfRangeException)
            {

            }
            finally
            {
                scenario.Reset();
                scenario.agentScript.MutateAgent();
            }
            
        }
    }


}
