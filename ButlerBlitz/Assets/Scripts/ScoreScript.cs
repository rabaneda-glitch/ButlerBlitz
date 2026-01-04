using UnityEngine;

public class ScoreScript : MonoBehaviour
{
    public static ScoreScript Instance; // Singleton simple

    [Header("Puntaje")]
    public float score = 0;

    float pointsScore, timeScore;

    void Awake()
    {
        // Inicializa la instancia del Singleton
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }


    public void AddPoints(float points)
    {
        pointsScore += points * 100;
    }

    public float AddTime()
    {
        //variable de tiempo al 90%
        float timeFactor = MomentumScript.Instance.highRange();
        float timePoints = timeFactor * 50; 

        //variable de tiempo restante
        float remainingTime = Timer.Instance.GetTimeLeft();
        float timeBonusPoints = remainingTime * 10;

        timeScore = timePoints + timeBonusPoints;

        return timeScore;
    }


    public float CalcScore()
    {
        score = pointsScore + AddTime();
                
        return score;
    }
    public string GetScoreString()
    {
        CalcScore();
        return Mathf.RoundToInt(score).ToString();
    }

}
