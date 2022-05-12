using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<Level> _levels = new List<Level>();
    [SerializeField] private int _levelIndex = 0;

    private static LevelManager _instance;
    public static LevelManager instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<LevelManager>();

            return _instance;
        }
    }

    [ContextMenu("Load Next Level")]
    public void NextLevel()
    {
        GameObject go = GameObject.FindWithTag("Level").gameObject;
        if (go != null)
            Destroy(go);

        if (_levels.Count > 0)
        {
            _levels[_levelIndex].Load();
        }
    }

    public void RestartLevel()
    {
        GameObject go = GameObject.FindWithTag("Level").gameObject;
        if (go != null)
            Destroy(go);

        _levels[_levelIndex].Load();
    }
}
