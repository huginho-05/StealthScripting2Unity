using System;
using Unity.Behavior;

[BlackboardEnum]
public enum EnemyCurrentState
{
	WaitingForPlayer,
	PhysicalCombat,
	Tired
}
