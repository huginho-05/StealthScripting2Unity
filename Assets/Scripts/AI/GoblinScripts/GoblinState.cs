using UnityEngine;

public abstract class GoblinState : MonoBehaviour
{
    //Todo estado en una maquina de estado se ejecuta en tres fases:
    
    public abstract void OnEnterState(Goblin_FMSController goblinController);

    public abstract void OnUpdateState();

    public abstract void OnExitState();
}
