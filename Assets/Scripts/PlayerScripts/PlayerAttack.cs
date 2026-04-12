using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    private Animator animator;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }
    
    private void OnAttack()
    {
        animator.SetTrigger("PlayerAttack");
    }
}
