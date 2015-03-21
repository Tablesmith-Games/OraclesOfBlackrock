using UnityEngine;
using System.Collections;

public class healthbar_Sprite : MonoBehaviour {

	public SpriteManager healthbarSpriteManager;
	public GameObject HealthBarClient;

	private Sprite healthbarSprite;

	public void Start()
	{
		HealthBarClient = GameObject.Find ("Healthbar");
		DrawHealthBar ();
	}

	//create sprite
	public void DrawHealthBar()
	{
		healthbarSprite = healthbarSpriteManager.AddSprite (HealthBarClient, 6f, 1f, new Vector2(0f, 0.9f), new Vector2(0.1f, 0.1f), Vector3.zero, false);
	}
	
	public void Update()
	{
		//Rotate sprite to face camera
		HealthBarClient.transform.eulerAngles = new Vector3 (
			Camera.main.transform.eulerAngles.x,
			Camera.main.transform.parent.gameObject.transform.eulerAngles.y,
			HealthBarClient.transform.eulerAngles.z
			);

		healthbarSpriteManager.Transform (healthbarSprite);
	}
}
