using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : MonoBehaviour
{
    public static string gameMode =" ";

    public void timerMode()
    {
        gameMode = "Timer Mode";
    }
    public void bestOf7()
    {
        gameMode = "Best of 7";
    }
}
