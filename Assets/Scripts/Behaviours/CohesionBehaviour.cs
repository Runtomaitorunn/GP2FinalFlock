using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Cohesion")]
public class CohesionBehaviour : FilteredFlockBehaviour
{
    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        // If no neighbors return no adjustment
        if(context.Count == 0)
            return Vector3.zero;

        // Add all points together and average
        Vector3 cohesionMove = Vector3.zero;

        // Check if the context on transforms is null, if not, pass in the agent and the filter
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);

        // Cohesion move
        foreach (Transform item in filteredContext)
        {
            cohesionMove += item.position;  
        }
        cohesionMove /= context.Count;

        // Offset from each agent itself
        cohesionMove -= agent.transform.position;
        return cohesionMove;
    }
}
