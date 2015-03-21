using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Library : MonoBehaviour {

	private Unit unitToFind;

	//public Card[] cardLibrary = new Card[librarySize];

	public List<Card> cardLibrary = new List<Card> ();
	public List<Unit> unitIndex = new List<Unit> ();
	public List<Squad> squadIndex = new List<Squad> ();

	//List<BadGuy> badguys = new List<BadGuy>();

	void Start()
	{
		Debug.Log (cardLibrary.Count);

		Resources.LoadAll ("", typeof(Card));
		Resources.LoadAll ("", typeof(Unit));
		Resources.LoadAll ("", typeof(Squad));

		//Finds how many cards there are
		//librarySize = Resources.FindObjectsOfTypeAll (typeof(Card)).Length;
		Debug.Log (Resources.FindObjectsOfTypeAll<Card> ().ToList ().Count);
		//Sets the cardlibrary[]
		cardLibrary = Resources.FindObjectsOfTypeAll<Card>().ToList();
		unitIndex = Resources.FindObjectsOfTypeAll<Unit>().ToList();
		squadIndex = Resources.FindObjectsOfTypeAll<Squad>().ToList();

		//Debug.Log(cardLibrary[0].cardName);
//		//Finds how many cards there are
//		librarySize = (int) FindObjectsOfType (typeof(Card)).Length;
//		//Sets the cardlibrary[]
//		cardLibrary = FindObjectsOfType (typeof(Card)) as Card[];

		Debug.Log (cardLibrary.Count);
	}

	public Unit findUnit(string unitName)
	{
		for(int i=0; i<unitIndex.Count; i++)
		{
			if(unitIndex[i].name == unitName)
			{
				unitToFind = unitIndex[i];
			}
		}
		return unitToFind;
	}
	
}
