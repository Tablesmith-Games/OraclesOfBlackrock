using UnityEngine;
using System.Collections;

/* 
Class to control the camera within the game world.
Camera will move up, down, left and right when the users mouse hits the side of the screen in 2D space.
Camera will check desired location, will stop if over limits.
Camera can also be controlled by W,A,S,D keys, will call the same movement as the mouse events.
*/


public class WorldCamera : MonoBehaviour {
	
	#region structs
	
	//box limits Struct
	public struct BoxLimit {
		public float LeftLimit;
		public float RightLimit;
		public float TopLimit;
		public float BottomLimit;
		
	}
	
	#endregion
	
	
	#region class variables
	
	public static BoxLimit cameraLimits       = new BoxLimit();
	public static BoxLimit mouseScrollLimits  = new BoxLimit();
	public static WorldCamera Instance;
	
	private float cameraMoveSpeed = 60f;
	private float shiftBonus      = 45f;
	private float mouseBoundary   = 25f;

	private float mouseX;

	public Terrain WorldTerrain;
	public float WorldTerrainPadding = 25f;

	
	#endregion
	
	
	void Awake()
	{
		Instance = this;
	}
	
	
	void Start () {
		
		//Declare camera limits
		cameraLimits.LeftLimit   = WorldTerrain.transform.position.x + WorldTerrainPadding;
		cameraLimits.RightLimit  = WorldTerrain.terrainData.size.x - WorldTerrainPadding;
		cameraLimits.TopLimit    = WorldTerrain.terrainData.size.z - WorldTerrainPadding;
		cameraLimits.BottomLimit = WorldTerrain.transform.position.z + WorldTerrainPadding;
		
		//Declare Mouse Scroll Limits
		mouseScrollLimits.LeftLimit   = mouseBoundary;
		mouseScrollLimits.RightLimit  = mouseBoundary;
		mouseScrollLimits.TopLimit    = mouseBoundary;
		mouseScrollLimits.BottomLimit = mouseBoundary;
		
	}
	
	
	public void HandleMouseRotation()
	{
		var easeFactor = 10f;
		if(Input.GetMouseButton(1) && Input.GetKey(KeyCode.LeftControl))
		{
			if(Input.mousePosition.x != mouseX)
			{
				var cameraRotationY = (Input.mousePosition.x - mouseX) * easeFactor * Time.deltaTime;
				this.transform.Rotate (0, cameraRotationY, 0);
			}
		}
	}
	
	void LateUpdate () {

		HandleMouseRotation ();
		
		if(CheckIfUserCameraInput())
		{
			Vector3 desiredTranslation = GetDesiredTranslation();
			
			if(!isDesiredPositionOverBoundaries(desiredTranslation))
			{
				this.transform.Translate(desiredTranslation);
			}
		}

		mouseX = Input.mousePosition.x;
	}
	//Check if the user is inputting commands for the camera to move
	public bool CheckIfUserCameraInput()
	{
		bool keyboardMove;
		bool mouseMove;
		bool canMove;
		
		//check keyboard		
		if(WorldCamera.AreCameraKeyboardButtonsPressed()){
			keyboardMove = true;			
		} else {
			keyboardMove = false;
		}
		
		//check mouse position
		if(WorldCamera.IsMousePositionWithinBoundaries())
			mouseMove = true; else mouseMove = false;
		
		
		if(keyboardMove || mouseMove)
			canMove = true; else canMove = false;
		
		return canMove;
	}
	
	
	
	
	//Works out the cameras desired location depending on the players input
	public Vector3 GetDesiredTranslation()
	{
		float moveSpeed = 0f;
		Vector3 desiredTranslation = new Vector3 ();
		
		if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
			moveSpeed = (cameraMoveSpeed + shiftBonus) * Time.deltaTime;
		else
			moveSpeed = cameraMoveSpeed * Time.deltaTime;
		
		
		//move via keyboard
		if(Input.GetKey(KeyCode.W) || Input.mousePosition.y > (Screen.height - mouseScrollLimits.TopLimit))
			desiredTranslation += Vector3.forward * moveSpeed;

		
		if(Input.GetKey(KeyCode.S) || Input.mousePosition.y < mouseScrollLimits.BottomLimit)
		   desiredTranslation += Vector3.back * moveSpeed;

		
		if(Input.GetKey(KeyCode.A) || Input.mousePosition.x < mouseScrollLimits.LeftLimit)
		   desiredTranslation += Vector3.left * moveSpeed;	

		
		if(Input.GetKey(KeyCode.D) || Input.mousePosition.x > (Screen.width - mouseScrollLimits.RightLimit))
		   desiredTranslation += Vector3.right * moveSpeed;

		
		return desiredTranslation;
	}
	
	
	
	
	
	//checks if the desired position crosses boundaries
	public bool isDesiredPositionOverBoundaries(Vector3 desiredTranslation)
	{
		Vector3 desiredWorldPosition = this.transform.TransformPoint (desiredTranslation);

		bool overBoundaries = false;
		//check boundaries
		if((desiredWorldPosition.x) < cameraLimits.LeftLimit)
			overBoundaries = true;
		
		if((desiredWorldPosition.x) > cameraLimits.RightLimit)
			overBoundaries = true;
		
		if((desiredWorldPosition.z) > cameraLimits.TopLimit)
			overBoundaries = true;
		
		if((desiredWorldPosition.z) < cameraLimits.BottomLimit)
			overBoundaries = true;
		
		return overBoundaries;
	}
	
	
	
	
	
	#region Helper functions
	
	public static bool AreCameraKeyboardButtonsPressed()
	{
		if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
			return true; else return false;
	}
	
	public static bool IsMousePositionWithinBoundaries()
	{
		if(
			(Input.mousePosition.x < mouseScrollLimits.LeftLimit && Input.mousePosition.x > -5) ||
			(Input.mousePosition.x > (Screen.width - mouseScrollLimits.RightLimit) && Input.mousePosition.x < (Screen.width + 5)) ||
			(Input.mousePosition.y < mouseScrollLimits.BottomLimit && Input.mousePosition.y > -5) ||
			(Input.mousePosition.y > (Screen.height - mouseScrollLimits.TopLimit) && Input.mousePosition.y < (Screen.height + 5))
			)
			return true; else return false;
	}
	#endregion
}

