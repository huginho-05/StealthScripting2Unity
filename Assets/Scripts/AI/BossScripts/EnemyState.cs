using UnityEngine;

public abstract class EnemyState : MonoBehaviour
{
    //Todo estado en una maquina de estado se ejecuta en tres fases:

    //public abstract void InitController(Allie_FSMController allieController);
    
    public abstract void OnEnterState(Enemy_FMSController enemyController);

    public abstract void OnUpdateState();

    public abstract void OnExitState();
}
