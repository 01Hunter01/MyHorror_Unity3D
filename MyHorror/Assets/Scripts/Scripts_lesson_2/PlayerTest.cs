using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    [SerializeField]
    private IntVariable Health;
    [SerializeField]
    //private IntGameAction helthChanged;

    public void Kill()
    {

    }

    public void TakeDamage(int amount)
    {
        Health.Value -= amount;
        //helthChanged.InvokeAction(Health.Value);

    }

    // Update is called once per frame
    void Update()
    {

    }
}

public interface IKillable
{
    void Kill();
}
