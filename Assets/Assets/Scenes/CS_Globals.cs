using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CS_Globals
{
    public static uint Progress = 0;
    public static uint Level = 1;
    public static uint GrassEaten = 0;
    public static uint MeatyEaten = 0;

    public static List<GameObject> Foods =
        new List<GameObject>();

    public static List<GameObject> Cells =
        new List<GameObject>();

    public static float ProgressPercent()
        => (float)Progress / RequiredProgress();

    public static int RequiredProgress()
        => (int)Level * 5;

    public static void Evolve()
    {
        SceneManager.LoadSceneAsync(2);
        // код для перехода на следующий этап сюда
        // если понадобится, можешь использвать переменные
        // GrassEaten: количество съеденной травы
        // MeatyEaten: количество съеденного мяса
    }
}
