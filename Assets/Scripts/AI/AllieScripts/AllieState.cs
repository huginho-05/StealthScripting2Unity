using UnityEngine;

public abstract class AllieState : MonoBehaviour
{
    //Todo estado en una maquina de estado se ejecuta en tres fases:

    //public abstract void InitController(Allie_FSMController allieController);
    
    public abstract void OnEnterState(Allie_FSMController allieController);

    public abstract void OnUpdateState();

    public abstract void OnExitState();
}
