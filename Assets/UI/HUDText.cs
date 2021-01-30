using UnityEngine;
using UnityEngine.UI;

public class HUDText : MonoBehaviour
{
    public static int Health = 100;
    public static string GunString;

    private Text text;

    private void Awake()
    {
        text = GetComponent<Text>();
    }

    private void Update()
    {
        text.text = $"Health {Health}      {GunString}";
    }
}
