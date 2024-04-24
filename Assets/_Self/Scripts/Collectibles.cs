using System;
using UnityEngine;

public interface ICollectible
{
    void Collect();
}

public enum CollectibleType
{
    Coin,
    Gem
}

public class Collectibles : MonoBehaviour, ICollectible
{
    public int IncrementValue;
    public CollectibleType ItemType;
    public static event Action<int, CollectibleType> OnCollectiblesCollected;
    public void Collect()
    {
        Debug.Log($"Hi You Have Collected {ItemType}");
        OnCollectiblesCollected?.Invoke(IncrementValue, ItemType);
        Destroy(gameObject);
    }
}
