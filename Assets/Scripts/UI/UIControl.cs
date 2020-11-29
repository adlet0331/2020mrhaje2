using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIControl : MonoBehaviour
{
    public GameObject selectedPiece;
    public SymbolScript[] allSymbols;
    private TurnManager turnManager;

    private GameObject moveButton;
    private GameObject attackButton;

    public bool moveSelected;
    public bool attackSelected;
    
    private void Start()
    {
        moveButton = transform.Find("Move Button").gameObject;
        attackButton = transform.Find("Attack Button").gameObject;
        turnManager = GameObject.Find("Turn Manager").GetComponent<TurnManager>();
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

        if (selectedPiece != null)
        {
            if (!selectedPiece.GetComponent<SymbolScript>().moved && !selectedPiece.GetComponent<SymbolScript>().attacked && turnManager.actedUnit == 3)
            {
                moveButton.SetActive(false);
                attackButton.SetActive(false);
            }
            else
            {
                moveButton.SetActive(!selectedPiece.GetComponent<SymbolScript>().moved);
                attackButton.SetActive(!selectedPiece.GetComponent<SymbolScript>().attacked);
            }
        }
    }
}
