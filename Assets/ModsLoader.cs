using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;


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

            // �������� text[4], ��� � ���� ���� � �����������
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
                // �������� text[3], ��� � ���� ���� � �����������
                mod.clan.Icon = null;
        }
        else if (text[0] == "Civi")
        {
            mod.Type = 3;
            mod.civi.Description = text[1];

            mod.civi.Gift1 = int.Parse(text[2]);
            mod.civi.Gift2 = int.Parse(text[3]);
            mod.civi.Gift3 = int.Parse(text[4]);

            // �������� text[5], ��� � ���� ���� � �����������
            mod.civi.Ok = null;

            // �������� text[6], ��� � ���� ���� � �����������
            mod.civi.Angry = null;
            
            // �������� text[7], ��� � ���� ���� � �����������
            mod.civi.Joy = null;
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
    public Image Icon;
}

public class CiviMod
{
    public string Description;
    public float Gift1;
    public float Gift2;
    public float Gift3;
    public Image Ok;
    public Image Angry;
    public Image Joy;
}