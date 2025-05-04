using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIScrollViewFitter : MonoBehaviour
{
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private float scrollDuration = 0.25f;
    public void EnsureVisibleSmooth(RectTransform target)
    {
        Canvas.ForceUpdateCanvases();

        RectTransform content = scrollRect.content;
        RectTransform viewport = scrollRect.viewport;

        Vector3[] targetCorners = new Vector3[4];
        Vector3[] viewportCorners = new Vector3[4];

        target.GetWorldCorners(targetCorners);
        viewport.GetWorldCorners(viewportCorners);

        float targetTopY = GetTopY(target);
        float viewportTopY = GetTopY(viewport);

        float delta = viewportTopY - targetTopY;

        if (Mathf.Abs(delta) > 1f)
        {
            Vector2 newAnchoredPos = content.anchoredPosition + new Vector2(0f, delta / scrollRect.content.lossyScale.y);
            content.DOAnchorPos(newAnchoredPos, scrollDuration).SetEase(Ease.OutCubic);
        }

        float GetTopY(RectTransform rt)
        {
            Vector3[] corners = new Vector3[4];
            rt.GetWorldCorners(corners);
            return corners[1].y;
        }
    }
}
