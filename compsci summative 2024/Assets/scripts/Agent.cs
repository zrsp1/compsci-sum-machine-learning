using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    [SerializeField]
    NN nn;
    public Rigidbody2D rb;
    public Transform target;
    public float mutationAmount = 0.8f;
    public float mutationChance = 0.2f;

    [SerializeField]
    Scenario scenario;

    // Start is called before the first frame update
    void Start()
    {
        MutateAgent();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        float[] nnInput = { transform.position.x, transform.position.y, target.position.x, target.position.y };
        float[] nnOutput = nn.Brain(nnInput);

        rb.AddForce(new Vector2(nnOutput[0], nnOutput[1]));
        //controller.Move(new Vector3(nnOutput[0], nnOutput[1]));

        //when the goal has been reached
        if (Vector2.Distance(transform.position, target.position) < 1f)
        {
            scenario.trainingManager.survivors.Add(nn.copyLayers());
            GetComponent<SpriteRenderer>().color = Color.green;
            Debug.Log("success");
        }
    }

    public void MutateAgent()
    {

        mutationAmount += Random.Range(-1.0f, 1.0f) / 100;
        mutationChance += Random.Range(-1.0f, 1.0f) / 100;

        //make sure mutation amount and chance are positive using max function
        mutationAmount = Mathf.Max(mutationAmount, 0);
        mutationChance = Mathf.Max(mutationChance, 0);

        nn.MutateNetwork(mutationAmount, mutationChance);
    }
}
