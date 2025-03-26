using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using Unity.VisualScripting;
using System.Linq;


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
            string[] readedLines = File.ReadAllLines(allMods[i] + "\\meta.txt");
            Mod.mods.Add(ModParse(readedLines));
        }
        
    }
    Sprite GetImage(string url,Vector2Int size)
    {
        byte[] pngData = File.ReadAllBytes(url);
        Texture2D texture = new Texture2D(size.x,size.y);
        texture.LoadImage(pngData);
        texture.filterMode = FilterMode.Point;
        texture.Apply();
        return Sprite.Create(texture, new Rect(0, 0, size.x,size.y), new Vector2(0.5f,0.5f));
    }
    public void OpenFolderInExplorer()
    {
        Application.OpenURL("file:///" + Path.Combine(rootPath, "Mods"));
    }
    public Mod ModParse(string[] text)
    {
        Mod mod = new Mod();

        if (text[0] == "Cell")
        {
            mod.Type = 1;
            mod.cell = new CellMod();
            mod.cell.IsPredator = text[1] == "1";

            mod.cell.Image = GetImage(Path.Combine(rootPath, "Mods", text[2]) , new Vector2Int(50, 83));
        }
        else if (text[0] == "Clan")
        {
            mod.Type = 2;
            mod.clan = new ClanMod();
            mod.clan.WoodSpeed = int.Parse(text[1]);
            mod.clan.HoneySpeed = int.Parse(text[2]);
            mod.clan.TowerDamage = int.Parse(text[3]);
            mod.clan.BearSpeed = int.Parse(text[4]);
            mod.clan.HeroDamage = int.Parse(text[5]);

            mod.clan.Icon = GetImage(Path.Combine(rootPath, "Mods", text[6]), new Vector2Int(83, 81));
        }
        else if (text[0] == "Civi")
        {
            mod.Type = 3;
            mod.civi = new CiviMod();
            mod.civi.Name = text[1];
            mod.civi.Description = text[2];

            mod.civi.Gift1 = int.Parse(text[3]);
            mod.civi.Gift2 = int.Parse(text[4]);
            mod.civi.Gift3 = int.Parse(text[5]);

            mod.civi.Ok = GetImage(Path.Combine(rootPath, "Mods", text[6]), new Vector2Int(256, 256));

            mod.civi.Angry = GetImage(Path.Combine(rootPath, "Mods", text[7]), new Vector2Int(256, 256));

            mod.civi.Joy = GetImage(Path.Combine(rootPath, "Mods", text[8]), new Vector2Int(256, 256));
        }

        return mod;
    }



    void Update()
    {
        
    }
}

public class Mod
{
    public static List<Mod> mods = new List<Mod>();
    public CellMod cell;
    public ClanMod clan;
    public CiviMod civi;
    public int Type;
}

public class CellMod
{
    public bool IsPredator;
    public Sprite Image;
}

public class ClanMod
{   
    public float WoodSpeed;
    public float HoneySpeed;
    public float TowerDamage;
    public float BearSpeed;
    public float HeroDamage;
    public Sprite Icon;
}

public class CiviMod
{
    public string Description;
    public string Name;
    public float Gift1;
    public float Gift2;
    public float Gift3;
    public Sprite Ok;
    public Sprite Angry;
    public Sprite Joy;
}