using System.Collections.Generic;
using UnityEngine;
//This give u an option in the asset menu to make the scriptable object
[CreateAssetMenu(menuName = "Flock/Behaviour/Avoidance")]
public class AvoidanceBehaviour : FlockBehaviour
{
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        //if no neighbours, return no adjustments
        if (context.Count == 0)
        {
            return Vector2.zero;
        }

        // adding all point together and average 
        Vector2 avoidanceMove = Vector2.zero;

        int avoidanceCount = 0;

        // Tracking how many neighbors are too close
        foreach (Transform transform in context)
        {
            // If neighbours are within distance
            if(Vector2.SqrMagnitude(transform.position - agent.transform.position) < flock.SqaureAvoidRadius)
            {
                avoidanceCount++; // Counts how many neighbors to avoid

                // Adding a vector pointing away from the neighbour
                avoidanceMove += (Vector2)(agent.transform.position - transform.position);
            }
            
        }
        // getting the average if theres neighbours  
        if (avoidanceCount > 0)
            avoidanceMove /= avoidanceCount;

        return avoidanceMove;


    }
}
