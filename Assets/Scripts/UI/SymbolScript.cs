using UnityEngine;
using UnityEngine.EventSystems;

public class SymbolScript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
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

    }

    // 다른 카드 지우기
    public static void DeleteCards()
    {
        GameObject cards = GameObject.Find("Cards");
        foreach (Transform c in cards.transform) c.gameObject.SetActive(false);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Start Drag");
    }

    public void OnDrag(PointerEventData eventData)
    {
        eventData.position = this.transform.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End Drag");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        DeleteCards();
        card.SetActive(true);
    }
}
