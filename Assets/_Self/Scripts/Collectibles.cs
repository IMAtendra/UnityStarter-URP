using System;
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

    public GameObject target;
    public bool move;

    void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Finish");
    }

    public void Collect()
    {
        Debug.Log($"Hi You Have Collected {ItemType}");
        OnCollectiblesCollected?.Invoke(IncrementValue, ItemType);
        gameObject.GetComponent<Collider>().enabled = false;
        move = true;
    }

    void LateUpdate()
    {
        if (move)
        {
            // move to Target 
            transform.position = Vector3.Lerp(transform.position, target.transform.position, 1f * Time.deltaTime);
            // Desired Distance to remove coin Object in scene
            if (Vector3.Distance(transform.position, target.transform.position) < 1f) Destroy(gameObject);
        }
    }
}
