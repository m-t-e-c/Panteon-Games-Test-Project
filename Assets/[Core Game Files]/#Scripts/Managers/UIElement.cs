using UnityEngine;

public abstract class UIElement : MonoBehaviour
{
    [SerializeField] protected UIMenuType _uiType; 

    // Virtual Methods
    protected virtual void Awake()
    {
        if (_uiType != UIMenuType.None)
            UIManager.OnRegisterUI?.Invoke(_uiType,this);
    }

    // Abstract Methods
    public abstract UIMenuType GetUIType();
    public abstract void ShowUI();
    public abstract void HideUI();
}
