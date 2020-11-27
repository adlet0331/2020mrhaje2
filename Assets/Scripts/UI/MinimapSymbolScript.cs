using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MinimapSymbolScript : MonoBehaviour
{
    private UIControl pieceUI;
    private GameObject card;
    private SymbolInfo symbolInfo;
    private TurnManager turnManager;

    public enum NameToNum { Infatry, Sniper, MachineGunner }
    public NameToNum cardName;
    public int team;

    // 능력치
    public int currentHP;
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
        turnManager = GameObject.Find("Turn Manager").GetComponent<TurnManager>();

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

        // HP가 0 이하가 되면 파괴
        if (currentHP <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void Move(MinimapGameBoard board)
    {
        
    }
}
