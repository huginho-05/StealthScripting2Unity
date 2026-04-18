using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "PlayAnimAndWaitAction", story: "[Anim] plays animation in [ParameterName]", category: "Action", id: "2ed7b6c7784d6e866507d9cf767d20dc")]
public partial class PlayAnimAndWaitAction : Action
{
    [SerializeReference] public BlackboardVariable<Animator> Anim;
    [SerializeReference] public BlackboardVariable<string> ParameterName;

    private float clipTime; //Cuanto dura la animacion
    private float timer = 0;
    
    protected override Status OnStart()
    {
        Anim.Value.SetTrigger(ParameterName.Value);
        timer = 0; //me aseguro que cada vez que empiece esta tarea, el timer empiece en 0
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        //Obtengo la longitud actual de la animación que se está ejecutando
        clipTime = Anim.Value.GetCurrentAnimatorStateInfo(0).length;
        timer += Time.deltaTime;

        //Operador ternario
        return timer >= clipTime ? Status.Success : Status.Running;
    }

    protected override void OnEnd()
    {
    }
}

