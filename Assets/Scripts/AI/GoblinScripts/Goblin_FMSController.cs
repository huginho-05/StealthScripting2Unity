using UnityEngine;

public class Goblin_FMSController : MonoBehaviour
{
    private GoblinState goblinCurrentState; //Estado actual de la maquina

    private GoblinIdleState idleState;

    private void Awake()
    {
        idleState = GetComponent<GoblinIdleState>();
        ChangeState(idleState);
    }

    private void Update()
    {
        goblinCurrentState?.OnUpdateState();  //Las interrogaciones sirven para comprobar si lo pedido existe
    }

    public void ChangeState(GoblinState goblinNewState)
    {
        goblinCurrentState?.OnExitState();
        goblinCurrentState = goblinNewState;
        goblinCurrentState = goblinNewState;
    }
}
