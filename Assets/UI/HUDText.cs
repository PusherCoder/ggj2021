using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HUDText : MonoBehaviour
{
    public static int Health = 100;
    public static string GunString;

    private static HUDText instance;

    [SerializeField] private CanvasGroup gameOver;
    [SerializeField] private CanvasGroup bloodCanvasGroup;

    [SerializeField] private AudioClip hurt1;
    [SerializeField] private AudioClip hurt2;
    [SerializeField] private AudioClip hurt3;

    private AudioSource audioSource;

    private Text text;
    private float bloodAlpha = 0f;

    private void Awake()
    {
        instance = this;
        text = GetComponent<Text>();
        audioSource = GetComponent<AudioSource>();
    }

    public static void TakeDamage(int damage)
    {
        Health -= damage;
        instance.bloodAlpha = 1f;

        float random = Random.Range(0f, 1f);
        if (random < .6f)
            instance.audioSource.clip = instance.hurt1;
        else if (random < .95f)
            instance.audioSource.clip = instance.hurt2;
        else
            instance.audioSource.clip = instance.hurt3;
        instance.audioSource.Play();
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
                AllEnemies.Enemies.Clear();
                Time.timeScale = 1f;
                Scene scene = SceneManager.GetActiveScene(); 
                SceneManager.LoadScene(scene.name);
            }

        }

        bloodAlpha -= Time.deltaTime * 2f;
        bloodCanvasGroup.alpha = bloodAlpha;
    }
}
