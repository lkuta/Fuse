using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;


public class FuseController : MonoBehaviour
{
    // Struct to represent each dice in the game:
    public struct Dice
    {
        public int Value;
        public Color DiceColor;
        public Color TextColor;

        public Dice(int value, Color diceColor, Color textColor)
        {
            Value = value;
            DiceColor = diceColor;
            TextColor = textColor;
        }
    }

    // Create the empty Dice Bag:
    List<Dice> diceBag = new List<Dice>();

    // Set the default colors for each of the dice:
    Color red = new Color32(255, 0, 0, 255);
    Color blue = new Color32(8, 69, 189, 255);
    Color green = new Color32(0, 102, 0, 255);
    Color yellow = new Color32(255, 247, 114, 255);
    Color black = new Color32(0, 0, 0, 255);

    // Used for the Blue, Red, and Blue Dice text:
    Color white = new Color32(255, 255, 255, 255);

    private TextMeshPro textMesh;
    private GameObject dice1;
    private GameObject dice2;
    private GameObject dice3;

    public Dice userDice1;
    public Dice userDice2;
    public Dice userDice3;

    // Start is called before the first frame update
    void Start()
    {
        //Disable each of the dice on the board:
        textMesh = GameObject.Find("Dice1/DiceValue").GetComponent<TextMeshPro>();
        textMesh.renderer.enabled = false;

        dice1 = GameObject.Find("Dice1");
        dice1.GetComponent<SpriteRenderer>().enabled = false;

        textMesh = GameObject.Find("Dice2/DiceValue").GetComponent<TextMeshPro>();
        textMesh.renderer.enabled = false;

        dice2 = GameObject.Find("Dice2");
        dice2.GetComponent<SpriteRenderer>().enabled = false;

        textMesh = GameObject.Find("Dice3/DiceValue").GetComponent<TextMeshPro>();
        textMesh.renderer.enabled = false;

        dice3 = GameObject.Find("Dice3");
        dice3.GetComponent<SpriteRenderer>().enabled = false;

        //int rand = new System.Random().Next(1, 7);
        System.Random rand2 = new System.Random();

        // Fill the dice Bag with 5 dice of each color:
        for (int i = 0; i < 5; i++)
        {
            Dice redDice = new Dice();
            redDice.Value = rand2.Next(1, 7);
            redDice.DiceColor = red;
            redDice.TextColor = white;

            Dice yellowDice = new Dice();
            yellowDice.Value = rand2.Next(1, 7);
            yellowDice.DiceColor = yellow;
            yellowDice.TextColor = black;

            Dice greenDice = new Dice();
            greenDice.Value = rand2.Next(1, 7);
            greenDice.DiceColor = green;
            greenDice.TextColor = black;

            Dice blueDice = new Dice();
            blueDice.Value = rand2.Next(1, 7);
            blueDice.DiceColor = blue;
            blueDice.TextColor = white;

            Dice blackDice = new Dice();
            blackDice.Value = rand2.Next(1, 7);
            blackDice.DiceColor = black;
            blackDice.TextColor = white;

            diceBag.Add(redDice);
            diceBag.Add(yellowDice);    
            diceBag.Add(blackDice);
            diceBag.Add(greenDice);
            diceBag.Add(blueDice);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void diceDiceButtonClicked ()
    {

        var rnd = new System.Random();
        diceBag = diceBag.OrderBy(item => rnd.Next()).ToList();

        // Pop 3 of the dice off of the list:
        userDice1 = diceBag[0];
        userDice2 = diceBag[1];
        userDice3 = diceBag[2];

        // Display those three dice to the player:
        textMesh = GameObject.Find("Dice1/DiceValue").GetComponent<TextMeshPro>();
        textMesh.color = userDice1.TextColor;
        textMesh.text = userDice1.Value.ToString();

        dice1 = GameObject.Find("Dice1");
        dice1.GetComponent<SpriteRenderer>().color = userDice1.DiceColor;

        dice1.GetComponent<SpriteRenderer>().enabled = true;
        textMesh.renderer.enabled = true;

        // Display those three dice to the player:
        textMesh = GameObject.Find("Dice2/DiceValue").GetComponent<TextMeshPro>();
        textMesh.color = userDice2.TextColor;
        textMesh.text = userDice2.Value.ToString();

        dice2 = GameObject.Find("Dice2");
        dice2.GetComponent<SpriteRenderer>().color = userDice2.DiceColor;

        dice2.GetComponent<SpriteRenderer>().enabled = true;
        textMesh.renderer.enabled = true;

        // Display those three dice to the player:
        textMesh = GameObject.Find("Dice3/DiceValue").GetComponent<TextMeshPro>();
        textMesh.color = userDice3.TextColor;
        textMesh.text = userDice3.Value.ToString();

        dice3 = GameObject.Find("Dice3");
        dice3.GetComponent<SpriteRenderer>().color = userDice3.DiceColor;

        dice3.GetComponent<SpriteRenderer>().enabled = true;
        textMesh.renderer.enabled = true;

        // Remove the dice from the list:
        diceBag.RemoveAt(0);
        diceBag.RemoveAt(1);
        diceBag.RemoveAt(2);



    }
}
