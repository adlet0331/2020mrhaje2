using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SymbolScript : MonoBehaviour, IPointerClickHandler
{
    private UIControl pieceUI;
    private GameObject card;
    private SymbolInfo symbolInfo;

    public enum NameToNum { Infatry, Sniper, MachineGunner }
    public NameToNum cardName;
    public int team;

    // 능력치 보정 값
    public int deltaMove;
    public int deltaRange;
    public int deltaDamage;

    [SerializeField] private bool selected;

    // Start is called before the first frame update
    void Start()
    {
        // PieceUI, Basecard, Symbol Information 받아오기
        pieceUI = GameObject.Find("PieceUI").GetComponent<UIControl>();
        card = pieceUI.transform.Find("Cards").transform.Find("BaseCard").gameObject;
        symbolInfo = GameObject.Find("Symbol Information").GetComponent<SymbolInfo>();
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
            foreach (SymbolScript symbol in pieceUI.allSymbols) symbol.PieceSelect(false);

            // 턴이 맞아야만 유닛이 선택됨
            if (GameObject.Find("Turn Manager").GetComponent<TurnManager>().turn == team) PieceSelect(true);
        }
        else
        {
            PieceSelect(false);
        }
    }

    // 피스 선택 (true면 이 데이터 집어넣고 false면 끄기)
    public void PieceSelect(bool state)
    {
        if (state)
        {
            selected = true;
            pieceUI.selectedPiece = gameObject;

            // card 관련 처리
            {
                card.transform.Find("Card Image").GetComponent<Image>().sprite = symbolInfo.cardInfos[(int)cardName].cardImage;
                card.transform.Find("Name").GetComponent<Text>().text = symbolInfo.cardInfos[(int)cardName].cardName;
                card.transform.Find("MaxHP").GetComponent<Text>().text = symbolInfo.cardInfos[(int)cardName].maxHP.ToString();
                card.transform.Find("Ability Name").GetComponent<Text>().text = symbolInfo.cardInfos[(int)cardName].abilityName;
                card.transform.Find("Ability Information").GetComponent<Text>().text = symbolInfo.cardInfos[(int)cardName].abilityInformation;
                card.transform.Find("Move").GetComponent<Text>().text = symbolInfo.cardInfos[(int)cardName].move.ToString() + "+" + deltaMove.ToString();
                card.transform.Find("Range").GetComponent<Text>().text = symbolInfo.cardInfos[(int)cardName].range.ToString() + "+" + deltaRange.ToString();
                card.transform.Find("Damage").GetComponent<Text>().text = symbolInfo.cardInfos[(int)cardName].damage.ToString() + "+" + deltaDamage.ToString();
                card.SetActive(true);
            }

            pieceUI.transform.Find("Move Button").gameObject.SetActive(true);
            pieceUI.transform.Find("Attack Button").gameObject.SetActive(true);
            transform.Find("Highlight").gameObject.SetActive(true);
        }
        else
        {
            selected = false;
            pieceUI.selectedPiece = null;

            card.SetActive(false);

            pieceUI.transform.Find("Move Button").gameObject.SetActive(false);
            pieceUI.transform.Find("Attack Button").gameObject.SetActive(false);
            transform.Find("Highlight").gameObject.SetActive(false);
        }
        pieceUI.moveSelected = false;
        pieceUI.attackSelected = false;
    }

    public void Move(GameBoard board)
    {
        Debug.Log("Move");
        if (Mathf.Abs(transform.parent.GetComponent<GameBoard>().index - board.index) <= symbolInfo.cardInfos[(int)cardName].move + deltaMove)
        {
            transform.SetParent(board.transform);
        }
        pieceUI.moveSelected = false;
    }
}
