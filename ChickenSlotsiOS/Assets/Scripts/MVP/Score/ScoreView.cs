using DG.Tweening;
using TMPro;
using UnityEngine;

public class ScoreView : View
{
    [Header("Score")]
    [SerializeField] private TextMeshProUGUI textCoins;
    [SerializeField] private GameObject display100;
    [SerializeField] private GameObject display10;
    [SerializeField] private GameObject display1000;

    [Header("Health")]
    [SerializeField] private Transform parentEggsHealth;
    [SerializeField] private GameObject healthPrefab;

    private Vector3 defaultDisplay10Size;
    private Vector3 defaultDisplay100Size;
    private Vector3 defaultDisplay1000Size;

    public void Initialize()
    {
        defaultDisplay10Size = display10.transform.localScale;
        defaultDisplay100Size = display100.transform.localScale;
        defaultDisplay1000Size = display1000.transform.localScale;
    }

    public void Dispose()
    {

    }

    #region Score

    public void DisplayCoins(int coins)
    {
        textCoins.text = coins.ToString();
    }

    public void DisplayWin(int coins)
    {
        switch (coins)
        {
            case 10:
                display10.transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.1f).
                    OnComplete(() => display10.transform.DOScale(defaultDisplay10Size, 0.2f));
                break;
            case 100:
                display100.transform.DOScale(new Vector3(1.3f, 1.3f, 1.3f), 0.1f).
                    OnComplete(() => display100.transform.DOScale(defaultDisplay100Size, 0.25f));
                break;
            case 1000:
                display1000.transform.DOScale(new Vector3(1.6f, 1.6f, 1.6f), 0.1f).
                    OnComplete(() => display1000.transform.DOScale(defaultDisplay1000Size, 0.3f));
                break;

        }
    }

    #endregion

    #region Health

    public void AddHealth(int countValue)
    {
        for (int i = 0; i < countValue; i++)
        {
            Instantiate(healthPrefab, parentEggsHealth);
        }
    }

    public void RemoveHealth()
    {
        Destroy(parentEggsHealth.GetChild(parentEggsHealth.childCount - 1).gameObject);
    }

    #endregion
}
