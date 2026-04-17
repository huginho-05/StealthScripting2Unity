using UnityEngine;

public class Enemy_FMSController : MonoBehaviour
{
    private EnemyState enemyCurrentState; //Estado actual de la maquina

    private EnemyPhysicalCombat physicalCombat;

    private void Awake()
    {
        physicalCombat = GetComponent<EnemyPhysicalCombat>();
        ChangeState(physicalCombat);
    }

    private void Update()
    {
        enemyCurrentState?.OnUpdateState();  //Las interrogaciones sirven para comprobar si lo pedido existe
    }

    public void ChangeState(EnemyState enemyNewState)
    {
        enemyCurrentState?.OnExitState();
        enemyCurrentState = enemyNewState;
        enemyCurrentState = enemyNewState;
    }
}
