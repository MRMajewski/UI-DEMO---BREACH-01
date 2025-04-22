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

        Vector3[] itemWorldCorners = new Vector3[4];
        Vector3[] viewWorldCorners = new Vector3[4];

        target.GetWorldCorners(itemWorldCorners);
        viewport.GetWorldCorners(viewWorldCorners);

        float itemTop = itemWorldCorners[1].y;
        float itemBottom = itemWorldCorners[0].y;
        float viewTop = viewWorldCorners[1].y;
        float viewBottom = viewWorldCorners[0].y;

        float delta = 0f;

        if (itemTop > viewTop)
        {
            delta = itemTop - viewTop;
        }
        else if (itemBottom < viewBottom)
        {
            delta = itemBottom - viewBottom;
        }

        if (Mathf.Abs(delta) > 1f)
        {
            Vector2 newPos = content.anchoredPosition;
            newPos.y -= delta;
            content.DOAnchorPos(newPos, scrollDuration).SetEase(Ease.OutCubic);
        }
    }
}
