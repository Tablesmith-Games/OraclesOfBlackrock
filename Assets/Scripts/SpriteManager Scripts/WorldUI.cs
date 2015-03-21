using UnityEngine;
using System.Collections;

public class WorldUI : MonoBehaviour {

	public SpriteManager spriteManager;

	public void DrawCurlAnimation()
	{
		GameObject Client = GameObject.Find ("Client");
		Sprite ClientSprite = spriteManager.AddSprite (Client, 10f, 10f, new Vector2(0f,0.75f), new Vector2(0.16666667f, 0.25f), Vector3.zero, false);


	}
}
