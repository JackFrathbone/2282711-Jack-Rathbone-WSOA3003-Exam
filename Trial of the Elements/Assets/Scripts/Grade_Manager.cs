using UnityEngine;
using TMPro;

public class Grade_Manager : MonoBehaviour
{
    private float averageTime;
    private float averageTimePlus;
    public int averageTimeMinus;

    private float gradeTimer;

    private string grade;

    public TextMeshProUGUI timeText, gradeText;

    private void Start()
    {
        averageTime = averageTimeMinus + (averageTimeMinus * 0.15f);
        averageTimePlus = averageTime + (averageTime * 0.15f);
    }

    private void Update()
    {
        gradeTimer += Time.deltaTime;

        float minutes = Mathf.FloorToInt(gradeTimer / 60);
        float seconds = Mathf.FloorToInt(gradeTimer % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        gradeText.text = grade;

        if(gradeTimer < averageTimeMinus)
        {
            grade = "A";
        }
        else if(gradeTimer >= averageTimeMinus && gradeTimer < averageTimePlus)
        {
            grade = "B";
        }
        else if(gradeTimer > averageTimeMinus && gradeTimer > averageTimePlus)
        {
            grade = "C";
        }
    }

    public string GiveGrade()
    {
        return grade;
    }


}
