using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AH = AdnanHelper;

public static class PopupSpawner
{
    public static PopupText spawnText(GameObject prefab, Transform transform)
    {
        return GameObject.Instantiate(prefab, transform).GetComponent<PopupText>();
    }
    public static PopupText spawnText(GameObject prefab, Vector3 pos)
    {
        return GameObject.Instantiate(prefab, pos, prefab.transform.rotation).GetComponent<PopupText>();
    }
}
