using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CS_Globals
{
    public static int Seed = 90000;
    public static uint Progress = 0;
    public static uint Level = 1;
    public static uint GrassEaten = 0;
    public static uint MeatyEaten = 0;

    public static Dictionary<string, int> EatenByType =
        new Dictionary<string, int>();

    public static List<GameObject> Foods =
        new List<GameObject>();

    public static List<GameObject> Cells =
        new List<GameObject>();

    public static float ProgressPercent()
        => (float)Progress / RequiredProgress();

    public static int RequiredProgress()
        => (int)Level * 3;

    public static void Evolve()
    {
        Seed = (int)Mathf.Lerp(Seed, 100000, 0.1f);
        SceneManager.LoadSceneAsync(8);
    }
}
