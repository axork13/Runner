using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private TextMeshProUGUI bestScoreText;

    void Update()
    {
        scoreText.text = "Score: " + GameManager.Instance.score;
        bestScoreText.text = "Best: " + GameManager.Instance.playerData.highScore;
    }
}
