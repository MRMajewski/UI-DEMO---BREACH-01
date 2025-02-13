using UnityEngine;
using UnityEngine.EventSystems;

public class UISelectableSFX : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
    [SerializeField]
    private string onClickSFX;

    [SerializeField]
    private string onPointerEnterSFX;

    public void OnPointerClick(PointerEventData eventData)
    {
        MusicManager.Instance.PlaySound(onClickSFX); 
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        MusicManager.Instance.PlaySound(onPointerEnterSFX); 
    }
}
