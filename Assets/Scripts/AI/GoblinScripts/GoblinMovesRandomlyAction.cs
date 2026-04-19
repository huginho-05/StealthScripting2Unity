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
    
    private NavMeshAgent _agent;
    private Vector3 _targetPosition;

    protected override Status OnStart()
    {
        _agent = Self.Value.GetComponent<NavMeshAgent>();
        if (_agent == null) return Status.Failure;

        // Calculamos un punto aleatorio dentro del radio
        _targetPosition = GetRandomPoint(Self.Value.transform.position, Radius.Value);
        
        _agent.SetDestination(_targetPosition);
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (_agent == null) return Status.Failure;

        // Si el agente aún está calculando el camino
        if (_agent.pathPending) return Status.Running;

        // Si el agente ha llegado cerca del destino, terminamos con éxito
        if (_agent.remainingDistance <= _agent.stoppingDistance)
        {
            return Status.Success;
        }

        return Status.Running;
    }

    private Vector3 GetRandomPoint(Vector3 center, float range)
    {
        // Generamos un punto aleatorio en una esfera y lo proyectamos al NavMesh
        Vector3 randomPoint = center + UnityEngine.Random.insideUnitSphere * range;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, range, NavMesh.AllAreas))
        {
            return hit.position;
        }
        return center; // Si falla, se queda donde está
    }
}

