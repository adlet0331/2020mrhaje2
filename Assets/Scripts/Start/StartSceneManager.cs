using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartSceneManager : MonoBehaviour
{
    public ToggleGroup CardList;
    public Transform Player1_Group;
    public Transform Player2_Group;
    public GameObject Player1_Check;
    public GameObject Player2_Check;
    public StartTextmesh CardTextmeshPrefab;

    private List<SymbolScript.NameToNum> CardList_Player1;
    private List<SymbolScript.NameToNum> CardList_Player2;
    private GameObject card;
    private SymbolInfo symbolInfo;
    [SerializeField] private bool Is_Player1;
    [SerializeField] private bool Is_FixedCard;
    [SerializeField] private SymbolScript.NameToNum currentCardName;

    private event Action<SymbolScript.NameToNum> Clicked;
    public event Action<bool> Checked;
    void TextmeshClicked(SymbolScript.NameToNum cardName) //Textmesh Clicked 이벤트 구독
    {
        currentCardName = cardName;
        SetCard(cardName);
    }
    void PlayerChecked(bool isPlayer1) //Toggle Checked 이벤트 구독
    {
        Is_Player1 = isPlayer1; //Player 1 인가?
        if (isPlayer1)
        {
            Is_Player1 = true;
            Player1_Check.GetComponent<Text>().color = Color.red;
            Player2_Check.GetComponent<Text>().color = Color.black;
        }
        else
        {
            Is_Player1 = false;
            Player1_Check.GetComponent<Text>().color = Color.black;
            Player2_Check.GetComponent<Text>().color = Color.red;
        }
    }
    private void Awake()
    {
        card = GameObject.Find("Canvas").transform.Find("BaseCard").gameObject;
        symbolInfo = GameObject.Find("Symbol Information").GetComponent<SymbolInfo>();
        foreach (StartTextmesh cardmesh in CardList.GetComponentsInChildren<StartTextmesh>())
        {
            cardmesh.Clicked += TextmeshClicked;
        }
        Player1_Check.GetComponentInChildren<StartPlayerToggle>().Checked += PlayerChecked;
        Player2_Check.GetComponentInChildren<StartPlayerToggle>().Checked += PlayerChecked;
        CardList_Player1 = new List<SymbolScript.NameToNum>();
        CardList_Player2 = new List<SymbolScript.NameToNum>();
    }
    private void Start()
    {
        TextmeshClicked(SymbolScript.NameToNum.Infatry);
        PlayerChecked(true);
    }
    public void AddButton()
    {
        StartTextmesh textmesh;
        if (Is_Player1)
        {
            if (CardList_Player1.Count == 5) return;
            CardList_Player1.Add(currentCardName);
            textmesh = Instantiate(CardTextmeshPrefab, Player1_Group);
        }
        else
        {
            if (CardList_Player2.Count == 5) return;
            CardList_Player2.Add(currentCardName);
            textmesh = Instantiate(CardTextmeshPrefab, Player2_Group);
        }
        textmesh.CardName = currentCardName;
        textmesh.GetComponent<TextMeshProUGUI>().text = symbolInfo.cardInfos[(int)currentCardName].cardName;
        textmesh.Clicked += SetCard;
    }
    public void DeleteButton()
    {
        if (Is_Player1)
        {
            foreach(StartTextmesh textmesh in Player1_Group.GetComponentsInChildren<StartTextmesh>())
            {
                if(textmesh.GetComponent<TextMeshProUGUI>().text == symbolInfo.cardInfos[(int)currentCardName].cardName)
                {
                    Destroy(textmesh.gameObject);
                    CardList_Player1.Remove(currentCardName);
                    return;
                }
            }
        }
        else
        {
            foreach (StartTextmesh textmesh in Player2_Group.GetComponentsInChildren<StartTextmesh>())
            {
                if (textmesh.GetComponent<TextMeshProUGUI>().text == symbolInfo.cardInfos[(int)currentCardName].cardName)
                {
                    Destroy(textmesh.gameObject);
                    CardList_Player2.Remove(currentCardName);
                    return;
                }
            }
        }
    }
    void SetCard(SymbolScript.NameToNum cardName)
    {
        card.transform.Find("Card Image").GetComponent<Image>().sprite = symbolInfo.cardInfos[(int)cardName].cardImage;
        card.transform.Find("Name").GetComponent<Text>().text = symbolInfo.cardInfos[(int)cardName].cardName;
        card.transform.Find("MaxHP").GetComponent<Text>().text = symbolInfo.cardInfos[(int)cardName].maxHP.ToString();
        card.transform.Find("Ability Name").GetComponent<Text>().text = symbolInfo.cardInfos[(int)cardName].abilityName;
        card.transform.Find("Ability Information").GetComponent<Text>().text = symbolInfo.cardInfos[(int)cardName].abilityInformation;
        card.transform.Find("Move").GetComponent<Text>().text = symbolInfo.cardInfos[(int)cardName].move.ToString();
        card.transform.Find("Range").GetComponent<Text>().text = symbolInfo.cardInfos[(int)cardName].range.ToString();
        card.transform.Find("Damage").GetComponent<Text>().text = symbolInfo.cardInfos[(int)cardName].damage.ToString();
    }
}
