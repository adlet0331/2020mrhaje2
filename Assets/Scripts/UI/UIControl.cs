using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIControl : MonoBehaviour
{
    public GameObject selectedPiece;
    public SymbolScript[] allSymbols;

    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject() == true && Input.GetMouseButtonDown(0))
        {
            foreach(SymbolScript symbolScript in allSymbols)
            {
                symbolScript.PieceSelect(false);
            }
        }
    }

    public void MovePiece()
    {

    }
}
