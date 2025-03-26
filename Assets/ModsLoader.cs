using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using Unity.VisualScripting;


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
        return Sprite.Create(texture, new Rect(0, 0, size.x,size.y), Vector2.zero);
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
            mod.cell.HP = int.Parse(text[1]);
            mod.cell.Speed = int.Parse(text[2]);
            mod.cell.Damage = int.Parse(text[3]);

            mod.cell.Image = GetImage(text[4], new Vector2Int(50, 83));
            mod.cell.Image = null;
        }
        else if (text[0] == "Clan")
        {
            mod.Type = 2;
            mod.clan.Type = text[1];

            if (text[1] == "WoodSpeed")
                mod.clan.WoodSpeed = int.Parse(text[2]);
            else if (text[1] == "HoneySpeed")
                mod.clan.HoneySpeed = int.Parse(text[2]);
            else if (text[1] == "TowerDamage")
                mod.clan.TowerDamage = int.Parse(text[2]);
            else if (text[1] == "BearSpeed")
                mod.clan.BearSpeed = int.Parse(text[2]);
            else if (text[1] == "HeroDamage")
                mod.clan.HeroDamage = int.Parse(text[2]);

            if (text[3] != "-")
                mod.clan.Icon = GetImage(text[3], new Vector2Int(83, 81));
            mod.clan.Icon = null;
        }
        else if (text[0] == "Civi")
        {
            mod.Type = 3;
            mod.civi.Description = text[1];

            mod.civi.Gift1 = int.Parse(text[2]);
            mod.civi.Gift2 = int.Parse(text[3]);
            mod.civi.Gift3 = int.Parse(text[4]);

            mod.civi.Ok = GetImage(text[5], new Vector2Int(256, 256));

            mod.civi.Angry = GetImage(text[6], new Vector2Int(256, 256));

            mod.civi.Joy = GetImage(text[7], new Vector2Int(256, 256));
        }

        return mod;
    }



    void Update()
    {
        
    }
}

public class Mod
{
    public static List<Mod> mods;
    public CellMod cell;
    public ClanMod clan;
    public CiviMod civi;
    public int Type;
}

public class CellMod
{
    public float Speed;
    public float HP;
    public float Damage;
    public Sprite Image;
}

public class ClanMod
{
    // "WoodSpeed" / "HoneySpeed" / "TowerDamage" / "BearSpeed" / "HeroDamage"
    public string Type;
    
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
    public float Gift1;
    public float Gift2;
    public float Gift3;
    public Sprite Ok;
    public Sprite Angry;
    public Sprite Joy;
}