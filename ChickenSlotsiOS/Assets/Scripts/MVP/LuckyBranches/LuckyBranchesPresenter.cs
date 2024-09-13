using System;

public class LuckyBranchesPresenter
{
    private LuckyBranchesModel luckyBranchesModel;
    private LuckyBranchesView luckyBranchesView;

    public LuckyBranchesPresenter(LuckyBranchesModel luckyBranchesModel, LuckyBranchesView luckyBranchesView)
    {
        this.luckyBranchesModel = luckyBranchesModel;
        this.luckyBranchesView = luckyBranchesView;
    }

    public void Initialize()
    {
        luckyBranchesView.Initialize();

        ActivateEvents();
    }

    public void Dispose()
    {
        DeactivateEvents();

        luckyBranchesView.Dispose();
    }

    private void ActivateEvents()
    {
        luckyBranchesView.OnClickBranch += luckyBranchesModel.OpenBranch;

        luckyBranchesModel.OnOpenBranch += luckyBranchesView.OpenCurrentBranch;
        luckyBranchesModel.OnCloseBranch += luckyBranchesView.CloseCurrentBranch;

        luckyBranchesModel.OnAddLeaf += luckyBranchesView.AddLeaf;
        luckyBranchesModel.OnRemoveLeaf += luckyBranchesView.RemoveLeaf;
    }

    private void DeactivateEvents()
    {
        luckyBranchesView.OnClickBranch -= luckyBranchesModel.OpenBranch;

        luckyBranchesModel.OnOpenBranch -= luckyBranchesView.OpenCurrentBranch;
        luckyBranchesModel.OnCloseBranch -= luckyBranchesView.CloseCurrentBranch;

        luckyBranchesModel.OnAddLeaf += luckyBranchesView.AddLeaf;
        luckyBranchesModel.OnRemoveLeaf += luckyBranchesView.RemoveLeaf;
    }

    #region Input Actions

    public event Action<int> OnGetMoney
    {
        add { luckyBranchesModel.OnGetMoney += value; }
        remove { luckyBranchesModel.OnGetMoney -= value; }
    }

    public event Action OnFailGame
    {
        add { luckyBranchesModel.OnFailGame += value; }
        remove { luckyBranchesModel.OnFailGame -= value; }
    }

    #endregion
}
