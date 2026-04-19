using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine.AI;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "AllieSwapsWithPlayer", story: "[Self] swaps with [Player]", category: "Action", id: "7e6f542fdc19ef7aaaca328691c5e845")]
public partial class AllieSwapsWithPlayerAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<GameObject> Player;

    protected override Status OnStart()
    {
        if (Self.Value == null || Player.Value == null) return Status.Failure;

        Vector3 GoblinPos = Self.Value.transform.position;
        Vector3 PlayerPos = Player.Value.transform.position;

        //Mover goblin
        NavMeshAgent selfAgent = Self.Value.GetComponent<NavMeshAgent>();
        if (selfAgent != null && selfAgent.enabled)
            selfAgent.Warp(PlayerPos);
        else
            Self.Value.transform.position = PlayerPos;

        //Mover player
        CharacterController cc = Player.Value.GetComponent<CharacterController>();
        Rigidbody rb = Player.Value.GetComponent<Rigidbody>();

        if (cc != null)
        {
            //Apagar CharacterController para teletransportar y volverlo a activar al completar el teletransporte
            cc.enabled = false;
            Player.Value.transform.position = GoblinPos;
            cc.enabled = true;
        }
        else if (rb != null)
        {
            rb.position = GoblinPos;
            rb.linearVelocity = Vector3.zero; //Evitar que se mueva de más por la inercia
        }

        return Status.Success;
    }

    protected override Status OnUpdate() => Status.Success;
}

