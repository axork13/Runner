using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class LifeManager : MonoBehaviour
{
    [SerializeField] private Slider slider;

    // perd 10% toute les 20s
    [SerializeField] float interval = 20f;
    [SerializeField] float lossPercent = 2f;

    private float timer;

    [Header("Scaling Settings")]
    [SerializeField] float lossPercentStep = 2f;
    [SerializeField] float stepInterval = 45f;
    [SerializeField] float maxLossPercent = 30f;

    public float CurrentLife => slider.value;
    public float LossPercent => lossPercent;

    private void Start()
    {
        slider = GetComponent<Slider>();
        timer = interval;
        StartCoroutine(ScaleLossPercentOverTime());

    }
    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            DamageLife();
            CheckGameOver();
            timer = interval;
        }

    }
    IEnumerator ScaleLossPercentOverTime()
    {
        while (lossPercent < maxLossPercent)
        {
            yield return new WaitForSeconds(stepInterval);
            lossPercent += lossPercentStep;
            lossPercent = Mathf.Min(lossPercent, maxLossPercent);
            Debug.Log($"[LifeManager] Nouveau lossPercent : {lossPercent}%");
        }
    }

    void DamageLife()
    {
        float loss = slider.maxValue * (lossPercent / 100f);
        ChangeLife(Mathf.Max(slider.value - loss, 0f));
    }

    public void InitLife(float value)
    {
        SetMaxLife(value);
        ChangeLife(value);
    }

    public void SetMaxLife(float value)
    {
        slider.maxValue = value;
    }

    public void ChangeLife(float value)
    {
        slider.value = value;
    }

    public void AddLife(float value)
    {
        slider.value = Mathf.Min(slider.value + value, slider.maxValue);
    }

    public void CheckGameOver()
    {
        if (slider.value <= 0)
        {
            GameManager.Instance.GameOver();
        }
    }

}
