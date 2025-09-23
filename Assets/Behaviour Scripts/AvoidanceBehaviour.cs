using System.Collections.Generic;
using UnityEngine;
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
        foreach (Transform Item in context)
        {
            if(Vector2.SqrMagnitude(Item.position - agent.transform.position) < flock.SqaureAvoidRadius)
            {
                avoidanceCount++;
                avoidanceMove += (Vector2)(agent.transform.position - Item.position);
            }
            
        }
        if (avoidanceCount > 0)
            avoidanceMove /= avoidanceCount;

        return avoidanceMove;


    }
}
