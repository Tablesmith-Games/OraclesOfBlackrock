using UnityEngine;
using System.Collections;

public class TurnManager : MonoBehaviour {

	public static bool isTurn;
	public bool haveDrawn;

	public GameObject hand;
	public GameObject deck;

	private Creature attacker;
	private Creature defender;

	int numberOfCardsToDraw = 4;

	// Use this for initialization
	void Start () 
	{
		int rand = Random.Range (1, 2);
		if(rand == 1)
		{
			isTurn = true;
		}
		else
		{
			isTurn = false;
		}

		Debug.Log (isTurn);

		for(int i = 0; i < numberOfCardsToDraw; i++)
		{
			Draw ();
		}

//		if(isTurn)
//		{
//			Draw ();
//		}
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void FixedUpdate()
	{
		if(isTurn)
		{
			if(!haveDrawn){
				Draw();
			}
		}
	}

	void OnGUI()
	{
		if(GUI.Button(new Rect(10, 70, 80, 30), "Toggle Turn"))
		{
			isTurn = !isTurn;
			haveDrawn = false;
			Debug.Log (isTurn);
		}
	}

	void Draw()
	{
		if(deck.GetComponent<Deck> ().cardsInDeck.Count > 0)
		{
			hand.GetComponent<Hand> ().cardsInHand.Add (deck.GetComponent<Deck> ().cardsInDeck [deck.GetComponent<Deck> ().cardsInDeck.Count - 1]);
			hand.GetComponent<Hand>().cardsInHand[hand.GetComponent<Hand>().cardsInHand.Count - 1].isInHand = true;
			if(!GameObject.Find(hand.GetComponent<Hand>().cardsInHand[hand.GetComponent<Hand>().cardsInHand.Count - 1].gameObject.name))
			{
				hand.GetComponent<Hand>().cardsInHand[hand.GetComponent<Hand>().cardsInHand.Count - 1] = 
				Instantiate (hand.GetComponent<Hand> ().cardsInHand[hand.GetComponent<Hand>().cardsInHand.Count - 1],
			    new Vector3(hand.transform.position.x + (hand.GetComponent<Hand>().cardsInHand.Count - 1 * -1 + hand.GetComponent<Hand>().cardsInHand.Count - 1 * -1), hand.transform.position.y, hand.transform.position.z),
			          		hand.transform.rotation) as Card;
			}
			deck.GetComponent<Deck>().cardsInDeck.RemoveAt(deck.GetComponent<Deck>().cardsInDeck.Count - 1);
			haveDrawn = true;
		} else {
			Debug.Log("No more cards in Deck");
			haveDrawn = true;
		}
	}
	public void commandAttack()
	{
		attacker = GameObject.Find ("Game Manager").GetComponent<MousePoint> ().attacker;
		defender = GameObject.Find ("Game Manager").GetComponent<MousePoint> ().defender;
		if(isTurn  && !attacker.isDead && !defender.isDead)
		{
			if(defender != attacker){
				Debug.Log ("Defenders endurance before attack: " + defender.endurance);
				attacker.Attack(defender);
				Debug.Log ("Defenders endurance after attack: " + defender.endurance);
				if(attacker.endurance <= 0)
				{
					attacker.isDead = true;
					attacker.transform.position = GameObject.Find("Graveyard").transform.position;
				}
				if(defender.endurance <= 0)
				{
					defender.isDead = true;
					defender.transform.position = GameObject.Find("Graveyard").transform.position;
				}
			}
		}
	}
}
