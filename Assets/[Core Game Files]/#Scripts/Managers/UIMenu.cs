using UnityEngine;
using DG.Tweening;

public class UIMenu : UIElement
{
    public override void HideUI()
    {
        transform.DOScale(Vector3.zero, 0.2f);
    }

    public override void ShowUI()
    {
        transform.DOScale(Vector3.one, 0.4f);
    }

    public override UIMenuType GetUIType()
    {
        return _uiType;
    }
}
