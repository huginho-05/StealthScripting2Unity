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

    private NavMeshAgent agent;
    private bool destinationSet = false;

    protected override Status OnStart()
    {
        if (Self.Value == null || Enemy.Value == null) return Status.Failure;

        agent = Self.Value.GetComponent<NavMeshAgent>();
        if (agent == null) return Status.Failure;

        //Calcular posición aleatoria alrededor del enemigo
        float angle = UnityEngine.Random.Range(0, 360) * Mathf.Deg2Rad;
        Vector3 offset = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * Radius.Value;
        Vector3 targetPos = Enemy.Value.transform.position + offset;

        //El punto está en el NavMesh
        if (NavMesh.SamplePosition(targetPos, out NavMeshHit hit, Radius.Value, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
            agent.isStopped = false;
            destinationSet = true;
            return Status.Running;
        }

        return Status.Failure;
    }

    protected override Status OnUpdate()
    {
        if (!destinationSet) return Status.Failure;

        //Comprobar si llega al destino
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            return Status.Success;
        }

        return Status.Running;
    }

}

