using UnityEngine;

[CreateAssetMenu(menuName = "TEMPLATE ASSETS/ New Level", fileName = "Level")]
public class Level : ScriptableObject
{
    [Header("=== Level Settings ===")]
    public GameObject world;
    public int levelIndex;

    public void Load()
    {
        if (world != null) 
            Instantiate(world);
    }
}
