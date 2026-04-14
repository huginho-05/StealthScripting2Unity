using UnityEngine;

public class Allie_FSMController : MonoBehaviour
{
    private AllieState allieCurrentState; //Estado actual de la maquina

    private AllieRelaxedState relaxedState;

    private void Awake()
    {
        relaxedState = GetComponent<AllieRelaxedState>();
        ChangeState(relaxedState);
    }

    private void Update()
    {
        allieCurrentState?.OnUpdateState();  //Las interrogaciones sirven para comprobar si lo pedido existe
    }

    public void ChangeState(AllieState allieNewState)
    {
        allieCurrentState?.OnExitState();
        allieCurrentState = allieNewState;
        allieCurrentState = allieNewState;
    }

}
