using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymbolClick : MonoBehaviour
{
    public GameObject card;

    // Start is called before the first frame update
    void Start()
    {
        card.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // 클릭하면 카드 띄우기
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
            if (hit.collider != null)
            {
                if (hit.transform == transform)
                {
                    DeleteCards();
                    card.SetActive(true);
                }
            }
            else DeleteCards();
        }
    }

    // 다른 카드 지우기
    void DeleteCards()
    {
        GameObject cards = GameObject.Find("Cards");
        foreach (Transform c in cards.transform) c.gameObject.SetActive(false);
    }
}
