using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dice : MonoBehaviour
{
    public Sprite[] diceSprite = new Sprite[7];
    
    private UIControl pieceUI;
    private TurnManager turnManager;
    private int n;

    // Start is called before the first frame update
    void Start()
    {
        pieceUI = GameObject.Find("PieceUI").GetComponent<UIControl>();
        turnManager = GameObject.Find("Turn Manager").GetComponent<TurnManager>();
        RollDice(); // 처음에도 주사위 굴리기

        
    }

    public void RollDice()
    {
        n = Random.Range(1, 7);
        transform.Find("Image").GetComponent<Image>().sprite = diceSprite[n];

        DeltaValue deltaValue = transform.Find("DeltaValue" + turnManager.turn).GetComponent<DeltaValue>();
        int[] deltaMove = deltaValue.deltaMove;
        int[] deltaRange = deltaValue.deltaRange;
        int[] deltaDamage = deltaValue.deltaDamage;

        // 능력치 보정
        foreach (SymbolScript symbolScript in pieceUI.allSymbols)
        {
            symbolScript.deltaMove = 0;
            symbolScript.deltaRange = 0;
            symbolScript.deltaDamage = 0;

            if (symbolScript.team == turnManager.turn)
            {
                symbolScript.deltaMove = deltaMove[n];
                symbolScript.deltaRange = deltaRange[n];
                symbolScript.deltaDamage = deltaDamage[n];
            }
        }

        // 우상단 문구
        transform.Find("Text").GetComponent<Text>().text
            = "이동 거리 " + (deltaMove[n] >= 0 ? "+" : "") + deltaMove[n]
            + " / 공격 거리 " + (deltaRange[n] >= 0 ? "+" : "") + deltaRange[n]
            + " / 공격력 " + (deltaDamage[n] >= 0 ? "+" : "") + deltaDamage[n] + "%";

        // DeltaValue 값 표시
        Transform grid = transform.Find("DeltaValueInfo").Find("Text Grid");
        grid.Find("Text (4)").GetComponent<Text>().text = (deltaMove[1] >= 0 ? "+" : "") + deltaMove[1];
        grid.Find("Text (7)").GetComponent<Text>().text = (deltaMove[2] >= 0 ? "+" : "") + deltaMove[2];
        grid.Find("Text (10)").GetComponent<Text>().text = (deltaMove[3] >= 0 ? "+" : "") + deltaMove[3];
        grid.Find("Text (13)").GetComponent<Text>().text = (deltaMove[4] >= 0 ? "+" : "") + deltaMove[4];
        grid.Find("Text (16)").GetComponent<Text>().text = (deltaMove[5] >= 0 ? "+" : "") + deltaMove[5];
        grid.Find("Text (19)").GetComponent<Text>().text = (deltaMove[6] >= 0 ? "+" : "") + deltaMove[6];

        grid.Find("Text (5)").GetComponent<Text>().text = (deltaRange[1] >= 0 ? "+" : "") + deltaRange[1];
        grid.Find("Text (8)").GetComponent<Text>().text = (deltaRange[2] >= 0 ? "+" : "") + deltaRange[2];
        grid.Find("Text (11)").GetComponent<Text>().text = (deltaRange[3] >= 0 ? "+" : "") + deltaRange[3];
        grid.Find("Text (14)").GetComponent<Text>().text = (deltaRange[4] >= 0 ? "+" : "") + deltaRange[4];
        grid.Find("Text (17)").GetComponent<Text>().text = (deltaRange[5] >= 0 ? "+" : "") + deltaRange[5];
        grid.Find("Text (20)").GetComponent<Text>().text = (deltaRange[6] >= 0 ? "+" : "") + deltaRange[6];

        grid.Find("Text (6)").GetComponent<Text>().text = (deltaDamage[1] >= 0 ? "+" : "") + deltaDamage[1] + "%";
        grid.Find("Text (9)").GetComponent<Text>().text = (deltaDamage[2] >= 0 ? "+" : "") + deltaDamage[2] + "%";
        grid.Find("Text (12)").GetComponent<Text>().text = (deltaDamage[3] >= 0 ? "+" : "") + deltaDamage[3] + "%";
        grid.Find("Text (15)").GetComponent<Text>().text = (deltaDamage[4] >= 0 ? "+" : "") + deltaDamage[4] + "%";
        grid.Find("Text (18)").GetComponent<Text>().text = (deltaDamage[5] >= 0 ? "+" : "") + deltaDamage[5] + "%";
        grid.Find("Text (21)").GetComponent<Text>().text = (deltaDamage[6] >= 0 ? "+" : "") + deltaDamage[6] + "%";
    }

    public void ShowDeltaValue(bool state)
    {
        transform.Find("DeltaValueInfo").gameObject.SetActive(state);
    }
}
