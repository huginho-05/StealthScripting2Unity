using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine.AI;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "GoblinMovesRandomly", story: "[Self] moves randomly", category: "Action", id: "11c56300da45eac1ea8a2a6135f9f7b9")]
public partial class GoblinMovesRandomlyAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<float> Radius;
    
    private NavMeshAgent agent;
    private Vector3 targetPosition;

    protected override Status OnStart()
    {
        agent = Self.Value.GetComponent<NavMeshAgent>();
        if (agent == null) return Status.Failure;

        //Punto aleatorio dentro del radio
        targetPosition = GetRandomPoint(Self.Value.transform.position, Radius.Value);
        
        agent.SetDestination(targetPosition);
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (agent == null) return Status.Failure;

        //Aun de camino
        if (agent.pathPending) return Status.Running;

        //Goblin llega al destino
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            return Status.Success;
        }

        return Status.Running;
    }

    private Vector3 GetRandomPoint(Vector3 center, float range)
    {
        //Punto aleatorio
        Vector3 randomPoint = center + UnityEngine.Random.insideUnitSphere * range;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, range, NavMesh.AllAreas))
        {
            return hit.position;
        }
        return center; //¿Falla?, se queda donde está
    }
}

