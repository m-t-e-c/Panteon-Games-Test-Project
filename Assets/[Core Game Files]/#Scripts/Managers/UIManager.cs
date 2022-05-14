using System;
using System.Collections.Generic;
using UnityEngine;

public enum UIMenuType { None, MainMenu, Pause, Gameplay, Win, Lose }

public class UIManager : MonoBehaviour
{
    // Events
    public static Action<UIMenuType, UIElement> OnRegisterUI;
    public static Action<string> OnCommandExecuted;

    // UI Element List
    private Dictionary<UIMenuType, UIElement> _uiElements = new Dictionary<UIMenuType, UIElement>();

    [SerializeField] private UIMenuType _startUI = UIMenuType.MainMenu;

    #region Unity Methods

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


    private void OnEnable()
    {
        OnRegisterUI += m_OnRegisterUI;
        OnCommandExecuted += m_OnCommandExecuted;
    }

    private void OnDisable()
    {
        OnRegisterUI -= m_OnRegisterUI;
        OnCommandExecuted -= m_OnCommandExecuted;
    }

    #endregion

    #region UIManager Methods
    // It executes commands according to incoming string.
    private void m_OnCommandExecuted(string command)
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
                ResetUI();
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
    private void m_OnRegisterUI(UIMenuType menuType, UIElement element)
    {
        if (name != null && element != null)
            if (!_uiElements.ContainsKey(menuType))
            {
                //Debug.Log("UI Registered --> " + element);
                _uiElements.Add(menuType, element);
            }
    }


    // If we have valid uiType, it will open ui.
    private void OpenUI(UIMenuType uiType)
    {
        UIElement uiElement = null;
        if (_uiElements.TryGetValue(uiType, out uiElement))
        {
            uiElement.ShowUI();
        }
    }


    // If we have valid uiType, it will close ui.
    private void CloseUI(UIMenuType uiType)
    {
        UIElement uiElement = null;
        if (_uiElements.TryGetValue(uiType, out uiElement))
        {
            uiElement.HideUI();
        }
    }

    // Reseting all UI element to default state.
    private void ResetUI()
    {
        foreach (UIElement element in _uiElements.Values)
        {
            if (element.GetUIType() == UIMenuType.MainMenu)
                element.ShowUI();

            element.HideUI();
        }
    }
    #endregion
}
