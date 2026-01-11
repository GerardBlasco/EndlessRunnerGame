using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonController : MonoBehaviour, IPointerEnterHandler, ISelectHandler
{
    public AudioClip highlightSound;

    public void OnPointerEnter(PointerEventData eventData)
    {
        PlaySound();
    }

    public void OnSelect(BaseEventData eventData)
    {
        PlaySound();
    }

    private void PlaySound()
    {
        AudioManager.instance.PlaySFX(highlightSound);
    }
}
