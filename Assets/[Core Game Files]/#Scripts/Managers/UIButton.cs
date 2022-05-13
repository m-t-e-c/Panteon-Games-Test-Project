using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UIButton : MonoBehaviour
{
    // Reference
    private Button _button;
    
    // Private
    [SerializeField] private string _buttonCommand;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(ClickEvent);
    }

    // Called when the button clicked.
    public void ClickEvent()
    {
        UIManager.OnCommandExecuted?.Invoke(_buttonCommand);
    }
}
