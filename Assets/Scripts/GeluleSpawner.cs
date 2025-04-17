using System.Collections;
using System.Linq;
using UnityEngine;

public class GeluleSpawner : MonoBehaviour
{


    [Header("Spawn Settings")]
    [SerializeField] private Transform scrollerTransform;
    [SerializeField] private GameObject[] gelules;
    [SerializeField] private Transform[] spawnPoints;

    [Header("Spawn Timing")]
    [SerializeField] private float rate = 2.5f; // 1 spawn toute les 2.5s
    [SerializeField] private float minRate = 1.2f;
    [SerializeField] private float speedUpInterval = 40f; // augmente la cadence toute les 40s
    [SerializeField] private float rateDecrease = 0.1f; //Accélération modérée

    [Header("Life Managers")]
    [SerializeField] private LifeManager[] lifeManagers;

    private int minSP = 1;
    private int maxSP = 4;

    void Start()
    {
        StartCoroutine(GeluleSpawn());
        StartCoroutine(SetToFourLane());
        StartCoroutine(SetToFiveLane());
    }

    IEnumerator GeluleSpawn()
    {
        while (true)
        {

            AdjustSpawnRateToLifeLoss();

            yield return new WaitForSeconds(rate);


            GeluleType typeToSpawn = RouletteGeluleType();

            GameObject prefabToSpawn = gelules.First(g => g.GetComponent<GeluleController>().type == typeToSpawn);

            GameObject newGelule = Instantiate(
                prefabToSpawn,
                spawnPoints[Random.Range(minSP, maxSP)].position,
                transform.rotation
            );

            GeluleController controller = newGelule.GetComponent<GeluleController>();
            if (controller != null)
            {
                Debug.Log("Gélule instanciée : " + controller.type);
                controller.OnChangeLife.AddListener(lifeManagers[(int)controller.type].AddLife);
            }

            newGelule.transform.SetParent(scrollerTransform);
        }
    }

    IEnumerator IncreaseSpawnInterval()
    {
        while (true)
        {
            yield return new WaitForSeconds(speedUpInterval);
            rate += rateDecrease; // on augmente le délai entre deux spawns
            Debug.Log("Nouvelle lenteur d'apparition : " + rate);
        }
    }

    IEnumerator SetToFourLane()
    {
        yield return new WaitForSeconds(60f);
        maxSP = 5;
    }

    IEnumerator SetToFiveLane()
    {
        yield return new WaitForSeconds(180f);
        minSP = 0;
    }

    private void AdjustSpawnRateToLifeLoss()
    {
        float averageLossPercent = lifeManagers.Average(lm => lm.LossPercent);

        // Calcule un facteur entre 0 et 1 basé sur la montée de difficulté
        float normalizedLoss = Mathf.InverseLerp(10f, 60f, averageLossPercent);
        float dynamicRate = Mathf.Lerp(2.5f, minRate, normalizedLoss); // plus le loss est grand, plus rate est petit

        rate = dynamicRate;
    }


    private GeluleType RouletteGeluleType()
    {
        float criticalThreshold = 0.2f;
        float criticalBoost = 300f;
        float[] weights = new float[lifeManagers.Length];

        for (int i = 0; i < lifeManagers.Length; i++)
        {
            float currentLife = Mathf.Clamp01(lifeManagers[i].CurrentLife);
            float missing = 1f - currentLife;
            
            if (currentLife <= 0.05f) weights[i] = 9999f; // quasi-garanti !
            else if (currentLife <= criticalThreshold)
            {
                // Priorité extrême pour cette jauge
                weights[i] = criticalBoost;
            }
            else
            {
                weights[i] = missing + 0.01f; // pour éviter 0
            }           

        }

        float total = weights.Sum();
        float random = Random.Range(0f, total);
        float cumulative = 0f;

        for (int i = 0; i < weights.Length; i++)
        {
            cumulative += weights[i];
            if (random <= cumulative)
            {
                Debug.Log($"[Roulette] {((GeluleType)i)} choisi (life: {lifeManagers[i].CurrentLife}, poids: {weights[i]})");
                return (GeluleType)i;
            }
        }

        return GeluleType.Blue;
    }


}
