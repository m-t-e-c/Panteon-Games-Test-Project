using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UIButton : MonoBehaviour
{
    private Button _button;
    
    [SerializeField] private string _buttonCommand;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(ClickEvent);
    }

    public void ClickEvent()
    {
        UIManager.instance.ExecuteCommand(_buttonCommand);
    }
}
