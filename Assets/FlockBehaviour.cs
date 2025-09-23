using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using UnityEngine;

public abstract class FlockBehaviour : ScriptableObject
{
    public abstract Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock);
}
