using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Creature : MonoBehaviour
{

    public NN nn;
    public float speed = 1;
    public float sight;
    public Vector2 nearestFood;
    public Rigidbody2D rb;
    public float hunger = 10f;
    public int food;
    public Sight eyes;
    public GameObject creaturePrefab;

    float randTargTimer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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


        Vector2 v2 = transform.position;
        rb.velocity = (nearestFood - v2).normalized * speed;
        hunger -= Time.deltaTime * speed * sight;
        if ( hunger < 0 )
        {
            Destroy(gameObject);
        }
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.transform.tag == "food")
        {
            eyes.minDist = 100;
            eyes.targ = null;

            Destroy(collision.gameObject);
            food++;
            hunger += 10f;

            if (food == 3)
            {
                food = 0;
                Reproduce();
            }
        }

    }

    void Reproduce()
    {
        GameObject children = Instantiate(creaturePrefab, transform.position - new Vector3(0,0.5f,0), Quaternion.identity);
        Creature traits = children.GetComponent<Creature>();
        traits.sight = sight + Random.Range(-0.25f, 0.25f);
        traits.speed = speed + Random.Range(-0.25f, 0.25f);
    }

}
