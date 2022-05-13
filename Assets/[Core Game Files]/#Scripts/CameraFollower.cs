using System;
using System.Collections.Generic;
using UnityEngine;

public enum CameraType { None, Player_Cam, Paint_Cam, Fail_Cam }

[Serializable]
public struct CameraData
{
    public CameraType cameraType;
    public Vector3 offset;
    public Vector3 rotationAngle;
    public float positionLerpSpeed;
    public float rotationLerpSpeed;
    public float fov;
}

public class CameraFollower : MonoBehaviour
{
    // Events
    public static Action<CameraType, Transform> OnCameraSetted;

    // References
    [Header("Cameras")]
    [SerializeField] private List<CameraData> _cameras = new List<CameraData>();
    [SerializeField] private CameraType _currentCameraType = CameraType.None;

    private Transform _target;
    private CameraData _currentCamera;
    private float defaultFov;

    #region Unity Methdos
    private void Start()
    {
        defaultFov = Camera.main.fieldOfView;
        SetCamera(_currentCameraType);
    }

    private void LateUpdate()
    {
        FollowTarget();
    }
    #endregion

    #region CameraFollower Methods

    public void SetCamera(CameraType camType)
    {
        foreach (CameraData camData in _cameras)
        {
            if (camData.cameraType == camType)
            {
                _currentCamera = camData;
                _currentCameraType = _currentCamera.cameraType;
                break;
            }
        }
    }

    private void FollowTarget()
    {
        // If there is no camera exist than return.
        if (_cameras.Count < 1 || _target == null)
            return;

        // Getting the CameraData parameters for to use more easily.
        Vector3 offset = _currentCamera.offset;
        Vector3 rotationAngle = _currentCamera.rotationAngle;
        float posLerpSpeed = _currentCamera.positionLerpSpeed;
        float rotLerpSepeed = _currentCamera.rotationLerpSpeed;
        float fov = _currentCamera.fov;

        // Setting the Camera Field of View.
        Camera.main.fieldOfView = fov;

        // We are only lerping in X axis to prevent stuterring.
        Vector3 followPos = new Vector3(_target.position.x, _target.position.y, _target.position.z) + offset;
        transform.position = new Vector3(
            Mathf.Lerp(transform.position.x, followPos.x, posLerpSpeed),
            followPos.y,
            followPos.z
            );

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(rotationAngle), rotLerpSepeed);
    }
    #endregion

    #region Action Methods
    private void m_OnCameraSetted(CameraType camType, Transform target)
    {
        SetCamera(camType);
        if (target != null)
            _target = target;
    }

    private void OnEnable() => OnCameraSetted += m_OnCameraSetted;
    private void OnDisable() => OnCameraSetted -= m_OnCameraSetted;

    #endregion
}
