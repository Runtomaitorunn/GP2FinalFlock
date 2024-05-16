using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public FlockAgent agentPrefab;
    List<FlockAgent> agents = new List<FlockAgent>();
    public FlockBehaviour behaviour;

    [Header("FlockGroup")]
    [Range(10, 500)]
    public int startingCount = 100;
    const float AgentDensity = 0.05f;
    [Range(1f, 100f)]
    public float driveFactor = 10f;
    [Range(1f, 100f)]
    public float maxSpeed = 5f;

    [Header("Neighbors")]
    [Range(1f, 10f)]
    public float neighborRadius = 5f;
    [Range(0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.5f;

    [Header("AccessiveMath")]
    float sqrMaxSpeed;
    float sqrNeighborRadius;
    float sqrAvoidanceRadius;

    [Header("StayInDuration")]
    [SerializeField]private float destroyDuration = 25f;
    public float SqrAvoidanceRadius { get { return sqrAvoidanceRadius; } }


    void Start()
    {
        // To simplify further math
        sqrMaxSpeed = maxSpeed * maxSpeed;
        sqrNeighborRadius = neighborRadius * neighborRadius;
        sqrAvoidanceRadius = sqrNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

        // Properties in each flockAgent
        for ( int i = 0; i < startingCount; i++ )
        {
            FlockAgent newAgent = Instantiate(
                agentPrefab,
                Random.insideUnitSphere * startingCount * AgentDensity,// the size of the circle is based on the number and density of our flock
                Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f)),
                transform// set the parent of all agents
                );
            newAgent.name = "Agent" + i;
            newAgent.Initialize(this);
            agents.Add(newAgent);

        }

        //Self destroy after a time
        StartCoroutine(DestroyAfterDuration());
    }

    void Update()
    {
        foreach (FlockAgent agent in agents)
        {
            List<Transform> context = GetNearbyObjects(agent);

            // Move each flock agent in behaviour ways
            Vector3 move = behaviour.CalculateMove(agent, context, this);
            move *= driveFactor;
            if (move.sqrMagnitude > sqrMaxSpeed)
            {
                move = move.normalized * maxSpeed;
            }
            agent.Move(move);
        }
    }

    List<Transform> GetNearbyObjects(FlockAgent agent)
    {
        List<Transform> context = new List<Transform>();
        Collider[] contextColliders = Physics.OverlapSphere(agent.transform.position, neighborRadius);
        foreach (Collider c in contextColliders)
        {
            if (c != agent.AgentCollider)
            {
                context.Add(c.transform);
            }
        }
        return context;
    }

    IEnumerator DestroyAfterDuration()
    {
        yield return new WaitForSeconds(destroyDuration);

        // Destroy it from the list
        FlocksData.flockList.Remove(gameObject);

        // Destroy the gameobject
        Destroy(gameObject);
    }


}
