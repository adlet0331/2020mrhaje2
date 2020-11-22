using UnityEngine;
using UnityEngine.EventSystems;

public class GameBoard : MonoBehaviour, IPointerDownHandler
{
    public int index;
    private UIControl PieceUI;

    private void Start()
    {
        PieceUI = GameObject.Find("PieceUI").GetComponent<UIControl>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;
        Debug.Log("Board Click " + index.ToString());
        if (PieceUI.moveSelected) PieceUI.selectedPiece.GetComponent<SymbolScript>().Move(this);
    }
}
