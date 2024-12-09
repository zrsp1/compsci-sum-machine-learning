using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class TrainingManager : MonoBehaviour
{

    public List<Scenario> scenarios = new List<Scenario>();
    [SerializeField]
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

        Debug.Log("new generation");
        foreach (var survivor in survivors)
        {
            Debug.Log(survivor);
        }


        foreach (var scenario in scenarios)
        {

            try
            {
                scenario.nn.layers = survivors[UnityEngine.Random.Range(0, survivors.Count - 1)];
            }
            catch (ArgumentOutOfRangeException)
            {
                Debug.Log("error");
            }
            finally
            {
                survivors.Clear();
                scenario.Reset();
                scenario.agentScript.MutateAgent();
            }
            
        }
    }


}
