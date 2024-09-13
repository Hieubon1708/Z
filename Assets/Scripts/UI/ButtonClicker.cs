using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonClicker : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public virtual void OnPointerDown(PointerEventData eventData)
    {
        ScaleButton(0.95f, 0.05f);
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        ScaleButton(1f, 0.05f);
        BlockController.instance.CheckButtonStateAll();
    }

    void ScaleButton(float value, float duration)
    {
        transform.DOKill();
        transform.DOScale(value, duration).SetEase(Ease.Linear);
    }
}
