using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIControl : MonoBehaviour
{
    public GameObject selectedPiece;
    public SymbolScript[] allSymbols;

    private GameObject moveButton;
    private GameObject attackButton;

    public bool moveSelected;
    public bool attackSelected;

    private void Start()
    {
        moveButton = transform.Find("Move Button").gameObject;
        attackButton = transform.Find("Attack Button").gameObject;
    }

    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject() == true && Input.GetMouseButtonDown(1))
        {
            foreach (SymbolScript symbolScript in allSymbols)
            {
                symbolScript.PieceSelect(false);
            }
        }

        moveSelected = moveButton == EventSystem.current.currentSelectedGameObject;
        attackSelected = attackButton == EventSystem.current.currentSelectedGameObject;
    }

    /*public void MoveClick()
    {
        moveSelected = true;
        attackSelected = false;
    }

    public void AttackClick()
    {
        moveSelected = false;
        attackSelected = true;
    }*/
}
