using UnityEngine;
using UnityEngine.EventSystems;

public class GameBoard : MonoBehaviour, IPointerClickHandler
{
    public int index;
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(index);
    }
}
