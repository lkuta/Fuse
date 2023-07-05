using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using System.IO;

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

    // Struct to represnet a single card in the game:
    public struct Card
    {
        public int Index;
        public int Value;
        public Sprite Sprite;

        public Card(int index, int value, Sprite sprite)
        {
            Index = index;
            Value = value;
            Sprite = sprite;
        }
    }

    // Create the empty Dice Bag:
    List<Dice> diceBag = new List<Dice>();

    // Create the empty deck of Bomb Cards:
    List<Card> bombDeck = new List<Card>();

    // Set the default colors for each of the dice:
    Color red = new Color32(255, 0, 0, 255);
    Color blue = new Color32(8, 69, 189, 255);
    Color green = new Color32(0, 102, 0, 255);
    Color yellow = new Color32(255, 247, 114, 255);
    Color black = new Color32(0, 0, 0, 255);
    Color grey = new Color32(224, 224, 224, 255);

    // Used for the Blue, Red, and Blue Dice text:
    Color white = new Color32(255, 255, 255, 255);

    private TextMeshPro textMesh;
    private GameObject dice1;
    private GameObject dice2;
    private GameObject dice3;

    public Dice userDice1;
    public Dice userDice2;
    public Dice userDice3;

    // Create all of the cards used in the game:
    void CreateCards()
    {
        int cardID = 1;
        
        // Pull all of the Sprites from the "Cards" folder:
        object[] allCardSprites = Resources.LoadAll("Cards", typeof(Sprite));



        // Temp card definition:
        Card loadedCard = new Card();

        // Create a Card struct for each image and put the struct in the deck:
        foreach (Sprite cardSprite in allCardSprites)
        {
            Debug.Log(cardSprite);
            loadedCard.Sprite = cardSprite;
            loadedCard.Index = cardID++;
            bombDeck.Add(loadedCard);
        }

        Debug.Log(bombDeck.Count);

        // Shake up (Shuffle) the deck:
        var rnd = new System.Random();
        bombDeck= bombDeck.OrderBy(item => rnd.Next()).ToList();
    }


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

        //Disable the cards on the board:
        Image playerCard1 = GameObject.Find("Canvas/Card1").GetComponent<Image>();
        playerCard1.enabled = false;

        Image playerCard2 = GameObject.Find("Canvas/Card2").GetComponent<Image>();
        playerCard2.enabled = false;

        Image playerCard3 = GameObject.Find("Canvas/Card3").GetComponent<Image>();
        playerCard3.enabled = false;

        Image playerCard4 = GameObject.Find("Canvas/Card4").GetComponent<Image>();
        playerCard4.enabled = false;

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

        CreateCards();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void diceDiceButtonClicked ()
    {

        // Shake up (Shuffle) the bag:
        var rnd = new System.Random();
        diceBag = diceBag.OrderBy(item => rnd.Next()).ToList();

        if (diceBag.Count >= 3)
        {
            //Debug.Log("Dice.Count: " + diceBag.Count);
            // Find Dice 3 in the list:
            userDice3 = diceBag[2];

            // Display Dice 3 to the player:
            textMesh = GameObject.Find("Dice3/DiceValue").GetComponent<TextMeshPro>();
            textMesh.color = userDice3.TextColor;
            textMesh.text = userDice3.Value.ToString();

            dice3 = GameObject.Find("Dice3");
            dice3.GetComponent<SpriteRenderer>().color = userDice3.DiceColor;

            dice3.GetComponent<SpriteRenderer>().enabled = true;
            textMesh.renderer.enabled = true;

            // Update text to list the current dice count:
            Text diceBagCount = GameObject.Find("Canvas/diceBagCount").GetComponent<Text>();
            diceBagCount.text = "Dice Remaining in Bag: " + (diceBag.Count - 1).ToString();

            if(diceBag.Count == 3)
            {
                // Change the button text to be "Bag Empty - You cannot draw more dice":
                TextMeshProUGUI buttonText = GameObject.Find("Canvas/drawDice/innerText").GetComponent<TextMeshProUGUI>();
                buttonText.text = "Bag Empty!";
                buttonText.color = black;
                Button button = GameObject.Find("Canvas/drawDice").GetComponent<Button>();
                button.interactable = false;
                button.GetComponent<Image>().color = grey;

                // Update text to list the current dice count:
                diceBagCount = GameObject.Find("Canvas/diceBagCount").GetComponent<Text>();
                diceBagCount.text = "Dice Remaining in Bag: 0";


                return;
            }
            // Remove the dice from the list:
            diceBag.RemoveAt(2);


        } if (diceBag.Count >= 2)
        {
            //Debug.Log("Dice.Count: " + diceBag.Count);
            // Find Dice 2 in the List:
            userDice2 = diceBag[1];

            // Display Dice 2 to the player:
            textMesh = GameObject.Find("Dice2/DiceValue").GetComponent<TextMeshPro>();
            textMesh.color = userDice2.TextColor;
            textMesh.text = userDice2.Value.ToString();

            dice2 = GameObject.Find("Dice2");
            dice2.GetComponent<SpriteRenderer>().color = userDice2.DiceColor;

            dice2.GetComponent<SpriteRenderer>().enabled = true;
            textMesh.renderer.enabled = true;

            // Update text to list the current dice count:
            Text diceBagCount = GameObject.Find("Canvas/diceBagCount").GetComponent<Text>();
            diceBagCount.text = "Dice Remaining in Bag: " + (diceBag.Count - 1).ToString();

            if (diceBag.Count == 2)
            {
                textMesh = GameObject.Find("Dice3/DiceValue").GetComponent<TextMeshPro>();
                textMesh.renderer.enabled = false;

                dice3 = GameObject.Find("Dice3");
                dice3.GetComponent<SpriteRenderer>().enabled = false;

                // Change the button text to be "Bag Empty - You cannot draw more dice":
                TextMeshProUGUI buttonText = GameObject.Find("Canvas/drawDice/innerText").GetComponent<TextMeshProUGUI>();
                buttonText.text = "Bag Empty!";
                buttonText.color = black;
                Button button = GameObject.Find("Canvas/drawDice").GetComponent<Button>();
                button.interactable = false;
                button.GetComponent<Image>().color = grey;

                // Update text to list the current dice count:
                diceBagCount = GameObject.Find("Canvas/diceBagCount").GetComponent<Text>();
                diceBagCount.text = "Dice Remaining in Bag: 0" ;

                return;
            }

            // Remove dice 2 from the bag:
            diceBag.RemoveAt(1);

        } if (diceBag.Count >= 1)
        {
            //Debug.Log("Dice.Count: " + diceBag.Count);
            // Find Dice 1 in the List:
            userDice1 = diceBag[0];

            // Display Dice 1 to the player:
            textMesh = GameObject.Find("Dice1/DiceValue").GetComponent<TextMeshPro>();
            textMesh.color = userDice1.TextColor;
            textMesh.text = userDice1.Value.ToString();

            dice1 = GameObject.Find("Dice1");
            dice1.GetComponent<SpriteRenderer>().color = userDice1.DiceColor;

            dice1.GetComponent<SpriteRenderer>().enabled = true;
            textMesh.renderer.enabled = true;

            // Update text to list the current dice count:
            Text diceBagCount = GameObject.Find("Canvas/diceBagCount").GetComponent<Text>();
            diceBagCount.text = "Dice Remaining in Bag: " + (diceBag.Count - 1).ToString();

            if (diceBag.Count == 1)
            {
                textMesh = GameObject.Find("Dice2/DiceValue").GetComponent<TextMeshPro>();
                textMesh.renderer.enabled = false;

                dice2 = GameObject.Find("Dice2");
                dice2.GetComponent<SpriteRenderer>().enabled = false;

                textMesh = GameObject.Find("Dice3/DiceValue").GetComponent<TextMeshPro>();
                textMesh.renderer.enabled = false;

                dice3 = GameObject.Find("Dice3");
                dice3.GetComponent<SpriteRenderer>().enabled = false;

                // Change the button text to be "Bag Empty - You cannot draw more dice":
                TextMeshProUGUI buttonText = GameObject.Find("Canvas/drawDice/innerText").GetComponent<TextMeshProUGUI>();
                buttonText.text = "Bag Empty!";
                buttonText.color = black;
                Button button = GameObject.Find("Canvas/drawDice").GetComponent<Button>();
                button.interactable = false;
                button.GetComponent<Image>().color = grey;

                // Update text to list the current dice count:
                diceBagCount = GameObject.Find("Canvas/diceBagCount").GetComponent<Text>();
                diceBagCount.text = "Dice Remaining in Bag: 0";

                return;
            }

            // Remove Dice 1 in the list:
            diceBag.RemoveAt(0);

        } if (diceBag.Count == 0)
        {
            // Update text to list the current dice count:
            Text diceBagCount = GameObject.Find("Canvas/diceBagCount").GetComponent<Text>();
            diceBagCount.text = "Dice Remaining in Bag: " + diceBag.Count.ToString();

            Debug.Log("Dice.Count: " + diceBag.Count);
            // Change the button text to be "Bag Empty - You cannot draw more dice":
            TextMeshProUGUI button = GameObject.Find("Canvas/drawDice/innerText").GetComponent<TextMeshProUGUI>();
            button.text = "Bag Empty!";
        }

    }

    public void DealCardsClicked ()
    {
        // Draw 4 cards and update the each sprite with the new cards:
        // Draw Card 1:
        Card newPlayerCard = bombDeck[0];
        Image playerCard1 = GameObject.Find("Canvas/Card1").GetComponent<Image>();
        playerCard1.sprite = newPlayerCard.Sprite;
        playerCard1.enabled = true;
        bombDeck.RemoveAt(0);

        // Draw Card 2:
        newPlayerCard = bombDeck[0];
        Image playerCard2 = GameObject.Find("Canvas/Card2").GetComponent<Image>();
        playerCard2.sprite = newPlayerCard.Sprite;
        playerCard2.enabled = true;
        bombDeck.RemoveAt(0);

        // Draw Card 3:
        newPlayerCard = bombDeck[0];
        Image playerCard3 = GameObject.Find("Canvas/Card3").GetComponent<Image>();
        playerCard3.sprite = newPlayerCard.Sprite;
        playerCard3.enabled = true;
        bombDeck.RemoveAt(0);

        // Draw Card 4:
        newPlayerCard = bombDeck[0];
        Image playerCard4 = GameObject.Find("Canvas/Card4").GetComponent<Image>();
        playerCard4.sprite = newPlayerCard.Sprite;
        playerCard4.enabled = true;
        bombDeck.RemoveAt(0);
    }
}
