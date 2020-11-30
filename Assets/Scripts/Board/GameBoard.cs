using UnityEngine;
using UnityEngine.EventSystems;

public class GameBoard : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public int index;
    private UIControl PieceUI;
    private Vector3 previousPosition;

    private void Start()
    {
        PieceUI = GameObject.Find("PieceUI").GetComponent<UIControl>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        previousPosition = eventData.position;
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (PieceUI.moveSelected) PieceUI.selectedPiece.GetComponent<SymbolScript>().Move(this);
        }
        
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (!previousPosition.Equals(eventData.position)) return;
        if (eventData.button == PointerEventData.InputButton.Left) //클릭하면 UI 사라지게 
        {
            foreach (SymbolScript symbols in GameObject.Find("PieceUI").GetComponent<UIControl>().allSymbols)
            {
                symbols.PieceSelect(false);
            }
            return;
        }
    }
}
