using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Dit script hangt aan (bv) de Camera
    // Zet hieronder al je variabelen die in de hele productie bereikbaar zijn

    public List<GameObject> interactableObst = new List<GameObject>();

    public bool gameMenuActive;
    public bool gameOver;

    public int score;
    public int countDownTimer;

    public static GameManager instance = null;
    void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);

    }

}
