using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "EnemyRunsPlayer", story: "[Self] runs [Player]", category: "Action", id: "172f9474224ba88b1d2470178d17c97c")]
public partial class EnemyRunsPlayerAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<GameObject> Player;
    
    public float Speed = 10f;
        
    public float StopDistance = 0.5f;

    private Vector3 targetPosition;
    private bool hasTarget = false;

    protected override Status OnStart()
    {
        if (Player.Value == null || Self.Value == null)
            return Status.Failure;
        
        //Ultima posicion del jugador 
        targetPosition = Player.Value.transform.position;
        hasTarget = true;

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (!hasTarget) return Status.Failure;

        GameObject enemy = Self.Value;

        //Mover al enemigo hacia esa posicion 
        enemy.transform.position = Vector3.MoveTowards(
            enemy.transform.position, 
            targetPosition, 
            Speed * Time.deltaTime
        );

        //Hacer que mire hacia ese punto
        Vector3 direction = (targetPosition - enemy.transform.position).normalized;
        if (direction != Vector3.zero)
        {
            enemy.transform.forward = direction;
        }

        //Llega al punto marcado
        float distanceLeft = Vector3.Distance(enemy.transform.position, targetPosition);
        if (distanceLeft <= StopDistance)
        {
            return Status.Success;
        }

        return Status.Running;
    }

    protected override void OnEnd()
    {
        hasTarget = false;
    }
}

