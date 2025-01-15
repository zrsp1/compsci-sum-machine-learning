using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Creature : MonoBehaviour
{

    public NN nn;
    public float speed = 1;
    public float sight;
    public Vector2 nearestFood;
    public Rigidbody2D rb;
    public float hunger = 100f;
    public float food;
    public int raynum;
    public float size;
    public GameObject creaturePrefab;

    float randTargTimer;
    // Start is called before the first frame update
    void Start()
    {
        raynum = nn.networkShape[0]/2;

        transform.localScale = new Vector2(size,size);

        //Debug.Log(nn.layers[0].biasesArray[0]);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (eyes.targ)
        {
            nearestFood = eyes.targ.position;
        }
        else if( randTargTimer > 5)
        {
            nearestFood = transform.position + new Vector3(Random.Range(-10, 10), Random.Range(-10, 10));
            randTargTimer = 0;
        }

        randTargTimer += Time.deltaTime;
        */


        //vision
        ///////////////////////////////////////////////

        List<float> eyes = new List<float>();


        for (int i = 0; i < raynum; i++)
        {

            //raycasts circularly around the agent.
            //the origin is offset by new Vector3(Mathf.Cos((2 * Mathf.PI) / raynum * i) * 0.6f, Mathf.Sin((2 * Mathf.PI) / raynum * i) * 0.6f) as to move it outside its own collider so it can only see other creatures and not itself
            RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(Mathf.Cos((2 * Mathf.PI) / raynum * i) * 0.6f, Mathf.Sin((2 * Mathf.PI) / raynum * i) * 0.6f)*size, new Vector3(Mathf.Cos((2 * Mathf.PI) / raynum * i)*10, Mathf.Sin((2 * Mathf.PI) / raynum * i)*10), 7.5f);
            Color lineCol = Color.white;
            if (hit)
            {
                lineCol = Color.green;

            }
            Debug.DrawLine(transform.position + new Vector3(Mathf.Cos((2 * Mathf.PI) / raynum * i) * 0.5f, Mathf.Sin((2 * Mathf.PI) / raynum * i) * 0.5f)*size, new Vector3(Mathf.Cos((2 * Mathf.PI) / raynum * i)*7.5f, Mathf.Sin((2 * Mathf.PI) / raynum * i) * 7.5f) + transform.position, lineCol);

            
            if(hit != false)
            {
                eyes.Add(hit.distance);
                eyes.Add(hit.transform.localScale.x);
                //Debug.Log(hit.transform.localScale.x);
                //eyes.Add(1f);

            }
            else
            {
                eyes.Add(100f);
                eyes.Add(0);
                //eyes.Add(0f);
            }

        }


        ////////////////////////////////////////


        //neural network
        //////////////////////////////////////////

        float[] nnInput = {transform.position.x, transform.position.y, nearestFood.x, nearestFood.y};
        //Debug.Log(eyes.Count);
        float[] nnOutput = nn.Brain(eyes.ToArray()) ;

        /////////////////////////////////////////////////////
 




        //movement
        ///////////////////////////////////////////////////////////////////

        Vector2 v2 = transform.position;
        rb.velocity = (new Vector2(nnOutput[0], nnOutput[1])).normalized * speed;

        //////////////////////////////////////////////////////////////////
        ///


        //hunger drain
        /////////////////////////////////////////////////


        hunger -= (Time.deltaTime + (Time.deltaTime * speed) + (Time.deltaTime * sight)) * size;
        if (hunger < 0)
        {
            Destroy(gameObject);
        }



        //constrains the creature by pac-manning them to the other side of the map
        if (transform.position.x > 40)
        {
            transform.position = new Vector3(-40,transform.position.y);
        }
        if (transform.position.x < -40)
        {
            transform.position = new Vector3(40, transform.position.y);
        }
        if (transform.position.y > 40)
        {
            transform.position = new Vector3(transform.position.x, -40);
        }
        if (transform.position.y < -40)
        {
            transform.position = new Vector3(transform.position.x, 40);
        }
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.transform.tag == "food")
        {

            eat(collision.gameObject);

        }

        if (collision.transform.tag == "creature")
        {
            Debug.Log("creature");
            
            if (collision.transform.localScale.x < transform.localScale.x*2/3)
            {
                eat(collision.gameObject);
            }

        }

    }

    void eat(GameObject target)
    {
        Destroy(target);
        food += target.transform.localScale.x *2;
        hunger += 30f * target.transform.localScale.x * 2;

        if (food >= 3 * size)
        {
            food = 0;
            Reproduce();
        }
    }

    void Reproduce()
    {
        GameObject children = Instantiate(creaturePrefab, transform.position - new Vector3(0,0.5f,0), Quaternion.identity);
        Creature traits = children.GetComponent<Creature>();
        //traits.sight = sight + Random.Range(-0.25f, 0.25f);
        //traits.speed = speed + Random.Range(-0.25f, 0.25f);
        traits.size = size + Random.Range(-0.25f, 0.25f);

        if (traits.size <= 0)
        {
            traits.size = 0.2f;
        }

        traits.hunger = 100;
        traits.nn.MutateNetwork(0.8f, 0.2f);
        
    }


}
