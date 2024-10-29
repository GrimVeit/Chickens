using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DynamicScroll : MonoBehaviour
{
    [SerializeField] private ScrollRect scrollRect;

    [SerializeField] private Transform centerPoint;
    [SerializeField] private Transform content;
    [SerializeField] private List<GameIcon> gameIcons = new List<GameIcon>();

    private IEnumerator smoothScroll_IEnumerator;

    private GameIcon currentGameIcon;

    private void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        for (int i = 0; i < gameIcons.Count; i++)
        {
            gameIcons[i].OnSelect += StartSmoothScroll;
            gameIcons[i].Initialize();
        }

        SelectCurrentGameIcon(gameIcons[0]);
    }

    public void Dispose()
    {
        for (int i = 0; i < gameIcons.Count; i++)
        {
            gameIcons[i].OnSelect -= StartSmoothScroll;
            gameIcons[i].Dispose();
        }
    }

    private void SelectCurrentGameIcon(GameIcon gameIcon)
    {
        currentGameIcon?.Deselect();
        currentGameIcon = gameIcon;
        currentGameIcon.Select();
    }

    private void StartSmoothScroll(GameIcon gameIcon)
    {
        //StopFindScrollGameIcon();

        SelectCurrentGameIcon(gameIcon);

        if (smoothScroll_IEnumerator != null)
            StopCoroutine(smoothScroll_IEnumerator);

        smoothScroll_IEnumerator = SmoothScrollToItem(currentGameIcon.RectTransform);
        StartCoroutine(smoothScroll_IEnumerator);
    }

    private IEnumerator SmoothScrollToItem(RectTransform item)
    {
        float distance = item.position.x - centerPoint.position.x;
        Vector3 targetPosition = content.position + new Vector3(-distance, 0, 0);
        float elapsedTime = 0f;
        float smoothDuration = 0.3f;


        while (elapsedTime < smoothDuration)
        {
            elapsedTime += Time.deltaTime;
            content.position = Vector3.Lerp(content.position, targetPosition, elapsedTime / smoothDuration);
            yield return null;
        }

        yield return null;
        scrollRect.velocity = Vector2.zero;
        content.position = targetPosition;
    }
}
