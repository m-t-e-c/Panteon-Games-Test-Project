using System.Collections.Generic;
using UnityEngine;

public enum UIMenuType { None, MainMenu, Pause, Gameplay, Win, Lose }

public class UIManager : MonoBehaviour
{
    private Dictionary<UIMenuType, UIElement> _uiElements = new Dictionary<UIMenuType, UIElement>();

    [SerializeField] private UIMenuType _startUI = UIMenuType.MainMenu;

    private static UIManager _instance;
    public static UIManager instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<UIManager>();

            return _instance;
        }
    }

    private void Start()
    {
        foreach (UIElement element in _uiElements.Values)
        {
            if (element.GetUIType() == _startUI)
                element.ShowUI();
            else
                element.HideUI();
        }
    }

    public void ExecuteCommand(string command)
    {
        if (string.IsNullOrEmpty(command))
            return;

        switch (command)
        {
            case "Show Pause Menu":
                OpenUI(UIMenuType.Pause);
                GameManager.instance.currentGameState = GameState.Paused;
                break;

            case "Close Pause Menu":
                CloseUI(UIMenuType.Pause);
                GameManager.instance.currentGameState = GameState.Playing;
                break;

            case "Show Win Menu":
                OpenUI(UIMenuType.Win);
                GameManager.instance.currentGameState = GameState.Finished;

                break;

            case "Show Lose Menu":
                OpenUI(UIMenuType.Lose);
                GameManager.instance.currentGameState = GameState.Finished;
                break;

            case "Show Main Menu":
                OpenUI(UIMenuType.MainMenu);
                GameManager.instance.currentGameState = GameState.Menu;
                break;

            case "Restart Level":
                LevelManager.instance.RestartLevel();
                GameManager.instance.currentGameState = GameState.Menu;
                break;

            case "Start Game":
                CloseUI(UIMenuType.MainMenu);
                OpenUI(UIMenuType.Gameplay);
                GameManager.instance.currentGameState = GameState.Playing;
                break;

            case "Lose Game":
                CloseUI(UIMenuType.Gameplay);
                OpenUI(UIMenuType.Lose);
                GameManager.instance.currentGameState = GameState.Finished;
                break;

            case "Win Game":
                CloseUI(UIMenuType.Gameplay);
                OpenUI(UIMenuType.Lose);
                GameManager.instance.currentGameState = GameState.Finished;
                break;
            default:
                Debug.LogError("There is no >> " + command + " << command. Please check your string.");
                break;
        }
    }

    // Registering UI Element to _uiElement dictionary so we can control it later.
    public void RegisterUI(UIMenuType menuType, UIElement element)
    {
        if (name != null && element != null)
            if (!_uiElements.ContainsKey(menuType))
            {
                Debug.Log("UI Registered --> " + element);
                _uiElements.Add(menuType, element);
            }
    }


    // If we have valid uiType, it will open ui.
    public void OpenUI(UIMenuType uiType)
    {
        UIElement uiElement = null;
        if (_uiElements.TryGetValue(uiType, out uiElement))
        {
            uiElement.ShowUI();
        }
    }


    // If we have valid uiType, it will close ui.
    public void CloseUI(UIMenuType uiType)
    {
        UIElement uiElement = null;
        if (_uiElements.TryGetValue(uiType, out uiElement))
        {
            uiElement.HideUI();
        }
    }
}
