using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
//This give u an option in the asset menu to make the scriptable object
[CreateAssetMenu(menuName = "Flock/Behaviour/Composite")]
public class CompositeBehaviour : FlockBehaviour
{
    public FlockBehaviour[] behaviours;
    public float[] weight;
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        if (weight.Length != behaviours.Length)
        {
            Debug.LogError("Data mismatch in " + name, this);
            return Vector2.zero;
        }

        // Set up moves
        Vector2 move = Vector2.zero;

        // go through behaviours
        for (int i = 0; i < behaviours.Length; i++)
        {
            // Gets the move from beaviours and multiply by its weight
            Vector2 partialMove = behaviours[i].CalculateMove(agent,context, flock) * weight[i];

            if (partialMove != Vector2.zero)
            {
                // Clamping partial move if it's too strong based on weight
                if (partialMove.sqrMagnitude > weight[i] * weight[i])
                {
                    partialMove.Normalize();
                    partialMove *= weight[i];
                }
                // adding it to the final move
                move += partialMove;
            }
        }
        return move;
    }
}
