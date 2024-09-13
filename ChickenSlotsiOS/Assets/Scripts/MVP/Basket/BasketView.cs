using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class BasketView : View
{
    [SerializeField] private TextMeshProUGUI textCoins;
    [SerializeField] private GameObject display100;
    [SerializeField] private GameObject display10;
    [SerializeField] private GameObject display1000;
    [SerializeField] private GameObject textDescription;
    [SerializeField] private Basket basket;
    private Canvas canvas;

    private Vector3 defaultDisplay10Size;
    private Vector3 defaultDisplay100Size;
    private Vector3 defaultDisplay1000Size;


    public void Initialize()
    {
        defaultDisplay10Size = display10.transform.localScale;
        defaultDisplay100Size = display100.transform.localScale;
        defaultDisplay1000Size = display1000.transform.localScale;

        canvas = GetComponentInParent<Canvas>();
        basket.Initialize();
        basket.OnMove += OnMove;
    }

    public void Dispose()
    {
        basket.OnMove -= OnMove;
    }

    public void StartMove()
    {
        textDescription.SetActive(false);
    }

    public void EndMove()
    {
        textDescription.SetActive(true);
    }

    public void Move(Vector2 vector)
    {
        basket.RectTransform.anchoredPosition += new Vector2(vector.x, 0);
    }

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

    #region Input

    private void OnMove(Vector2 vector)
    {
        OnMove_Action?.Invoke(vector / canvas.scaleFactor);
    }

    public event Action<Vector2> OnMove_Action;

    public event Action OnStartMove_Action
    {
        add { basket.OnStartMove += value; }
        remove { basket.OnStartMove -= value; }
    }
    
    public event Action OnEndMove_Action
    {
        add { basket.OnEndMove += value; }
        remove { basket.OnEndMove -= value; }
    }

    public event Action<EggValues> OnGetEgg_Action
    {
        add { basket.OnGetEggValues += value; }
        remove { basket.OnGetEggValues -= value; }
    }

    #endregion
}
