using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] public float timeToSunrise;
    public TextMeshProUGUI timeText;
    private bool gameOver;

    // Start is called before the first frame update
    void Start()
    {
        timeToSunrise = 30f;
    }

    // Update is called once per frame
    void Update()
    {
        timeToSunrise -= Time.deltaTime;
        if (timeToSunrise < 0)
        {
            DisplayTime(timeToSunrise);
        }
        else
        {
            timeToSunrise = 0;
            gameOver = true;
        }
    }

    void DisplayTime(float timeToSunrise)
    {
        float minutes = Mathf.FloorToInt(timeToSunrise / 60);
        float seconds = Mathf.FloorToInt(timeToSunrise % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
