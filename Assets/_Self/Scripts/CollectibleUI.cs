using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class CollectibleUI : MonoBehaviour
{
	public int score;
    [SerializeField] private TMP_Text CoinText;
    [SerializeField] private TMP_Text GemText;

    private void OnEnable()
    {
        Collectibles.OnCollectiblesCollected += CollectiblesCollected;
    }

    private void OnDisable()
    {
        Collectibles.OnCollectiblesCollected -= CollectiblesCollected;
    }

    private void CollectiblesCollected(int valueForIncrement, CollectibleType item)
    {
        // Debug.Log($"Increasing amount {valueForIncrement} of {item}");
        if (item == CollectibleType.Coin)
        {
            score += valueForIncrement;
            CoinText.text = score.ToString();
            Debug.Log(item + " : " + score);
        }
        
        if (item == CollectibleType.Gem)
        {
            score += valueForIncrement;
            GemText.text = score.ToString();
            Debug.Log(item + " : " + score);
        }
    }
}