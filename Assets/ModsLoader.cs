using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class ModsLoader : MonoBehaviour
{
    string rootPath = Path.Combine(Application.dataPath, "..");
    // Start is called before the first frame update
    void Start()
    {
        string mapsPath = Path.Combine(rootPath, "Mods");
        
        if (!Directory.Exists(Path.Combine(rootPath, "Mods")))
        {
            Directory.CreateDirectory(mapsPath);
        }
        string[] allMods = Directory.GetDirectories(mapsPath);
        for (int i = 0; i < allMods.Length; i++)
        {
            string[] readedLines = File.ReadAllLines(allMods[i] + "meta.txt");
            ModParse(readedLines);
        }
    }
    public void OpenFolderInExplorer()
    {
        Application.OpenURL("file:///" + Path.Combine(rootPath, "Mods"));
    }
    public Mod ModParse(string[] text)
    {
        return null;
    }



    void Update()
    {
        
    }
}
public class Mods : MonoBehaviour
{
    public static List<Mod> mods;
}
public class Mod : MonoBehaviour
{

}
public class CellMod : MonoBehaviour
{
    public float speed;
    public float damage;
}
public class ClanMod : MonoBehaviour
{

}
public class CivMod : MonoBehaviour
{

}