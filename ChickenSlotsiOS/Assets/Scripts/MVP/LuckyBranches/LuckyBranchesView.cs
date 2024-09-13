using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuckyBranchesView : View
{
    public event Action<int> OnClickBranch;

    [SerializeField] private List<Branch> branches = new List<Branch>();
    [SerializeField] private Transform contentScrollViewTransform;
    [SerializeField] private GameObject leafPrefab;

    private int currentBranchIndex = -1;

    public void Initialize()
    {
        for (int i = 0; i < branches.Count; i++)
        {
            branches[i].Initialize(i);
            branches[i].OnClickBranch += ClickBranch;
        }
    }

    public void Dispose()
    {
        for (int i = 0; i < branches.Count; i++)
        {
            branches[i].OnClickBranch -= ClickBranch;
            branches[i].Dispose();
        }
    }

    public void OpenCurrentBranch(BranchOutcomeData branchOutcomeData)
    {
        branches[currentBranchIndex].SetBranchOutcomeData(branchOutcomeData);
        branches[currentBranchIndex].OpenBranch();
    }

    public void CloseCurrentBranch()
    {
        branches[currentBranchIndex].CloseBranch();
    }

    //private void ClickBranch(Branch branch)
    //{
    //    if(this.branch == null)
    //    {
    //        this.branch = branch;
    //        OnClickBranch?.Invoke();
    //        return;
    //    }

    //    if(!this.branch.isOpened)
    //       this.branch = branch;

    //    OnClickBranch?.Invoke();
    //}

    private void ClickBranch(int branch)
    {
        if (this.currentBranchIndex == -1)
        {
            this.currentBranchIndex = branch;
            OnClickBranch?.Invoke(currentBranchIndex);
            return;
        }

        if (!branches[currentBranchIndex].isOpened)
            this.currentBranchIndex = branch;

        OnClickBranch?.Invoke(currentBranchIndex);
    }

    public void AddLeaf()
    {
        Instantiate(leafPrefab, contentScrollViewTransform);
    }

    public void RemoveLeaf()
    {
        Destroy(contentScrollViewTransform.GetChild(contentScrollViewTransform.childCount - 1).gameObject);
    }
}
