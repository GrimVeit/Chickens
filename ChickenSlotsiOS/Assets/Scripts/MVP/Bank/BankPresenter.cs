using System;

public class BankPresenter : IMoneyProvider
{
    private BankModel bankModel;
    private BankView bankView;

    public BankPresenter(BankModel bankModel, BankView bankView)
    {
        this.bankModel = bankModel;
        this.bankView = bankView;
    }

    public void Initialize()
    {
        bankModel.Initialize();
        bankView.Initialize();

        bankModel.OnAddMoney += bankView.AddMoney;
        bankModel.OnRemoveMoney += bankView.RemoveMoney;
        bankModel.OnChangeMoney += bankView.SendMoneyDisplay;

        bankView.SendMoneyDisplay(bankModel.Money);
    }

    public void Dispose()
    {
        bankModel.OnAddMoney -= bankView.AddMoney;
        bankModel.OnRemoveMoney -= bankView.RemoveMoney;
        bankModel.OnChangeMoney -= bankView.SendMoneyDisplay;

        bankModel.Destroy();
    }

    public void SendMoney(int money)
    {
        bankModel.SendMoney(money);
    }

    public void SendMoney(float money)
    {
        bankModel.SendMoney(money);
    }

    public bool CanAfford(int bet)
    {
        return bankModel.CanAfford(bet);
    }

    public float GetMoney() => bankModel.Money;

    public event Action<float> OnChangeMoney
    {
        add { bankModel.OnChangeMoney += value; }
        remove { bankModel.OnChangeMoney -= value; }
    }
}

public interface IMoneyProvider
{
    float GetMoney();

    event Action<float> OnChangeMoney;
    void SendMoney(float money);
    bool CanAfford(int money);
}


