using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DiceRoll : MonoBehaviour
{
    private TextMeshPro textMesh;
    private int value;
    private float startPosX;
    private float startPosY;
    private bool isBeingHeld = false;

    // Start is called before the first frame update
   /* void Start()
    {
        textMesh = GameObject.Find("redDice/DiceValue").GetComponent<TextMeshPro>();
        value = 2;
    } */

    // Update is called once per frame
    void Update()
    {
        textMesh.text = value.ToString();
        value++;
    }

    private void OnMouseDown()
    {
        Vector3 mousePos;
        mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mouse)
    }

    private void OnMouseUp()
    {

    }
}
