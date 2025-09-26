using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Flock/Behaviour/Alignment")]
public class AlignmentBehaviour : FlockBehaviour
{
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        //if no neighbours, maintain current alignment
        if (context.Count == 0)
        {
            return agent.transform.up;
        }

        // adding all point together and average
        Vector2 alignmentMove = Vector2.zero;

        // Foreach neighbour add to the alignment move
        foreach (Transform t in context)
        {
            alignmentMove += (Vector2)t.transform.up;
        }
        // Getting the average direction
        alignmentMove /= context.Count;

        return alignmentMove;

    }
}
