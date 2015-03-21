using UnityEngine;
using System.Collections;
/*
 * This Script attacched to all controlable units wether walkable or not
 */
public class Unit : MonoBehaviour {

	//for Mouse.cs
	public Vector2 ScreenPos;
	public bool OnScreen;
	public bool Selected = false;

	public int powerCost;

	public bool isBuilding = false;
	public bool isBuilt;

	private GameObject parent;

	public bool isWalkable = true;
	public bool isInSquad;
	public int positionInSquad;
	private GameObject parentObject;

	public float maxHealth = 1000f;
	public float currentHealth = 1000f;

	void Start()
	{
		if (!isInSquad)
		{
			UnitController.CallUnitSpawn (this.transform.gameObject);
		}
		if(isBuilding)
		{
			isBuilt = false;
			isWalkable = false;
		}
		OnScreen = false;

	}

	void Awake()
	{
		Physics.IgnoreLayerCollision (8, 8, true);
		if (this.isInSquad) 
		{
			//Debug.Log (isInSquad);
			//Debug.Log (this.gameObject.GetComponentInParent<Squad>().squadMaxHealth + this.maxHealth);
			//Debug.Log(collider.transform.root);
			this.gameObject.GetComponentInParent<Squad>().squadMaxHealth = this.gameObject.GetComponentInParent<Squad>().squadMaxHealth + this.maxHealth;
			//parent = collider.transform.root;
			//parent.gameObject.GetComponent<Squad>().squadHealth += this.currentHealth;
		}

	}

	void Update()
	{
		//if unit is not selected
		if (!Selected)
		{
			ScreenPos = Camera.main.WorldToScreenPoint(this.transform.position);

			if(MousePoint.UnitWithinScreenSpace(ScreenPos))
			{

				if(!OnScreen)
				{
					//Debug.Log ("Adding this to screen: " + this);
					MousePoint.UnitsOnScreen.Add(this.gameObject);
					OnScreen = true;
				}

			} else {
				//remove object if previously on screen
				if(OnScreen)
				{
					MousePoint.RemoveFromOnScreenUnits(this.gameObject);
				}
			}
		}

	}


}
