using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int score = 0;

    [SerializeField]
    public AudioClip sfxPickUp; 
    private AudioSource audioSource;
    public PlayerData playerData;

    private void Awake()
    {
        // Singleton : une seule instance accessible globalement
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persiste entre les scènes

            playerData = SaveManager.Load();
        }
        else
        {
            Destroy(gameObject);
        }

        audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void AddScore(int value)
    {
        score += value;
        Debug.Log("Score: " + score);
    }

    public void GameOver()
    {        
        if (score > playerData.highScore)
        {
            playerData.highScore = score;
            SaveManager.Save(playerData);
        }
        SceneManager.LoadScene(2);
    }

    public void PlayPickUpSound()
    {
        audioSource.PlayOneShot(sfxPickUp);
    }
}