using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Branch : MonoBehaviour
{
    public bool isOpened { get; private set; }
    public event Action<int> OnClickBranch;

    [SerializeField] private Button branchButton;
    [SerializeField] private Image imageBranch;
    [SerializeField] private Image imageBranchOutcome;
    [SerializeField] private Sprite openBranch;
    [SerializeField] private Sprite closeBranch;

    private BranchOutcomeData branchOutcomeData;

    private Color noneColor = new Color(0, 0, 0, 0);
    private Color normalColor = Color.white;

    private int branchIndex;

    public void Initialize(int index)
    {
        branchIndex = index;
        branchButton.onClick.AddListener(HandlerClickToBranch);
    }

    public void Dispose()
    {
        branchButton?.onClick.RemoveListener(HandlerClickToBranch);
    }

    public void SetBranchOutcomeData(BranchOutcomeData branchOutcomeData)
    {
        this.branchOutcomeData = branchOutcomeData;
    }

    public void OpenBranch()
    {
        isOpened = true;

        imageBranchOutcome.color = normalColor;

        if(branchOutcomeData.Sprite == null)
            imageBranchOutcome.color = noneColor;

        imageBranchOutcome.sprite = branchOutcomeData.Sprite;

        imageBranch.sprite = openBranch;
    }

    public void CloseBranch()
    {
        isOpened = false;

        imageBranchOutcome.color = noneColor;

        imageBranch.sprite = closeBranch;
    }

    private void HandlerClickToBranch()
    {
        OnClickBranch?.Invoke(branchIndex);
    }

}
