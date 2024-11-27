using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameProgressView : View, IGameProgressView
{
    public event Action OnClickToSecondGame;

    [SerializeField] private Button buttonSecondGame;

    public void Initialize()
    {
        buttonSecondGame.onClick.AddListener(HandlerClickToSecondGame);
    }

    public void Dispose()
    {
        buttonSecondGame.onClick.RemoveListener(HandlerClickToSecondGame);
    }

    public void ActivateSecondGameButton()
    {
        buttonSecondGame.gameObject.SetActive(true);
    }

    public void DeactivateSecondGameButton()
    {
        buttonSecondGame.gameObject.SetActive(false);
    }

    #region Input

    private void HandlerClickToSecondGame()
    {
        OnClickToSecondGame?.Invoke();
    }

    #endregion
}
