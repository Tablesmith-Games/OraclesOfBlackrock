using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Hand : MonoBehaviour {

	public static int cardPosInHand = 0;

	public GameObject field;

	//public int offset = 2;
 
	public List<Card> cardsInHand = new List<Card>();

	void Start()
	{
		//FormHand ();
	}

	// Update is called once per frame
	void Update ()
	{

	}

	void FixedUpdate()
	{
		if(Input.GetMouseButtonUp(0)){
			for (int i = 0; i < cardsInHand.Count; i++)
			{
				if(!cardsInHand[i].isInHand)
				{
					field.gameObject.GetComponent<Field>().cardsOnField.Add(cardsInHand[i]);
					Debug.Log ("Added card to field!");
					cardsInHand.Remove(cardsInHand[i]);
				}

			}
		}
	}

	void OnGUI()
	{

	}

	public void FormHand()
	{
		for (int i = 0; i < cardsInHand.Count; i++)
		{
			if(i <= 3)
			{
				cardPosInHand = -1;
			} else {
				cardPosInHand = 1;
			}
				Instantiate(cardsInHand[i], new Vector3(
											(this.transform.position.x + (i * cardPosInHand + 3.0f)),
											this.transform.position.y,
											this.transform.position.z),
			            					this.transform.rotation);
		}
	}
}
