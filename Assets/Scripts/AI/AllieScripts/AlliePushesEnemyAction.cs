using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine.AI;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "AlliePushesEnemy", story: "[Self] pushes [Enemy]", category: "Action", id: "7e0472d6c01159a0026489a5d7ae9d27")]
public partial class AlliePushesEnemyAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<GameObject> Enemy;

    // Fuerza del empujón, ajustable desde el Behavior Graph
    [SerializeReference] public BlackboardVariable<float> PushForce = new BlackboardVariable<float>(10f);

    protected override Status OnStart()
    {
        if (Self.Value == null || Enemy.Value == null) return Status.Failure;

        // 1. Calcular la dirección del empujón (desde el aliado hacia el enemigo)
        Vector3 pushDirection = (Enemy.Value.transform.position - Self.Value.transform.position).normalized;
        pushDirection.y = 0.2f; // Le damos un pequeño toque hacia arriba para que no roce con el suelo

        // 2. Intentar aplicar fuerza física si el enemigo tiene Rigidbody
        Rigidbody enemyRb = Enemy.Value.GetComponent<Rigidbody>();
        if (enemyRb != null)
        {
            // Limpiamos velocidad previa para que el impacto sea seco
            enemyRb.linearVelocity = Vector3.zero; 
            enemyRb.AddForce(pushDirection * PushForce.Value, ForceMode.Impulse);
        }
        else
        {
            // 3. Si usa NavMesh sin Rigidbody, lo movemos con un "Warp" rápido hacia atrás
            NavMeshAgent enemyAgent = Enemy.Value.GetComponent<NavMeshAgent>();
            if (enemyAgent != null && enemyAgent.enabled)
            {
                Vector3 targetPos = Enemy.Value.transform.position + (pushDirection * 2f);
                enemyAgent.Warp(targetPos);
            }
        }

        return Status.Success;
    }

    protected override Status OnUpdate() => Status.Success;
}

