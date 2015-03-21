using UnityEngine;
using System.Collections;

public class Mouse : MonoBehaviour {
		
//		#region Class Variables
//		
//		RaycastHit hit;
//
//		public GameObject field;
//
//		public bool selectedCardOnField = false;
//		public GameObject target;
//		public GameObject clone;
//		public GameObject cardToDrag;
//
//		public Creature attacker;
//		public Creature defender;
//		
//		public static Vector3 RightClickPoint;
//		
//		private static Vector3 mouseDownPoint;
//		public static Vector3 currentMousePoint; //In World Space
//		
//		private static float clickDragZone = 1.3f;
//
//		//Mouse Stuff
//		private bool FinishDragOnThisFrame;
//		private bool StartedDrag;
//		
//		private static bool UserisDragging;
//		private static float TimeLimitBeforeDeclareDrag = 1f;
//		private static float TimeLeftBeforeDeclareDrag;
//		private static Vector2 MouseDragStart;
//		
//		#endregion
//		
//		// Update is called once per frame
//		void Update () {
//			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
//
//			if (Physics.Raycast (ray, out hit, Mathf.Infinity, 1 << 0)) {
//
//			currentMousePoint = hit.point;
//
//			if (Input.GetMouseButtonDown (0)) {
//				mouseDownPoint = hit.point;
//				TimeLeftBeforeDeclareDrag = TimeLimitBeforeDeclareDrag;
//				MouseDragStart = Input.mousePosition;
//				StartedDrag = true;
//				mouseDownPoint = hit.point;
//			}	
//			else if(Input.GetMouseButton(0))
//			{
//				//if the user is not dragging, do tests
//				if(!UserisDragging)
//				{
//					TimeLeftBeforeDeclareDrag -= Time.deltaTime;
//					if(TimeLeftBeforeDeclareDrag <= 0f || UserDraggingByPosition(MouseDragStart, Input.mousePosition))
//					{
//						UserisDragging = true;	
//						if(hit.collider.gameObject.GetComponent<Card>() && !hit.collider.gameObject.GetComponent<Card>().isInHand)
//						{
//							if(hit.collider.gameObject.GetComponent<Creature>().endurance > 0)
//							{
//								if(TurnManager.isTurn)
//								{
//									clone = Instantiate(target, hit.point, Quaternion.identity) as GameObject;
//									selectedCardOnField = true;
//									attacker = hit.collider.gameObject.GetComponent<Creature>();
//								}
//							} else {
//							clone = Instantiate(target, hit.point, Quaternion.identity) as GameObject;
//							selectedCardOnField = true;
//							}
//						}
//					}
//				}
//				
//				//User is dragging, compute
//				if(UserisDragging)
//				{
//					if(hit.collider.gameObject.GetComponent<Card>() && hit.collider.gameObject.GetComponent<Card>().isInHand)
//					{
//						cardToDrag = hit.collider.gameObject;
//					}
//					if(cardToDrag != null && Mathf.Abs(cardToDrag.transform.position.x - currentMousePoint.x) < 6.0 && Mathf.Abs(cardToDrag.transform.position.z - currentMousePoint.z) < 6.0)
//					{
//						cardToDrag.transform.position = new Vector3(currentMousePoint.x, 1, currentMousePoint.z);
//					}
//					if(selectedCardOnField)
//					{
//						clone.transform.position = new Vector3(hit.point.x, 1, hit.point.z);
//					}
//				}
//			}
//			else if(Input.GetMouseButtonUp(0))
//			{
//				if(UserisDragging){
//					if(hit.collider.gameObject.GetComponent<Card>() && hit.collider.gameObject.GetComponent<Card>().isInHand){
////						if(hit.collider.gameObject.transform.position.x < field.transform.position.x + field.transform.localScale.x/2 &&
////						   hit.collider.gameObject.transform.position.x > field.transform.position.x - field.transform.localScale.x/2 &&
////						   hit.collider.gameObject.transform.position.z < field.transform.position.z + field.transform.localScale.z/2 &&
////						   hit.collider.gameObject.transform.position.z > field.transform.position.z - field.transform.localScale.z/2)
//						if (field.collider.bounds.IntersectRay(ray))
//						{
//							cardToDrag = null;
//							hit.collider.gameObject.GetComponent<Card>().isInHand = false;
//						}
//					}
//					if(selectedCardOnField)
//					{
//						Destroy(clone);
//						selectedCardOnField = false;
//						if(!hit.collider.gameObject.GetComponent<Card>().isInHand)
//						{
//							defender = hit.collider.gameObject.GetComponent<Creature>();
//						}
//						GameObject.Find("Game Manager").GetComponent<TurnManager>().commandAttack();
//					}
//
//					FinishDragOnThisFrame = true;
//				}
//				
//				TimeLeftBeforeDeclareDrag = 0f;
//				UserisDragging = false;
//
//
//				//Debug.Log("User stopped dragging: " + UserisDragging);
//			}
//
//			Debug.DrawRay (ray.origin, ray.direction * 1000, Color.yellow);
//			}
//		}
//
//		void LateUpdate()
//		{
//			
//			if ((UserisDragging || FinishDragOnThisFrame)) {
//				
//			}
//			if (FinishDragOnThisFrame) 
//			{
//				FinishDragOnThisFrame = false;
//			}
//		}
//
//		void OnGUI()
//		{
//
//		}
//		
//		#region Helper
//		public bool UserDraggingByPosition(Vector2 DragStartPoint, Vector2 NewPoint)
//		{
//			if (
//				(NewPoint.x > DragStartPoint.x + clickDragZone || NewPoint.x < DragStartPoint.x - clickDragZone) ||
//				(NewPoint.y > DragStartPoint.y + clickDragZone || NewPoint.y < DragStartPoint.y - clickDragZone)
//				)
//				return true;
//			else return false;
//		}
//		public bool DidUserClickLeftMouse(Vector3 hitPoint)
//		{
//			if(
//				(mouseDownPoint.x < hitPoint.x + clickDragZone && mouseDownPoint.x > hitPoint.x - clickDragZone) &&
//				(mouseDownPoint.y < hitPoint.y + clickDragZone && mouseDownPoint.y > hitPoint.y - clickDragZone) &&
//				(mouseDownPoint.z < hitPoint.z + clickDragZone && mouseDownPoint.z > hitPoint.z - clickDragZone)
//				)
//				return true; else return false;
//		}
//		#endregion
	}
