using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    //public int health;
    [SerializeField]
    private int currentHealth;
    public IntVariable health;
    //public IntGameAction takeDamage;

    public IKillable player;

    [SerializeField]
    private int damage = 10;
    void Start()
    {
        currentHealth = health.Value;
        player = GameObject.Find("Player").GetComponent<IKillable>();
    }

    //[]
    public void Hit()
    {
        if (true)
        {
            if (player != null)
            {
                player.Kill();
            }

            //takeDamage.InvokeAction(10);
            //player.TakeDamage(damage);
        }
        //
    }
    private void OnGUI()
    {
        if (GUI.Button(new Rect(20, 110, 180, 30), "Hit"))
        {
            Hit();
        }


    }
}
