using UnityEngine;
using DG.Tweening;

public class UIMenu : UIElement
{
    public override void HideUI()
    {
        transform.DOScale(new Vector3(0,1,1), 0.2f);
    }

    public override void ShowUI()
    {
        transform.DOScale(new Vector3(1, 1, 1), 0.2f);
    }

    public override UIMenuType GetUIType()
    {
        return _uiType;
    }
}
