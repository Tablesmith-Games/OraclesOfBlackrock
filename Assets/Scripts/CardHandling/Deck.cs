using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Deck : MonoBehaviour {

	public GameObject lib;

	public static int numberOfCardsInDeck = 0;

	public List<Card> cardsInDeck = new List<Card>();

	void Start()
	{
//		//Debug.Log (numberOfCardsInDeck);

		cardsInDeck = lib.GetComponent<Library> ().cardLibrary;
//
//		numberOfCardsInDeck = lib.GetComponent<Library>().cardLibrary.Length;
//		cardsInDeck = lib.GetComponent<Library>().cardLibrary;

		//Random rnd = new Random();

		//int r = rnd(0, cardsInDeck.Count);
			
		//Debug.Log("Card Name: " + cardsInDeck[cardsInDeck.Count - 1].name);
		//Debug.Log ("no of cards in deck: " + cardsInDeck.Count);
		//Debug.Log (cardsInDeck);
	}

	void Update()
	{

	}

	void OnGUI()
	{

	}
}
