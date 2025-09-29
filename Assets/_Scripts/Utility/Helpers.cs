using UnityEngine;

public static class Helpers
{
    public static void SetLayerRecursively(GameObject gameObject, int layer)
    {
        if (gameObject == null) return;

        gameObject.layer = layer;

        foreach (Transform child in gameObject.transform)
        {
            if (child == null) continue;
            SetLayerRecursively(child.gameObject, layer);
        }
    }

    public static void SetLayerRecursively(GameObject gameObject, string layerName)
    {
        int layer = LayerMask.NameToLayer(layerName);
        SetLayerRecursively(gameObject, layer);
    }
}
