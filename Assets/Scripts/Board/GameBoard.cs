using UnityEngine.EventSystems;
using UnityEngine;

public class GameBoard : MonoBehaviour, IPointerClickHandler
{
    public int index;
    private int speed  = 10;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Board Clicked");
        GameObject.Find("Main Camera").transform.position = Vector3.MoveTowards(GameObject.Find("Main Camera").transform.position, transform.position, speed);
        return;
    }

}
