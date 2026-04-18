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

    [Tooltip("Velocidad de la embestida")]
    public float Speed = 10f;
    
    [Tooltip("Distancia de parada para considerar que llegó")]
    public float StopDistance = 0.5f;

    private Vector3 _targetPosition;
    private bool _hasTarget = false;

    protected override Status OnStart()
    {
        if (Player.Value == null || Self.Value == null)
            return Status.Failure;
        
        //Ultima posicion del jugador 
        _targetPosition = Player.Value.transform.position;
        _hasTarget = true;

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (!_hasTarget) return Status.Failure;

        GameObject enemy = Self.Value;

        //Mover al enemigo hacia esa posicion 
        enemy.transform.position = Vector3.MoveTowards(
            enemy.transform.position, 
            _targetPosition, 
            Speed * Time.deltaTime
        );

        //Hacer que mire hacia ese punto
        Vector3 direction = (_targetPosition - enemy.transform.position).normalized;
        if (direction != Vector3.zero)
        {
            enemy.transform.forward = direction;
        }

        //Llega al punto marcado
        float distanceLeft = Vector3.Distance(enemy.transform.position, _targetPosition);
        if (distanceLeft <= StopDistance)
        {
            return Status.Success;
        }

        return Status.Running;
    }

    protected override void OnEnd()
    {
        _hasTarget = false;
    }
}

