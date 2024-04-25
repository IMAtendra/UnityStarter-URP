using System;
using Unity.Mathematics;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Rendering.Universal;

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

    public GameObject effect;
    public GameObject destroyEffect;

    public void Collect()
    {
        Debug.Log($"Hi You Have Collected {ItemType}");
        OnCollectiblesCollected?.Invoke(IncrementValue, ItemType);
        gameObject.GetComponent<Collider>().enabled = false;
        Destroy(gameObject);
        
        // Spawn & Destroy Effect
        var temp = Instantiate(effect, transform.position, quaternion.identity);
        Destroy(temp, 2f);
        Destroy(gameObject);
        
        // Spawn & Destroy Effect
        var temp = Instantiate(destroyEffect, transform.position, quaternion.identity);
        Destroy(temp, 2f);
    }
}
