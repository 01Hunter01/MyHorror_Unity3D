using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour
{

    public UnityEvent enter;
    public UnityEvent exit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            enter.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            exit.Invoke();
    }
}
