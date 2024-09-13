using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class LuckyBranchesModel
{
    public event Action OnFailGame;

    public event Action<int> OnGetMoney;

    public event Action OnAddLeaf;
    public event Action OnRemoveLeaf;

    public event Action<BranchOutcomeData> OnOpenBranch;
    public event Action OnCloseBranch;

    private BranchOutcomeDataContainer branchOutcomeDataContainer;
    private BranchOutcomeData currentBranchOutcomeData;
    private MiniGameCoinNominals coinNominals;
    private Dictionary<BranchOutcome, Action> branchOutcomesActions = new Dictionary<BranchOutcome, Action>();

    private IParticleEffectProvider particleEffectProvider;
    private ISoundProvider soundProvider;

    private IEnumerator timeToCloseBranch_Coroutine;

    private bool isOpenedBranch = false;

    private float timeDuration = 1f;

    private int currentCoinNominalIndex = -1;
    private int countLeaf = 0;

    private int currentBranchIndex;

    public LuckyBranchesModel(BranchOutcomeDataContainer branchOutcomeDataContainer, MiniGameCoinNominals coinNominals, ISoundProvider soundProvider, IParticleEffectProvider particleEffectProvider)
    {
        this.branchOutcomeDataContainer = branchOutcomeDataContainer;
        this.coinNominals = coinNominals;

        this.soundProvider = soundProvider;
        this.particleEffectProvider = particleEffectProvider;

        branchOutcomesActions[BranchOutcome.None] = HandleNothing;
        branchOutcomesActions[BranchOutcome.Leaf] = HandleLeaf;
        branchOutcomesActions[BranchOutcome.Parrot] = HandleParrot;
        branchOutcomesActions[BranchOutcome.Coin] = HandleCoin;
    }

    public void OpenBranch(int index)
    {
        if (isOpenedBranch)
        {
            soundProvider.PlayOneShot("Error");
            return;
        }

        isOpenedBranch = true;
        currentBranchIndex = index;

        soundProvider.PlayOneShot("BranchOpen");
        currentBranchOutcomeData = GetRandomBranchOutcomeData();
        branchOutcomesActions[currentBranchOutcomeData.Outcome]?.Invoke();
        OnOpenBranch?.Invoke(currentBranchOutcomeData);

        ActivateCoroutineTimeToCloseBranch();
    }

    private void CloseBranch()
    {
        soundProvider.PlayOneShot("BranchClose");
        OnCloseBranch?.Invoke();

        isOpenedBranch = false;
    }

    private void ActivateCoroutineTimeToCloseBranch()
    {
        if(timeToCloseBranch_Coroutine != null)
        {
            Coroutines.Stop(TimeToCloseBranch_Coroutine());
        }
        Coroutines.Start(TimeToCloseBranch_Coroutine());
    }

    private IEnumerator TimeToCloseBranch_Coroutine()
    {
        yield return new WaitForSeconds(timeDuration);

        CloseBranch();
    }

    private BranchOutcomeData GetRandomBranchOutcomeData()
    {
        int randomIndex = UnityEngine.Random.Range(0, branchOutcomeDataContainer.branchOutcomeDatas.Count);
        return branchOutcomeDataContainer.branchOutcomeDatas[randomIndex];
    }

    private void HandleNothing()
    {
        //soundProvider.PlayOneShot("");
    }

    private void HandleCoin()
    {
        particleEffectProvider.Play("Branch_" + currentBranchIndex);

        if (currentCoinNominalIndex < coinNominals.Nominals.Count - 1)
        {
            currentCoinNominalIndex += 1;

            OnGetMoney?.Invoke(coinNominals.Nominals[currentCoinNominalIndex]);

            return;
        }
        OnGetMoney?.Invoke(coinNominals.Nominals[coinNominals.Nominals.Count - 1]);

    }

    private void HandleLeaf()
    {
        //soundProvider.PlayOneShot("");
        //particleEffectProvider.Play("");

        if (countLeaf >= 3) return;

        countLeaf += 1;

        OnAddLeaf?.Invoke();
    }

    private void HandleParrot()
    {
        if (countLeaf >= 1)
        {
            //soundProvider.PlayOneShot("");
            //particleEffectProvider.Play("");

            OnRemoveLeaf?.Invoke();
            countLeaf -= 1;
            return;
        }

        //soundProvider.PlayOneShot("");
        //particleEffectProvider.Play("");

        Debug.Log("Проигрыш");
        OnFailGame?.Invoke();
    }
}
