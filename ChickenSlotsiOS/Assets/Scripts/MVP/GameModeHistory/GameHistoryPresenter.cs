using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHistoryPresenter
{
    private GameHistoryModel gameHistoryModel;

    public GameHistoryPresenter(GameHistoryModel gameHistoryModel)
    {
        this.gameHistoryModel = gameHistoryModel;
    }

    public void Initialize()
    {
        gameHistoryModel.Initialize();
    }

    public void Dispose()
    {
        gameHistoryModel.Dispose();
    }

    #region Input

    public event Action OnNoneLastGame
    {
        add { gameHistoryModel.OnNoneLastGame += value; }
        remove { gameHistoryModel.OnNoneLastGame += value; }
    }

    public event Action OnLastGameIsArcada
    {
        add { gameHistoryModel.OnLastGameIsArcada += value; }
        remove { gameHistoryModel.OnLastGameIsArcada -= value; }
    }

    public event Action OnLastGameIsCampaign
    {
        add { gameHistoryModel.OnLastGameIsCampaign += value; }
        remove { gameHistoryModel.OnLastGameIsCampaign -= value; }
    }

    public void SetCurrentTypeGame(TypeGame typeGame)
    {
        gameHistoryModel.SetCurrentTypeGame(typeGame);
    }

    public void DeleteHistory()
    {
        gameHistoryModel.DeleteHistory();
    }

    #endregion
}
