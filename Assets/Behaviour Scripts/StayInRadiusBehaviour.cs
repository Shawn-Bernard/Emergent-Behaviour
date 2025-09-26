using System.Collections.Generic;
using UnityEngine;
//This give u an option in the asset menu to make the scriptable object
[CreateAssetMenu(menuName = "Flock/Behaviour/Stay In Radius")]
public class StayInRadiusBehaviour : FlockBehaviour
{
    public Vector2 center;
    public float radius;

    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        Vector2 centerOffset = center - (Vector2)agent.transform.position;

        // If t is 0 were in the center, if it 1 were in the radius, if bigger then 1 out of radius
        float t = centerOffset.magnitude / radius;
        if (t < 0.9f)
        {
            return Vector2.zero;
        }

        return centerOffset * t * t;

    }
}
