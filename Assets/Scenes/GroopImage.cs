using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class GroopImage : MonoBehaviour
{
    [SerializeField] private SpriteRenderer image;
    [SerializeField] private List<Sprite> sprites = new List<Sprite>();
    [SerializeField] private TextMeshProUGUI countText;
    public int countBear;
    public int owner;
    public GameObject targetPosition;
    public Vector3 StartPosition;
    public Vector3 EndPosition;
    public Groop thisGroop;
    public int targetWalking;
    public int startWalking;
    public GroopController creator;

    private void OnMouseDown()
    {
        ReturnGroop();
    }
    public void SinhronPosition()
    {
        this.transform.position = targetPosition.transform.position;
    }
    public void ReturnGroop()
    {
        thisGroop.playerNavigation.SetTargetPosition(StartPosition);
        EndPosition = StartPosition;
        targetWalking = startWalking;
    }
    public void SetInfo(int count, Groop groop, GroopController groopCreator)
    {
        countText.text = count.ToString();
        countBear = count;
        thisGroop = groop;
        creator = groopCreator;
        image.sprite = sprites[owner];
    }
}