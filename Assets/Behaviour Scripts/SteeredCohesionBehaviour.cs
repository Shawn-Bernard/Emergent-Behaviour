using System.Collections.Generic;
using UnityEngine;
//This give u an option in the asset menu to make the scriptable object
[CreateAssetMenu(menuName = "Flock/Behaviour/Steered Cohesion")]
public class SteeredCohesionBehaviour : FlockBehaviour
{
    Vector2 currentVelocity;
    public float agentSmoothTime = 0.5f;
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        //if no neighbours, maintain current alignment
        if (context.Count == 0)
        {
            return Vector2.zero;
        }

        // adding all point together and average 
        Vector2 cohesionMove = Vector2.zero;
        foreach (Transform item in context)
        {
            // adding up all neighbours position 
            cohesionMove += (Vector2)item.position;
        }
        // Getting the average position 
        cohesionMove /= context.Count;

        // Offsetting from agent position
        cohesionMove -= (Vector2)agent.transform.position;
        //Smoothing the move over time 
        cohesionMove = Vector2.SmoothDamp(
            agent.transform.up, 
            cohesionMove,
            ref currentVelocity,// Tracks velocity for smoothing
            agentSmoothTime);

        return cohesionMove;

    }
}
