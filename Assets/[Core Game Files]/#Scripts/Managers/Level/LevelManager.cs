using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LevelManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private List<Level> _levels = new List<Level>();
    public Transform debrisParent;

    // Component References
    private NavMeshSurface _navMeshSurface;

    private int _currentlevelIndex = 0;

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


    private void Start()
    {
        _navMeshSurface = GetComponent<NavMeshSurface>();
        NextLevel();
    }

    // If there is already a Level gameobject its deletes it,
    // and instantiate a new level prefab by current level index if there is a level in level list.
    public void RestartLevel()
    {
        GameObject go = GameObject.FindWithTag("Level");
        if (go != null)
            Destroy(go);

        if(_levels.Count > 0)
        {
            _levels[_currentlevelIndex].Load();
            _navMeshSurface.BuildNavMesh();
        }
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
}
