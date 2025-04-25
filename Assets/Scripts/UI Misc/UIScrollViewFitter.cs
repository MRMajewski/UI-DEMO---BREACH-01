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

        float itemTop = GetTopY(target);
        float viewTop = GetTopY(viewport);

        float delta = 0f;

        delta = viewTop - itemTop;

        if (Mathf.Abs(delta) > 1f)
        {
            Vector2 newPos = content.anchoredPosition;
            newPos.y += delta;
            content.DOAnchorPos(newPos, scrollDuration).SetEase(Ease.OutCubic);
        }

        float GetTopY(RectTransform rt)
        {
            Vector3[] corners = new Vector3[4];
            rt.GetWorldCorners(corners);
            return corners[1].y;
        }
    }
}
