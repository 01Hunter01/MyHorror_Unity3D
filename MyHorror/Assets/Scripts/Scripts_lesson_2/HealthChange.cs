using TMPro;
using UnityEngine;

public class HealthChange : MonoBehaviour
{
    public TextMeshProUGUI text;
    //public IntVariable helth;
    public void UpdateHealth(int num)
    {
        text.text = "" + num;
    }
}
