using UnityEngine;
using System.Collections;

public class HealthbarClient : MonoBehaviour {
	
	public Sprite myHealthBarSprite;
	public SpriteManager HealthbarSpriteManager;

	void Start()
	{
		HealthbarSpriteManager = GameObject.Find ("healthbar_SpriteManager").GetComponent<SpriteManager> ();
	}

	void Update()
	{
		//Debug.Log("Overall parent is: " + transform.root.gameObject.GetComponent<Squad> ());
				if (transform.parent.gameObject.GetComponent<Unit> ()) {
						
						if (!transform.parent.gameObject.GetComponent<Unit>().isInSquad && transform.parent.gameObject.GetComponent<Unit> ().Selected == true) {
								if (myHealthBarSprite.hidden == true) {
										HealthbarSpriteManager.ShowSprite (myHealthBarSprite);
										HealthbarSpriteManager.UpdateBounds ();
								}

								//rotate and re-pos sprite
								transform.eulerAngles = new Vector3 (
				Camera.main.transform.eulerAngles.x,
				Camera.main.transform.parent.gameObject.transform.eulerAngles.y,
				transform.eulerAngles.z
								);

								HealthbarSpriteManager.Transform (myHealthBarSprite);

						} else {

								if (myHealthBarSprite.hidden == false)
										HealthbarSpriteManager.HideSprite (myHealthBarSprite);
						}
		} else if(transform.root.gameObject.GetComponent<Squad>() != null){
			transform.position = new Vector3(transform.root.FindChild("Cube").position.x, transform.position.y, transform.root.FindChild("Cube").position.z);
				if(transform.root.gameObject.GetComponent<Squad> ().Selected == true)
				{
					if (myHealthBarSprite.hidden == true) {
						HealthbarSpriteManager.ShowSprite (myHealthBarSprite);
						HealthbarSpriteManager.UpdateBounds ();
					}
					
					//rotate and re-pos sprite
					transform.eulerAngles = new Vector3 (
						Camera.main.transform.eulerAngles.x,
						Camera.main.transform.parent.gameObject.transform.eulerAngles.y,
						transform.eulerAngles.z
						);
					
					HealthbarSpriteManager.Transform (myHealthBarSprite);
				} else {
					if (myHealthBarSprite.hidden == false)
						HealthbarSpriteManager.HideSprite (myHealthBarSprite);
				}
				//Debug.Log("Overall parent is: " + transform.root.gameObject);
			}
		}
}
