using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HUDText : MonoBehaviour
{
    public static int Health = 100;
    public static string GunString;

    [SerializeField] private CanvasGroup gameOver;

    private Text text;

    private void Awake()
    {
        text = GetComponent<Text>();
    }

    private void Update()
    {
        text.text = $"Health {Mathf.Clamp(Health, 0, 9999)}      {GunString}";

        if (Health <= 0)
        {
            gameOver.alpha += Time.unscaledDeltaTime;
            gameOver.alpha = Mathf.Clamp01(gameOver.alpha);
            Time.timeScale = 1 - gameOver.alpha;

            if (gameOver.alpha > .85f && Input.GetMouseButtonDown(0))
            {
                Health = 100;
                Time.timeScale = 1f;
                Scene scene = SceneManager.GetActiveScene(); 
                SceneManager.LoadScene(scene.name);
            }

        }
    }
}
