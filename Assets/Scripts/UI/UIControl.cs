using UnityEngine;
using UnityEngine.EventSystems;

public class UIControl : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("123");
    }
}
