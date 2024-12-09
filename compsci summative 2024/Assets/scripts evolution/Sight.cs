using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sight : MonoBehaviour
{

    public Creature creature;
    public CircleCollider2D circleCollider;
    public Transform targ;

    //set mindist to an arbitrarily large number such that it will be greater than any possible distance between food
    public float minDist = 100;
    // Start is called before the first frame update
    void Start()
    {
        circleCollider.radius = creature.sight*5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (targ == null)
        {
            minDist = 100;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "food")
        {
            float d = Vector2.Distance(transform.position, collision.transform.position);
            if (minDist > d)
            {
                targ = collision.transform;
                minDist = d;
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }
}
