using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MinimapSymbolScript : MonoBehaviour
{
    private SymbolInfo symbolInfo;

    public enum NameToNum { Infatry, Sniper, MachineGunner }
    public NameToNum cardName;

    // 능력치
    public int currentHP;

    public void setCardName(int index, int team, int cardNum)
    {
        if (cardNum == 0) cardName = NameToNum.Infatry;
        else if (cardNum == 1) cardName = NameToNum.Sniper;
        else cardName = NameToNum.MachineGunner;

        if (team == 1)
        {
            this.transform.GetChild(0).GetComponent<Image>().sprite = SymbolInfo.Instance.cardInfos[(int)cardName].pieceSprite_L;
        }
        if (team == 2)
        {
            this.transform.GetChild(0).GetComponent<Image>().sprite = SymbolInfo.Instance.cardInfos[(int)cardName].pieceSprite_R;
        }
        // HP 초기화
        currentHP = symbolInfo.cardInfos[(int)cardName].maxHP;
    }
    // Start is called before the first frame update
    void Start()
    {
        symbolInfo = GameObject.Find("Symbol Information").GetComponent<SymbolInfo>();
    }

    // 필드 자신에게서 HP 정보를 받아 활용
    public void getHP(int curHP)
    {
        // HP 바 조정
        currentHP = curHP;
        Vector3 barScale = transform.Find("Current HP Bar").localScale;
        barScale.x = (float)currentHP / symbolInfo.cardInfos[(int)cardName].maxHP;
        transform.Find("Current HP Bar").localScale = barScale;

        // HP가 0 이하가 되면 파괴
        if (currentHP <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void Move(GameObject board)
    {
        transform.SetParent(board.transform);
    }
}
