using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HealthManager : MonoBehaviour {

	private int unitLayer = 8;

	public SpriteManager HealthbarSpriteManager;

	//Health Tracking 
	float maxHealth;
	float currentHealth;
	float newHealth;
	
	void Start () 
	{
		UnitController.onUnitSpawn += AddHealthbarToUnitSpawned;

		UnitController.onUnitDead += RemoveHealthbarToUnitDead;

		//AddHealthBarToUnits ();
	}

	public void AddHealthbarToUnitSpawned(GameObject unit)
	{

		Debug.Log (unit);

		//Debug.Log (unit.transform.FindChild ("Healthbar"));

		GameObject healthBarClient = unit.transform.FindChild ("Healthbar").gameObject;

		//Debug.Log ("healthBarClient: ");

		Sprite healthbarSprite = HealthbarSpriteManager.AddSprite (healthBarClient, 6f, 1f, new Vector2 (0f, 0.9f), new Vector2 (0.1f, 0.1f), Vector3.zero, false);

		//Debug.Log ("healthbar Sprite: ");

		healthBarClient.GetComponent<HealthbarClient> ().myHealthBarSprite = healthbarSprite;
	}
	

	public void RemoveHealthbarToUnitDead(GameObject unit)
	{
		GameObject healthBarClient = unit.transform.FindChild ("Healthbar").gameObject;

		Sprite healthbarSprite = healthBarClient.GetComponent<HealthbarClient> ().myHealthBarSprite as Sprite;

		HealthbarSpriteManager.RemoveSprite (healthbarSprite);
	}

	public void updateHealthbarToUnit(GameObject Unit, float HealthDiff)
	{
		if (Unit.GetComponent<Unit> ()) {
						maxHealth = Unit.GetComponent<Unit> ().maxHealth;
						currentHealth = Unit.GetComponent<Unit> ().currentHealth;
						newHealth = currentHealth + HealthDiff;

						//apply new health
						Unit.GetComponent<Unit> ().currentHealth = newHealth;
		
				} else if (Unit.GetComponent<Squad> ()) {
					maxHealth = Unit.GetComponent<Squad> ().squadMaxHealth;
					currentHealth = Unit.GetComponent<Squad> ().squadCurrentHealth;
					newHealth = currentHealth + HealthDiff;
					
					//apply new health
					Unit.GetComponent<Squad> ().squadCurrentHealth = newHealth;
				}

		if(newHealth > 0)
		{
			//get healthbar client
			GameObject healthBarClient = Unit.transform.FindChild ("Healthbar").gameObject;

			float NoOfSpritesAcross = 10f;
			float NoOfSpritesDown = 10f;

			float OnePercent = maxHealth / 100;
			float HealthPercentage = newHealth / OnePercent;

			float UV_X = Mathf.Ceil(HealthPercentage % NoOfSpritesAcross);
			float UV_Y = Mathf.Floor(HealthPercentage / NoOfSpritesDown);

			UV_X /= 10f;
			UV_Y /= 10f;

			UV_X = 1f - UV_X;

			//remove current sprite
			Sprite currentHealthbarSprite = healthBarClient.GetComponent<HealthbarClient>().myHealthBarSprite as Sprite;
			HealthbarSpriteManager.RemoveSprite(currentHealthbarSprite);
			//apply new sprite with updated uvs
			Sprite healthbarSprite = HealthbarSpriteManager.AddSprite(healthBarClient, 6f, 1f, new Vector2 (UV_X, UV_Y), new Vector2 (0.1f, 0.1f), Vector3.zero, false);
			healthBarClient.GetComponent<HealthbarClient>().myHealthBarSprite = healthbarSprite;
		}
	}

	public void AddHealthBarToUnits()
	{
		//GameObject[] GameObjectArray = FindObjectOfType (typeof(GameObject)) as GameObject[];
		GameObject[] GameObjectArray = FindObjectsOfType(typeof(GameObject)) as GameObject[];
		List<GameObject> unitList = new List<GameObject>();

		for(int i = 0; i < GameObjectArray.Length; i++)
		{
			if(GameObjectArray[i].layer == unitLayer)
			{
				if(!GameObjectArray[i].GetComponent<Unit>().isInSquad)
					unitList.Add(GameObjectArray[i]);
			}
		}

		if(unitList.Count == 0)
			return;

		GameObject[] Units = unitList.ToArray ();

		for(int i = 0; i < Units.Length; i++)
		{
			AddHealthbarToUnitSpawned(Units[i].gameObject);
		}

		return;
	}

	void OnGUI()
	{
		if(Input.GetKeyUp(KeyCode.Space))
		{
			updateHealthbarToUnit(GameObject.Find ("Large Cube"), -10f);
		}
	}
}
