using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrashAll : MonoBehaviour
{
    private Collider colli;
    public GameObject fire;
    [SerializeField]private int collisionCount = 5;
    // Start is called before the first frame update
    void Start()
    {
        colli = GetComponent<Collider>();
    }
    private void FixedUpdate()
    {
        if(collisionCount >= 10)
            Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("some collider has enter");


        Vector3 firePosition = other.ClosestPointOnBounds(transform.position);

        // Instantiate particles
        Instantiate(fire, firePosition,Quaternion.identity);

        // play sounds

        //Count the collision
        collisionCount++;
    }
}
