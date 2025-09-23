using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{

    public FlockAgent agentPrefab;
    
    List<FlockAgent> agents = new List<FlockAgent>();

    public FlockBehaviour behaviour;

    [Range(10, 500)]
    public int agentCount = 250;

    const float AgentDensity = 0.08f;

    [Range(1f, 100f)]
    public float Speed = 10f;
    [Range(1f, 100f)]
    public float maxSpeed = 5f;
    [Range(1f, 10f)]
    public float neighbourRadius = 1.5f;
    [Range(0f, 1f)]
    public float avoidRadiusMultiplier = 0.5f;

    float sqaureMaxSpeed;
    float sqaureNeighbourRadius;
    float sqaureAvoidRadius;

    public float SqaureAvoidRadius { get { return sqaureAvoidRadius; } }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sqaureMaxSpeed = maxSpeed * maxSpeed;
        sqaureNeighbourRadius = neighbourRadius * neighbourRadius;
        sqaureAvoidRadius = sqaureAvoidRadius * avoidRadiusMultiplier * avoidRadiusMultiplier;

        for (int i = 0; i < agentCount; i++)
        {
            FlockAgent newAgent = Instantiate(agentPrefab, Random.insideUnitCircle * agentCount * AgentDensity, Quaternion.identity,transform);
            newAgent.name = "Agent " + i;
            agents.Add(newAgent);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach(FlockAgent agent in agents)
        {
            List<Transform> context = GetNearbyObjects(agent);
            Vector2 moveSpeed = behaviour.CalculateMove(agent, context,this);
            moveSpeed *= Speed;
            if (moveSpeed.sqrMagnitude > sqaureMaxSpeed)
            {
                moveSpeed = moveSpeed.normalized * maxSpeed;
            }
            agent.Move(moveSpeed);
        }
    }

    List<Transform> GetNearbyObjects(FlockAgent agent)
    {
        List<Transform> context = new List<Transform>();
        // Getting a list of agents from agent position with the neighbours radius
        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(agent.transform.position, neighbourRadius);

        foreach(Collider2D c in contextColliders)
        {
            if (c != agent.AgentCollider)
            {
                context.Add(c.transform);
            }
        }

        return context;
    }
}
