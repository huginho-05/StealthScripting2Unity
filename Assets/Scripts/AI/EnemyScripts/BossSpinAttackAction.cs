using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "BossSpinAttack", story: "[Self] spins around himself while navigates to [Player]", category: "Action", id: "4a201769b0b4355ca016f5d17dba4d69")]
public partial class BossSpinAttackAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<GameObject> Player;

    [Header("Ataque")]
    public float MoveSpeed = 5f;
    public float SpinSpeed = 0.1f;
    public float AttackDuration = 4.0f; 

    [Header("Mareo")]
    public float DizzyDuration = 2.0f;

    private float _timer;
    private bool _isDizzy;

    protected override Status OnStart()
    {
        if (Self.Value == null || Player.Value == null) return Status.Failure;
        
        _timer = 0f;
        _isDizzy = false;
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (Self.Value == null || Player.Value == null) return Status.Failure;

        Transform bossTransform = Self.Value.transform;
        _timer += Time.deltaTime;

        if (!_isDizzy)
        {
            //Girar sobre sí mismo
            bossTransform.Rotate(Vector3.up, SpinSpeed * Time.deltaTime);

            //Moverse hacia el jugador
            Vector3 targetPos = Player.Value.transform.position;
            Vector3 destination = new Vector3(targetPos.x, bossTransform.position.y, targetPos.z);
            bossTransform.position = Vector3.MoveTowards(bossTransform.position, destination, MoveSpeed * Time.deltaTime);

            //Se acaba el tiempo de ataque
            if (_timer >= AttackDuration)
            {
                _isDizzy = true;
                _timer = 0f; 
            }
        }
        else
        {
            if (_timer >= DizzyDuration)
            {
                //Termina la acción y el Behavior Tree pasará al siguiente nodo
                return Status.Success;
            }
        }

        return Status.Running;
    }
}

