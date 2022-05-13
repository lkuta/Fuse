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
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2((int)mousePos.x, (int)mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if(hit.collider != null)
            {
                textMesh = GameObject.Find("redDice/DiceValue").GetComponent<TextMeshPro>();
                value = new System.Random().Next(1, 7);
                textMesh.text = value.ToString();
            }
        }
    }

}
