using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Squad : MonoBehaviour {

	public float squadCurrentHealth;
	public float squadMaxHealth;

	public bool Selected;

	public List<Unit> children = new List<Unit>();

	// Use this for initialization
	void Start () 
	{
		UnitController.CallUnitSpawn (this.transform.gameObject);
	}

	void Awake()
	{
		children = gameObject.GetComponentsInChildren<Unit>().ToList();
		Selected = false;
		squadCurrentHealth = squadMaxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (squadMaxHealth);

	}

	public void Select () //Need to call this somewhere
	{
		if (Selected == true) 
		{

			foreach(Unit child in children)
			{
				
				Debug.Log (child.transform.FindChild ("Selected"));
				GameObject SelectedObj = child.transform.FindChild ("Selected").gameObject;
				SelectedObj.SetActive (true);
				//child.FindChild("Selected").gameObject.SetActive(true);
			}
			//this.transform
		} else if (Selected == false) {
			foreach(Unit child in children)
			{
				
				Debug.Log (child.transform.FindChild ("Selected"));
				GameObject SelectedObj = child.transform.FindChild ("Selected").gameObject;
				SelectedObj.SetActive (false);
				//child.FindChild("Selected").gameObject.SetActive(true);
			}
		}
	}
}
