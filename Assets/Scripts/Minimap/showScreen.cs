using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showScreen : MonoBehaviour
{
    private Vector3 v;

    public void Start()
    {
        v = transform.localPosition;
    }

    public void SetLocation(float value)
    {
        transform.localPosition = v + new Vector3(326*value, 0, 0);
    }

}
