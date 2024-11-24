using System;
using UnityEngine;
using UnityEngine.UI;

public class GameTrackerConstruct :MonoBehaviour
{
    public event Action<int, int> OnChooseLevel;

    public int Level => level;

    [SerializeField] private int level;
    [SerializeField] private int typeGame;
    [SerializeField] private Button buttonPlay_Chicken;
    [SerializeField] private Button buttonPlay_AvailableEgg;
    [SerializeField] private GameObject object_UnavailableEgg;
    [SerializeField] private GameObject objectTextEgg;
    [SerializeField] private GameObject objectTextChicken;

    public void Initialize()
    {
        buttonPlay_Chicken.onClick.AddListener(HandlerClickToChooseLevelButton);
        buttonPlay_AvailableEgg.onClick.AddListener(HandlerClickToChooseLevelButton);
    }

    public void Dispose()
    {
        buttonPlay_Chicken.onClick.RemoveListener(HandlerClickToChooseLevelButton);
        buttonPlay_AvailableEgg.onClick.RemoveListener(HandlerClickToChooseLevelButton);
    }

    public void AvailableLevel()
    {
        buttonPlay_AvailableEgg.gameObject.SetActive(false);
        buttonPlay_Chicken.gameObject.SetActive(true);
        object_UnavailableEgg.SetActive(false);

        objectTextChicken.SetActive(true);
        objectTextEgg.SetActive(false);
    }

    public void UnavailableLevel()
    {
        buttonPlay_AvailableEgg.gameObject.SetActive(false);
        buttonPlay_Chicken.gameObject.SetActive(false);
        object_UnavailableEgg.SetActive(true);

        objectTextChicken.SetActive(false);
        objectTextEgg.SetActive(true);
        objectTextEgg.transform.SetParent(object_UnavailableEgg.transform);
    }

    public void CurrentLevel()
    {
        buttonPlay_AvailableEgg.gameObject.SetActive(true);
        buttonPlay_Chicken.gameObject.SetActive(false);
        object_UnavailableEgg.SetActive(false);

        objectTextChicken.SetActive(false);
        objectTextEgg.SetActive(true);
        objectTextEgg.transform.SetParent(buttonPlay_AvailableEgg.transform);
    }

    #region Input

    private void HandlerClickToChooseLevelButton()
    {
        OnChooseLevel?.Invoke(level, typeGame);
    }

    #endregion
}


