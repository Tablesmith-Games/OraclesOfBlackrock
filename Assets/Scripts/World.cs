using UnityEngine;
using System.Collections;

public class World : MonoBehaviour {
	RaycastHit hit;
	
	private float raycastLength = 100;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		
		if (Physics.Raycast (ray, out hit, raycastLength)) 
		{
			if(hit.collider.name == "Ground")
			{

			}
		}
		
		Debug.DrawRay (ray.origin, ray.direction * raycastLength, Color.yellow);
	}
}
