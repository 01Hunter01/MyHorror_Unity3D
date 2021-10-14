using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Compare : MonoBehaviour
{
    public int trashhold = 30;
    public UnityEvent arbbv;
    public void Check(int num)
    {
        if (num <= trashhold)
        {
            arbbv.Invoke();
        }
    }
}
