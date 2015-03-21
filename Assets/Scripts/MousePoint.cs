using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MousePoint : MonoBehaviour {

	#region Class Variables

	RaycastHit hit;

	//Card Handling
	public GameObject field;
	
	public bool selectedCardOnField = false;
	public GameObject target;
	public GameObject clone;
	public GameObject cardToDrag;
	
	public Creature attacker;
	public Creature defender;

	//Resource Handling
	public GameObject library;

	//Field Handling
	public static Vector3 RightClickPoint;
	public static ArrayList CurrentlySelectedUnits = new ArrayList (); //of gameobjects
	public static ArrayList UnitsOnScreen = new ArrayList ();
	public static ArrayList UnitsInDrag = new ArrayList();
	private bool FinishDragOnThisFrame;
	private bool StartedDrag;

	public GUIStyle MouseDragSkin;

	public GameObject Target;

	private static bool displayBuildingOption = false;

	private static Vector3 mouseDownPoint;
	private static Vector3 currentMousePoint; //In World Space


	private static bool UserisDragging;
	private static float TimeLimitBeeforeDeclareDrag = 1f;
	private static float TimeLeftBeforeDeclareDrag;
	private static Vector2 MouseDragStart;

	private static float clickDragZone = 1.3f;

	//GUI
	private float boxWidth;
	private float boxHeight;
	private float boxTop;
	private float boxLeft;
	private static Vector2 boxStart;
	private static Vector2 boxFinish;


	#endregion
	
	// Update is called once per frame
	void Update () {
		Target.transform.position = new Vector3 (10,0,0);
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

		if (Physics.Raycast (ray, out hit, Mathf.Infinity)) {

			currentMousePoint = hit.point;
						//store point at mousebutton down
						if (Input.GetMouseButtonDown (0)) {
							mouseDownPoint = hit.point;
							TimeLeftBeforeDeclareDrag = TimeLimitBeeforeDeclareDrag;
							MouseDragStart = Input.mousePosition;
							StartedDrag = true;
						}	
						else if(Input.GetMouseButton(0))
						{
							//if the user is not dragging, do tests
							if(!UserisDragging)
							{
								TimeLeftBeforeDeclareDrag -= Time.deltaTime;
								if(TimeLeftBeforeDeclareDrag <= 0f || UserDraggingByPosition(MouseDragStart, Input.mousePosition))
								{
									UserisDragging = true;
									if(hit.collider.gameObject.GetComponent<Card>() && !hit.collider.gameObject.GetComponent<Card>().isInHand)
									{
										if(hit.collider.gameObject.GetComponent<Creature>().endurance > 0)
										{
											if(TurnManager.isTurn)
											{
												clone = Instantiate(target, hit.point, Quaternion.identity) as GameObject;
												selectedCardOnField = true;
												attacker = hit.collider.gameObject.GetComponent<Creature>();
											}
										} else {
											clone = Instantiate(target, hit.point, Quaternion.identity) as GameObject;
											selectedCardOnField = true;
										}
									}
								}
							}

							//User is dragging, compute
							if(UserisDragging)
							{
								if(hit.collider.gameObject.GetComponent<Card>() && hit.collider.gameObject.GetComponent<Card>().isInHand)
								{
									cardToDrag = hit.collider.gameObject;
								}
								if(cardToDrag != null && Mathf.Abs(cardToDrag.transform.position.x - currentMousePoint.x) < 6.0 && Mathf.Abs(cardToDrag.transform.position.z - currentMousePoint.z) < 6.0)
								{
									cardToDrag.transform.position = new Vector3(currentMousePoint.x, 1, currentMousePoint.z);
								}
								if(selectedCardOnField)
								{
									clone.transform.position = new Vector3(hit.point.x, 1, hit.point.z);
								}
							}
						}
						else if(Input.GetMouseButtonUp(0))
						{
							if(UserisDragging)
							{

								if(hit.collider.gameObject.GetComponent<Card>() && hit.collider.gameObject.GetComponent<Card>().isInHand){
									if (field.GetComponent<Collider>().bounds.IntersectRay(ray))
									{
										cardToDrag.GetComponent<Card>().isInHand = false;
										//Debug.Log (cardToDrag.GetComponent<Card>().cardName);
										Instantiate(library.GetComponent<Library>().findUnit(cardToDrag.GetComponent<Card>().cardName), new Vector3(cardToDrag.transform.position.x, cardToDrag.transform.position.y, cardToDrag.transform.position.z), Quaternion.identity);
										Destroy(cardToDrag);
										cardToDrag = null;
										UnitsInDrag.Clear();
										//Transefer card to field
									}
								}
								if(selectedCardOnField)
								{
									Destroy(clone
						        );
									selectedCardOnField = false;
									if(!hit.collider.gameObject.GetComponent<Card>().isInHand)
									{
										defender = hit.collider.gameObject.GetComponent<Creature>();
									}
									GameObject.Find("Game Manager").GetComponent<TurnManager>().commandAttack();
								}
								FinishDragOnThisFrame = true;
							}
							TimeLeftBeforeDeclareDrag = 0f;
							UserisDragging = false;
						}
						//Mouse Click
						if (!UserisDragging) {
								if (hit.collider.name == "Ground") {
										if (Input.GetMouseButtonDown (1)) {
											//if(GameObject.Find("Target Instantiated") == null){
												GameObject TargetObj = Instantiate (Target, hit.point, Quaternion.identity) as GameObject;
												TargetObj.name = "Target Instantiated";
												RightClickPoint = hit.point;
												//}
										} else if (Input.GetMouseButtonUp (0) && DidUserClickLeftMouse (mouseDownPoint)) {
												if (!Common.ShiftKeysDown ())
														DeselectGameObjectsIfSelected ();
										}
								} else {
										if (Input.GetMouseButtonUp (0) && DidUserClickLeftMouse (mouseDownPoint)) {
												if (hit.collider.gameObject.GetComponent<Unit>()) {
														
														//Found a unit we can select
														//Debug.Log ("Unit Found");

														if (!UnitAlreadyInCurrentlySelectedUnits (hit.collider.gameObject)) {
															
																//remove rest of units if not shiftkey
																if (!Common.ShiftKeysDown ()) {
																		DeselectGameObjectsIfSelected ();
																}
																
																GameObject SelectedObj = hit.collider.transform.FindChild ("Selected").gameObject;
																SelectedObj.SetActive (true);
																	if(hit.collider.gameObject.GetComponent<Unit>().isInSquad)
																	{
																		hit.collider.gameObject.GetComponentInParent<Squad>().Selected = true;
																		hit.collider.gameObject.GetComponentInParent<Squad>().Select();
																	}
																//add unit to currently selected unit
																if(!hit.collider.gameObject.GetComponent<Unit>().isInSquad)
																{
																	CurrentlySelectedUnits.Add (hit.collider.gameObject);

																	hit.collider.gameObject.GetComponent<Unit>().Selected = true;
																} 
																else if(hit.collider.gameObject.GetComponent<Unit>().isInSquad)
																{
																	foreach(Unit child in hit.collider.gameObject.GetComponentInParent<Squad>().children)
																	{
																		Unit unitToAdd = child;
																		Debug.Log (child);
																		child.Selected = true;
																		CurrentlySelectedUnits.Add(unitToAdd.gameObject);
																		Debug.Log (child.gameObject);
																	}
																}

																else if(hit.collider.gameObject.GetComponent<Unit>().isBuilding && !hit.collider.gameObject.GetComponent<Unit>().isBuilt)
																{
																	displayBuildingOption = true;
																	hit.collider.gameObject.GetComponent<Animation>().Play();
																}
																

														} else {
																//unit is already in currently selected unit arraylist
																//remove unit!
																if (Common.ShiftKeysDown ())
																{
																		RemoveUnitFromCurrentlySelectedUnits (hit.collider.gameObject);
																}
																else {
																		DeselectGameObjectsIfSelected ();
																		GameObject SelectedObj = hit.collider.transform.FindChild ("Selected").gameObject;
																		SelectedObj.SetActive (true);
																		CurrentlySelectedUnits.Add (hit.collider.gameObject);
																		hit.collider.gameObject.GetComponent<Unit>().Selected = true;
																}
														}

												} else {
														if (!Common.ShiftKeysDown ())
																DeselectGameObjectsIfSelected ();
												}
										}
								}

						} else {
								if (Input.GetMouseButtonUp (0) && DidUserClickLeftMouse (mouseDownPoint)) {
										if (!Common.ShiftKeysDown ())
												DeselectGameObjectsIfSelected ();
								}
						} // End of raycast hit
				}//End of is not dragging
		if (!Common.ShiftKeysDown () && StartedDrag && UserisDragging)
		{
			DeselectGameObjectsIfSelected ();
			StartedDrag = false;
		}

		Debug.DrawRay (ray.origin, ray.direction * 1000, Color.yellow);

		if (UserisDragging && !hit.collider == hit.collider.gameObject.GetComponent<Card>() && cardToDrag == null) 
		{
			//Debug.Log(cardToDrag);
			//Gui Variables
			boxWidth = Camera.main.WorldToScreenPoint (mouseDownPoint).x - Camera.main.WorldToScreenPoint (currentMousePoint).x;
			boxHeight = Camera.main.WorldToScreenPoint (mouseDownPoint).y - Camera.main.WorldToScreenPoint (currentMousePoint).y;
			
			boxLeft = Input.mousePosition.x;
			boxTop = (Screen.height - Input.mousePosition.y) - boxHeight;

			if(Common.FloatToBool(boxWidth))
				if(Common.FloatToBool (boxHeight))
					boxStart = new Vector2(Input.mousePosition.x, Input.mousePosition.y + boxHeight);
				else
					boxStart = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
			else
				if(!Common.FloatToBool(boxWidth))
					if(Common.FloatToBool(boxHeight))
						boxStart = new Vector2(Input.mousePosition.x + boxWidth, Input.mousePosition.y + boxHeight);
					else
						boxStart = new Vector2(Input.mousePosition.x + boxWidth, Input.mousePosition.y);
			
			boxFinish = new Vector2 (
										boxStart.x + Common.Unsigned(boxWidth),
										boxStart.y - Common.Unsigned(boxHeight)
									);
		}
	}

	void LateUpdate()
	{
		UnitsInDrag.Clear ();
		//Debug.Log (UnitsOnScreen.Count);
		if (!hit.collider == hit.collider.gameObject.GetComponent<Card>() && cardToDrag == null &&(UserisDragging || FinishDragOnThisFrame) && UnitsOnScreen.Count > 0) {
						for (int i = 0; i < UnitsOnScreen.Count; i++) {
								Debug.Log (UnitsOnScreen [i]);
								GameObject UnitObj = UnitsOnScreen [i] as GameObject;
								Unit UnitScript = UnitObj.GetComponent<Unit> ();
								GameObject SelectedObj = UnitObj.transform.FindChild ("Selected").gameObject;
								//Debug.Log (UnitObj);
								if (!UnitAlreadyInDraggedUnits (UnitObj)) {
									//Debug.Log (UnitObj);
										if (UnitInsideDrag (UnitScript.ScreenPos)) {
												SelectedObj.SetActive (true);
												UnitsInDrag.Add (UnitObj);
										} else {
												if (!UnitAlreadyInCurrentlySelectedUnits (UnitObj))
														SelectedObj.SetActive (false);
							
										}
								}
						}
				}
				if (FinishDragOnThisFrame) 
				{
					FinishDragOnThisFrame = false;
					PutDraggedUnitsInCurrentlyelectedUnits();
				}
	}

	void OnGUI()
	{
		if(UserisDragging && !hit.collider == hit.collider.gameObject.GetComponent<Card>() && cardToDrag == null)
		{

			GUI.Box (new Rect(boxLeft,
			                  boxTop,
			                  boxWidth,
			                  boxHeight), "", MouseDragSkin);
		}
		if(displayBuildingOption)
		{
			GUI.Button(new Rect(100,100,100,50), "Build: 100 power");
		}
	}

	#region Helper
	//is user dragging realative to mouse drag start point
	public bool UserDraggingByPosition(Vector2 DragStartPoint, Vector2 NewPoint)
	{
		if (
			(NewPoint.x > DragStartPoint.x + clickDragZone || NewPoint.x < DragStartPoint.x - clickDragZone) ||
			(NewPoint.y > DragStartPoint.y + clickDragZone || NewPoint.y < DragStartPoint.y - clickDragZone)
		   )
			return true;
		else return false;
	}
	public bool DidUserClickLeftMouse(Vector3 hitPoint)
	{
		if(
			(mouseDownPoint.x < hitPoint.x + clickDragZone && mouseDownPoint.x > hitPoint.x - clickDragZone) &&
			(mouseDownPoint.y < hitPoint.y + clickDragZone && mouseDownPoint.y > hitPoint.y - clickDragZone) &&
			(mouseDownPoint.z < hitPoint.z + clickDragZone && mouseDownPoint.z > hitPoint.z - clickDragZone)
		   )
				return true; else return false;
		}

	public static void DeselectGameObjectsIfSelected()
	{
		if (CurrentlySelectedUnits.Count > 0) 
		{
			for(int i = 0; i < CurrentlySelectedUnits.Count; i++)
			{
				GameObject ArrayListUnit = CurrentlySelectedUnits[i] as GameObject;

				if(ArrayListUnit.GetComponent<Unit>().isBuilding)
				{
						displayBuildingOption = false;
				}
				if(ArrayListUnit.GetComponent<Unit>().isInSquad)
				{
					ArrayListUnit.transform.root.gameObject.GetComponent<Squad>().Selected = false;
				}
				ArrayListUnit.transform.FindChild ("Selected").gameObject.SetActive(false);
				ArrayListUnit.GetComponent<Unit>().Selected = false;
				
				
			}
			CurrentlySelectedUnits.Clear ();
		}
	}

	public static bool UnitAlreadyInCurrentlySelectedUnits(GameObject Unit)
	{
		if (CurrentlySelectedUnits.Count > 0) 
		{
			for(int i = 0; i < CurrentlySelectedUnits.Count; i++)
			{
				GameObject ArrayListUnit = CurrentlySelectedUnits[i] as GameObject;
				if(ArrayListUnit == Unit)
					return true;
			}
			
			return false;
		} else
			return false;
	}

	public void RemoveUnitFromCurrentlySelectedUnits(GameObject Unit)
	{
		if (CurrentlySelectedUnits.Count > 0) 
		{
			for(int i = 0; i < CurrentlySelectedUnits.Count; i++)
			{
				GameObject ArrayListUnit = CurrentlySelectedUnits[i] as GameObject;
				if(ArrayListUnit == Unit)
				{
					CurrentlySelectedUnits.RemoveAt (i);
					ArrayListUnit.transform.FindChild ("Selected").gameObject.SetActive(false);
				}
			}
			
			return;
		} else
			return;
	}

	public static bool UnitWithinScreenSpace(Vector2 UnitScreenPos)
	{
		if(
			(UnitScreenPos.x < Screen.width && UnitScreenPos.y < Screen.height) &&
			(UnitScreenPos.x > 0f && UnitScreenPos.y > 0f)
			)
			return true;
		else
			return false;
	}

	public static void RemoveFromOnScreenUnits(GameObject Unit)
	{
		for (int i = 0; i < UnitsOnScreen.Count; i++) 
		{
			GameObject UnitObj = UnitsOnScreen[i] as GameObject;
			if(Unit == UnitObj)
			{
				UnitsOnScreen.RemoveAt (i);
				UnitObj.GetComponent<Unit>().OnScreen = false;
				return;
			}
		}
		return;
	}

	public static bool UnitInsideDrag(Vector2 UnitScreenPos)
	{
		if(
			(UnitScreenPos.x > boxStart.x && UnitScreenPos.y < boxStart.y) &&
			(UnitScreenPos.x < boxFinish.x && UnitScreenPos.y > boxFinish.y)
			) return true;
		else 
			return false;
	}

	public static bool UnitAlreadyInDraggedUnits(GameObject Unit)
	{
		if (UnitsInDrag.Count > 0) 
		{
			for(int i = 0; i < UnitsInDrag.Count; i++)
			{
				GameObject ArrayListUnit = UnitsInDrag[i] as GameObject;
				if(ArrayListUnit == Unit)
					return true;
			}
			
			return false;
		} else
			return false;
	}

	public static void PutDraggedUnitsInCurrentlyelectedUnits()
	{
		if (!Common.ShiftKeysDown ())
			DeselectGameObjectsIfSelected ();

		if (UnitsInDrag.Count > 0) {
						for (int i = 0; i < UnitsInDrag.Count; i++) {
								GameObject UnitObj = UnitsInDrag [i] as GameObject;

								if (!UnitAlreadyInCurrentlySelectedUnits (UnitObj)) {
									if(UnitObj.GetComponent<Unit>().isInSquad)
									{
										UnitObj.GetComponentInParent<Squad>().Selected = true;
										UnitObj.GetComponentInParent<Squad>().Select();			

										foreach(Unit child in UnitObj.GetComponentInParent<Squad>().children)
										{
											Unit unitToAdd = child;
											Debug.Log ("PutDraggedUnitsInCurrentlyelectedUnits" + unitToAdd);
											child.Selected = true;
											CurrentlySelectedUnits.Add(unitToAdd.gameObject);
											//Debug.Log (child.gameObject);
										}
									} else {
										CurrentlySelectedUnits.Add (UnitObj);
										UnitObj.GetComponent<Unit> ().Selected = true;
									}
									if(UnitObj.GetComponent<Unit> ().isBuilding && !UnitObj.GetComponent<Unit> ().isBuilt)
									{
										displayBuildingOption = true;
									}
								}
						}

						UnitsInDrag.Clear ();
				}
	}
	#endregion

}