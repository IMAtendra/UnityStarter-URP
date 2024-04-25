using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneActiveObjectChecker : MonoBehaviour
{
    public void CheckActivePickupObjects()
    {
        var foundObjects = Object.FindObjectsOfType<Collectibles>();
        int count = foundObjects.Length;
        Debug.Log($"Now You have to Collect {count} Coin");

        if (count == 0) Debug.LogWarning("Yeah! You Have Collected All Collectibles");
    }

    void LateUpdate() => CheckActivePickupObjects();
}
