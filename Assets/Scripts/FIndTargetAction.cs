using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "FIndTarget", story: "[Sensor] is searching", category: "Action", id: "be04563c58c5ee79d5fe89180d08b191")]
public partial class FIndTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<SensorSystem> Sensor;
    [SerializeReference] public BlackboardVariable<GameObject> Target;

    protected override Status OnUpdate()
    {
        //Pido al sensor todos los Update que me evalue si encuentra un posible objetivo...
        GameObject posibleTarget = Sensor.Value.SearchTarget();
        
        //Si no tengo objetivo todavia y encuentri algo nuevo...
        if (Target.Value == null && posibleTarget != null)
        {
            //Hemos encontrado algo
            Target.Value = posibleTarget;
            return Status.Success;
        }
   
        return Status.Running;
        
    }

    protected override void OnEnd()
    {
    }
}

