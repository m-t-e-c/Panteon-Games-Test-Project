using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LevelManager : MonoBehaviour
{
    // References
    [SerializeField] private List<Level> _levels = new List<Level>();
    [SerializeField] private int _currentlevelIndex = 0;

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

    // Component References
    private NavMeshSurface _navMeshSurface;

    private void Start()
    {
        _navMeshSurface = GetComponent<NavMeshSurface>();
        NextLevel();
    }

    [ContextMenu("Load Next Level")]
    public void NextLevel()
    {
        GameObject go = GameObject.FindWithTag("Level");
        if (go != null)
            Destroy(go);


        if (_currentlevelIndex == _levels.Count - 1)
            _currentlevelIndex = 0;
        else
            _currentlevelIndex++;

        _levels[_currentlevelIndex].Load();
        _navMeshSurface.BuildNavMesh();
    }

    public void RestartLevel()
    {
        GameObject go = GameObject.FindWithTag("Level");
        if (go != null)
            Destroy(go);

        _levels[_currentlevelIndex].Load();
        _navMeshSurface.BuildNavMesh();
    }
}
