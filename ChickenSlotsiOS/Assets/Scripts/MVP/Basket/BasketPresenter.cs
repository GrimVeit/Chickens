using System;

public class BasketPresenter
{
    private IBasketModel basketModel;
    private IBasketView basketView;

    public BasketPresenter(BasketModel basketModel, IBasketView basketView)
    {
        this.basketModel = basketModel;
        this.basketView = basketView;
    }

    public void Initialize()
    {
        ActivateEvents();

        basketModel.Initialize();
        basketView.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        basketView.Dispose();
    }

    private void ActivateEvents()
    {
        basketView.OnSetPositionIndex += basketModel.SetPositionIndex;
        basketView.OnSetLeft += basketModel.MoveLeftIndex;
        basketView.OnSetRight += basketModel.MoveRightIndex;

        basketModel.OnMoveIndex += basketView.MoveToIndex;
    }

    private void DeactivateEvents()
    {
        basketView.OnSetPositionIndex -= basketModel.SetPositionIndex;
        basketView.OnSetLeft -= basketModel.MoveLeftIndex;
        basketView.OnSetRight -= basketModel.MoveRightIndex;

        basketModel.OnMoveIndex -= basketView.MoveToIndex;
    }

    #region Input

    public void Start()
    {
        basketModel.Activate();
    }

    public void Stop()
    {
        basketModel.Deactivate();
    }

    #endregion
}

public interface IBasketModel
{
    public event Action<int> OnMoveIndex;

    void Initialize();
    void Dispose();
    void Activate();
    void Deactivate();
    void MoveLeftIndex();
    void MoveRightIndex();
    void SetPositionIndex(int index);
}

public interface IBasketView
{
    public event Action<int> OnSetPositionIndex;
    public event Action OnSetLeft;
    public event Action OnSetRight;

    void Initialize();
    void Dispose();
    void MoveToIndex(int index);
}
