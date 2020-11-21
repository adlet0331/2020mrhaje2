using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SymbolScript : MonoBehaviour, IPointerClickHandler
{
    private GameObject pieceUI;
    private GameObject card;

    public Sprite cardImage;
    public string cardName;
    public int maxHP;
    public string abilityName;
    public string abilityInformation;
    public int move;
    public int range;
    public int damage;

    public int deltaMove;
    public int deltaRange;
    public int deltaDamage;

    [SerializeField] private bool selected;

    // Start is called before the first frame update
    void Start() //PieceUI랑 Basecard gameObject받아오기 
    {
        pieceUI = GameObject.Find("PieceUI");
        card = pieceUI.transform.Find("Cards").transform.Find("BaseCard").gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!selected)
        {
            // 다른 모든 피스에 대해 PieceSelect(false) 실행
            Transform board = GameObject.Find("GameBoard").transform;
            foreach (Transform child in board)
            {
                foreach(Transform piece in child)
                {
                    if (piece.GetComponent<SymbolScript>() == null) continue;
                    piece.GetComponent<SymbolScript>().PieceSelect(false);
                }
            }

            PieceSelect(true);
        }
        else
        {
            PieceSelect(false);
        }
    }

    public void PieceSelect(bool state) //피스 선택 (true면 이 데이터 집어넣고 false면 끄기)
    {
        if (state)
        {
            selected = true;
            pieceUI.GetComponent<UIControl>().selectedPiece = gameObject;
            card.transform.Find("Card Image").GetComponent<Image>().sprite = cardImage;
            card.transform.Find("Name").GetComponent<Text>().text = cardName;
            card.transform.Find("MaxHP").GetComponent<Text>().text = maxHP.ToString();
            card.transform.Find("Ability Name").GetComponent<Text>().text = abilityName;
            card.transform.Find("Ability Information").GetComponent<Text>().text = abilityInformation;
            card.transform.Find("Move").GetComponent<Text>().text = move.ToString() + "+" + deltaMove.ToString();
            card.transform.Find("Range").GetComponent<Text>().text = range.ToString() + "+" + deltaRange.ToString();
            card.transform.Find("Damage").GetComponent<Text>().text = damage.ToString() + "+" + deltaDamage.ToString();
            card.SetActive(true);
            pieceUI.transform.Find("Move Button").gameObject.SetActive(true);
            pieceUI.transform.Find("Attack Button").gameObject.SetActive(true);
            transform.Find("Highlight").gameObject.SetActive(true);
        }
        else
        {
            selected = false;
            card.SetActive(false);
            pieceUI.transform.Find("Move Button").gameObject.SetActive(false);
            pieceUI.transform.Find("Attack Button").gameObject.SetActive(false);
            transform.Find("Highlight").gameObject.SetActive(false);
        }
    }
}
