using UnityEngine;
using System.Collections;
using System;

public class Card : MonoBehaviour {

	public enum Affinty{Fire, Frost, Nature, Shadow};


	private Vector3 oldScale;

	#region Card Stats
	public string cardName;
	public int blackrockCost;
	public Affinty cardAffinty;
	public bool isInHand;
	#endregion


	public Card(string cardName, int blackrockCost, Affinty cardAffinty, bool isInHand)
	{
		this.cardName = cardName;
		this.blackrockCost = blackrockCost;
		this.cardAffinty = cardAffinty;
		this.isInHand = isInHand;

	}
		
	public void OnMouseEnter ()
	{
		oldScale = this.transform.localScale;
			float growthAmount = 6;
			float aspectRatio = 0.714286f;
			this.transform.localScale = new Vector3(growthAmount * aspectRatio ,growthAmount, this.transform.localScale.z); // apply the growthAmount to the localScale
			if(Settings.tooltipsEnabled)
			{
				Debug.Log ("Tooltips enabled!");
			}
		Debug.Log("Mouse is over card!");
	}

	void OnMouseExit()
	{
		this.transform.localScale = oldScale;
		Debug.Log("Mouse has left card!");
	}

	public string CardToUnitConversion(Card card)
	{
		string unitName = card.name;
		return unitName;
	}
}
