using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine.AI;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "AllieGetAwayEnemy", story: "[Self] get away [Enemy]", category: "Action", id: "6cda5b711a011d7f806314b813d7d555")]
public partial class AllieFightEnemyAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<GameObject> Enemy;
    [SerializeReference] public BlackboardVariable<float> Radius = new BlackboardVariable<float>(4f);

    private NavMeshAgent _agent;
    private bool _destinationSet = false;

    protected override Status OnStart()
    {
        if (Self.Value == null || Enemy.Value == null) return Status.Failure;

        _agent = Self.Value.GetComponent<NavMeshAgent>();
        if (_agent == null) return Status.Failure;

        //Calcular posición aleatoria alrededor del enemigo
        float angle = UnityEngine.Random.Range(0, 360) * Mathf.Deg2Rad;
        Vector3 offset = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * Radius.Value;
        Vector3 targetPos = Enemy.Value.transform.position + offset;

        //El punto está en el NavMesh
        if (NavMesh.SamplePosition(targetPos, out NavMeshHit hit, Radius.Value, NavMesh.AllAreas))
        {
            _agent.SetDestination(hit.position);
            _agent.isStopped = false;
            _destinationSet = true;
            return Status.Running;
        }

        return Status.Failure;
    }

    protected override Status OnUpdate()
    {
        if (!_destinationSet) return Status.Failure;

        //Comprobar si llega al destino
        if (!_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance)
        {
            return Status.Success;
        }

        return Status.Running;
    }

}

