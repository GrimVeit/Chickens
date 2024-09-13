using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePanel : Panel
{
    [SerializeField] protected Vector3 from;
    [SerializeField] protected Vector3 to;
    [SerializeField] protected float time;
    [SerializeField] protected CanvasGroup canvasGroup;
    protected Tween tween;

    public override void ActivatePanel()
    {
        if (tween != null) { tween.Kill(); }

        panel.SetActive(true);
        tween = panel.transform.DOLocalMove(to, time);
        CanvasGroupAlpha(canvasGroup, 0, 1, time);
    }

    public override void DeactivatePanel()
    {
        if (tween != null) { tween.Kill(); }

        tween = panel.transform.DOLocalMove(from, time).OnComplete(() => panel.SetActive(false));
        CanvasGroupAlpha(canvasGroup, 1, 0, time);
    }

    private void CanvasGroupAlpha(CanvasGroup canvasGroup, float from, float to, float time)
    {
        Coroutines.Start(SmoothVal(canvasGroup, from, to, time));
    }

    private IEnumerator SmoothVal(CanvasGroup canvasGroup, float from, float to, float timer)
    {
        float t = 0.0f;
        canvasGroup.alpha = from;

        while (t < 1.0f)
        {
            t += Time.deltaTime * (1.0f / timer);
            if (canvasGroup != null)
                canvasGroup.alpha = Mathf.Lerp(from, to, t);
            yield return 0;
        }
    }
}
