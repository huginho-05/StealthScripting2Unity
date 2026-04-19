using System;
using UnityEngine;

public class ActivateGoblin : MonoBehaviour
{
    [SerializeField] private GameObject goblin;

    private void OnTriggerEnter(Collider other)
    {
        goblin.SetActive(true);
    }
}
