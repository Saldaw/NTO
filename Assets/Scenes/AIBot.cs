using System.Collections;
using UnityEngine;

public class AIBot : MonoBehaviour
{
    [SerializeField] private int numBot;
    [SerializeField] private int necessarAmountOfFriendshipForAttackHonyTree;
    [SerializeField] private int friendshipLevelRequiredToLaunchAttackOnPlayer;
    [SerializeField] private ShopController shopController;
    private GroopController groopController;

    // Start is called before the first frame update
    void Start()
    {
        groopController = GetComponent<GroopController>();
        StartCoroutine(TiksBot());
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void AttackOnTree()
    {
        int countHonyBotBase = PlayerPrefs.GetInt($"countHony{numBot}");
        groopController.CreateGroop((int)(countHonyBotBase * UnityEngine.Random.Range(5f, 20f))+1, UnityEngine.Random.Range(10, 19));
    }
    private void AttackOnPlayer()
    {
        int countHonyBotBase = PlayerPrefs.GetInt($"countHony{numBot}");
        groopController.CreateGroop((int)(countHonyBotBase * UnityEngine.Random.Range(5f, 20f))+1, 5);
    }
    private void CheckInfo()
    {
        int frending = PlayerPrefs.GetInt($"friendy{numBot}");
        int countHonyPlayerBase = PlayerPrefs.GetInt("countHony5");
        int countHonyBotBase = PlayerPrefs.GetInt($"countHony{numBot}");
        if (countHonyBotBase < countHonyPlayerBase)
        {
            if (frending <= necessarAmountOfFriendshipForAttackHonyTree)
            {
                if (UnityEngine.Random.Range(1, 3) > 1)AttackOnTree();
            }
            else if (UnityEngine.Random.Range(1, 3) == 1 && frending > 1)
            {
                PlayerPrefs.SetInt($"friendy{numBot}", frending - 1);
            }
        }
        else if (frending <= friendshipLevelRequiredToLaunchAttackOnPlayer && UnityEngine.Random.Range(1, 2) == 1)
        {
            AttackOnPlayer();
        }
        else if (UnityEngine.Random.Range(1, 3) == 1 && frending <= 99)
        {
            PlayerPrefs.SetInt($"friendy{numBot}", frending + 1);
            if(UnityEngine.Random.Range(1, 3) == 1) shopController.RestartShop(numBot);
        }
    }
    private IEnumerator TiksBot()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            if (PlayerPrefs.GetInt($"isDie{numBot}")==1 )
            {
                break;
            }
            CheckInfo();
            yield return new WaitForSeconds(90f);
        }
    }
}
