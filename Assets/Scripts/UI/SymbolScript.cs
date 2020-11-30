using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SymbolScript : MonoBehaviour, IPointerClickHandler, IPointerDownHandler
{
    private UIControl pieceUI;
    private GameObject card;
    private SymbolInfo symbolInfo;
    private TurnManager turnManager;
    private MinimapSymbolScript miniSymbol;

    public enum NameToNum { Infatry, Sniper, MachineGunner }
    public NameToNum cardName;
    public int index;
    public int team;

    // 능력치
    public int currentHP;
    public int deltaMove;
    public int deltaRange;
    public int deltaDamage;

    public bool moved;
    public bool attacked;

    [SerializeField] private bool selected;

    void setCardName()
    {
        if (team == 1)
        {
            NameToNum cardname = SymbolInfo.Instance.BoardPieceInfo_Left[index];
            this.cardName = cardname;
            this.transform.GetChild(0).GetComponent<Image>().sprite = SymbolInfo.Instance.cardInfos[(int)cardname].pieceSprite_L;
        }
        if(team == 2)
        {
            NameToNum cardname = SymbolInfo.Instance.BoardPieceInfo_Right[index];
            this.cardName = cardname;
            this.transform.GetChild(0).GetComponent<Image>().sprite = SymbolInfo.Instance.cardInfos[(int)cardname].pieceSprite_R;
        }
        miniSymbol.setCardName(index, team, (int)cardName);
    }

    // Start is called before the first frame update
    void Start()
    {
        // PieceUI, Basecard, Symbol Information 받아오기
        pieceUI = GameObject.Find("PieceUI").GetComponent<UIControl>();
        card = pieceUI.transform.Find("Cards").transform.Find("BaseCard").gameObject;
        symbolInfo = GameObject.Find("Symbol Information").GetComponent<SymbolInfo>();
        turnManager = GameObject.Find("Turn Manager").GetComponent<TurnManager>();

        //미니맵에서 자기 자신 찾기
        miniSymbol = GameObject.Find("mini" + this.name).GetComponent<MinimapSymbolScript>();

        setCardName();

        // HP 초기화
        currentHP = symbolInfo.cardInfos[(int)cardName].maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        // HP 바 조정
        Vector3 barScale = transform.Find("Current HP Bar").localScale;
        barScale.x = (float)currentHP / symbolInfo.cardInfos[(int)cardName].maxHP;
        transform.Find("Current HP Bar").localScale = barScale;

        //미니맵 자신에게 정보 전달
        miniSymbol.getHP(currentHP);

        // HP가 0 이하가 되면 파괴
        if (currentHP <= 0)
        {
            PieceSelect(false);
            gameObject.SetActive(false);
        }
    }

    // 선택; 마우스를 뗄 때 실행됨
    public void OnPointerClick(PointerEventData eventData)
    {
        // 턴이 맞아야만 실행됨
        if (turnManager.turn != team) return;
        if (!selected)
        {
            // 다른 모든 피스에 대해 PieceSelect(false) 실행
            foreach (SymbolScript symbol in pieceUI.allSymbols) symbol.PieceSelect(false);
            PieceSelect(true);
        }
        else
        {
            PieceSelect(false);
        }
    }

    // 공격; 유닛을 누르는 순간 실행됨
    public void OnPointerDown(PointerEventData eventData)
    {
        if (pieceUI.attackSelected)
        {
            pieceUI.selectedPiece.GetComponent<SymbolScript>().Attack(this);
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
            card.transform.Find("Card Image").GetComponent<Image>().sprite = symbolInfo.cardInfos[(int)cardName].cardImage;
            card.transform.Find("Name").GetComponent<Text>().text = symbolInfo.cardInfos[(int)cardName].cardName;
            card.transform.Find("MaxHP").GetComponent<Text>().text = symbolInfo.cardInfos[(int)cardName].maxHP.ToString();
            card.transform.Find("Ability Name").GetComponent<Text>().text = symbolInfo.cardInfos[(int)cardName].abilityName;
            card.transform.Find("Ability Information").GetComponent<Text>().text = symbolInfo.cardInfos[(int)cardName].abilityInformation;
            card.transform.Find("Move").GetComponent<Text>().text = symbolInfo.cardInfos[(int)cardName].move + (deltaMove>=0?"+":"") + deltaMove;
            card.transform.Find("Range").GetComponent<Text>().text = symbolInfo.cardInfos[(int)cardName].range + (deltaRange >= 0 ? "+" : "") + deltaRange;
            card.transform.Find("Damage").GetComponent<Text>().text = symbolInfo.cardInfos[(int)cardName].damage + (deltaDamage >= 0 ? "+" : "") + symbolInfo.cardInfos[(int)cardName].damage * deltaDamage / 100;
            card.SetActive(true);

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
        if (Mathf.Abs(transform.parent.GetComponent<GameBoard>().index - board.index) <= symbolInfo.cardInfos[(int)cardName].move + deltaMove)
        {
            miniSymbol.Move(GameObject.Find("mini" + board.name));
            transform.SetParent(board.transform);
            if (!moved && !attacked) turnManager.actedUnit++;
            moved = true;
        }
        pieceUI.moveSelected = false;
        PieceSelect(false);
    }

    public void Attack(SymbolScript target)
    {
        if (Mathf.Abs(transform.parent.GetComponent<GameBoard>().index - target.transform.parent.GetComponent<GameBoard>().index)
            <= symbolInfo.cardInfos[(int)cardName].range + deltaRange)
        {
            target.currentHP -= symbolInfo.cardInfos[(int)cardName].damage + symbolInfo.cardInfos[(int)cardName].damage * deltaDamage / 100;
            if (!moved && !attacked) turnManager.actedUnit++;
            attacked = true;
        }
        pieceUI.attackSelected = false;
        PieceSelect(false);
    }
}
