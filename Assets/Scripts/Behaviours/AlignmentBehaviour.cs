using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Alignment")]
public class AlignmentBehaviour : FilteredFlockBehaviour
{
    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        // If no neighbors, maintain current alignment
        if (context.Count == 0)
            return agent.transform.forward;

        // Add all points together and average
        Vector3 alignmentMove = Vector3.zero;
        
        // Check if the context on transforms is null, if not, pass in the agent and the filter
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);

        // Alignment
        foreach (Transform item in filteredContext)
        {
            alignmentMove += item.transform.forward;
        }
        alignmentMove /= context.Count;

        return alignmentMove;
    }


}
