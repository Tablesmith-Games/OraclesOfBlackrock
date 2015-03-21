using UnityEngine;
using System.Collections;
using Pathfinding;

public class UnitPath : MonoBehaviour {

	private Seeker seeker;
	private CharacterController controller;
	private Path path;
	private Unit unit;
	private Vector3 desiredPos;
	private Vector3 reformPos;
	public GameObject rallyPoint;
	private bool reformed;
	private float spacing = 4;
	
	public float speed;
	
	//The max distance from AI to a waypoint for it to continue
	public float defualtWaypointDistance = 5f;
	
	//Current waypoint
	private int currentWaypoint = 0;

	public void Start () 
	{
		//Get the seeker component attached to this GameObject
		seeker = GetComponent<Seeker>();
		controller = GetComponent<CharacterController> ();
		unit = GetComponent<Unit> ();
		if(unit.isInSquad)
		{
			reformed = true;
		}
		else{
			reformed = false;
		}
	}

	public void LateUpdate()
	{

		if (unit.Selected && unit.isWalkable) 
		{
					if (Input.GetMouseButtonDown (1)) 
						{
								//Debug.Log("in get" + reformed + ", " + currentWaypoint);
								desiredPos = MousePoint.RightClickPoint;
								rallyPoint.transform.position = desiredPos;
								seeker.StartPath (transform.position, desiredPos, OnPathComplete);
								if(reformed == true){
									reformed = false;
								}
						}
		 }
	}

	public void OnPathComplete (Path p) {
		//We got our path back
		if (!p.error) {

			path = p;
			//Reset waypoint counter
			currentWaypoint = 0;
			//Debug.Log("on path complete set waypoint " + reformed + ", " + currentWaypoint);
			
		} else {
			//blah

		}
	}
	
	
	public void FixedUpdate()
	{
		//Debug.Log("fixed update " + reformed + ", " + currentWaypoint);

		if (!unit.isWalkable)
		{
			//Debug.Log("unit is not walkable " + reformed + ", " + currentWaypoint);
			return;
		}
		if (path == null)
		{
			//Debug.Log("path is null " + reformed + ", " + currentWaypoint);
			return;
		}
		if (currentWaypoint >= path.vectorPath.Count)
		{
			//Debug.Log("currentWaypoint is greater than vectorpath " + reformed + ", " + currentWaypoint);
			return;
		}

		if(!reformed && unit.isInSquad && currentWaypoint == path.vectorPath.Count - 1)
		{
			//Debug.Log("In if not reformed" + reformed + ", " + currentWaypoint);
			ReformSquad();
		}


		Vector3 dir = (path.vectorPath [currentWaypoint] - transform.position).normalized;
		dir *= speed * Time.fixedDeltaTime;
		controller.SimpleMove (dir);

		if(currentWaypoint < path.vectorPath.Count - 1)
		{
			transform.LookAt (new Vector3 (path.vectorPath [currentWaypoint].x, transform.position.y, path.vectorPath [currentWaypoint].z));
			//Debug.Log("Look at " + reformed + ", " + currentWaypoint);
		}

		float nextWaypointDistance = defualtWaypointDistance;
		//Debug.Log("next waypoint distance " + reformed + ", " + currentWaypoint + ", " + nextWaypointDistance);
		if(currentWaypoint == path.vectorPath.Count - 1)
		{
			//Debug.Log("next waypoint to 0f " + reformed + ", " + currentWaypoint + ", " + nextWaypointDistance);
			nextWaypointDistance = 0f;
		} else if (currentWaypoint == 1){
			//Debug.Log("in else if" + reformed + ", " + currentWaypoint);
			nextWaypointDistance = 20f;
		}

		if(Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]) <= nextWaypointDistance)
		{
			//Debug.Log("current waypoint ++" + reformed + ", " + currentWaypoint);
			currentWaypoint += 1;
			return;
		}

		if(Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]) < 0.5f && currentWaypoint == path.vectorPath.Count - 1)
		{
			//Debug.Log("path to null " + reformed + ", " + currentWaypoint + ", " + nextWaypointDistance);
			path = null;
			return;
		}
	}

	public void ReformSquad()
	{
		if(unit.positionInSquad == 1)
		{
			reformPos.x = rallyPoint.transform.position.x - spacing;
			reformPos.z = rallyPoint.transform.position.z;
			seeker.StartPath (transform.position, reformPos, OnPathComplete);
		}
		else if(unit.positionInSquad == 2)
		{
			reformPos = rallyPoint.transform.position;
			seeker.StartPath (transform.position, reformPos, OnPathComplete);
		}
		else if(unit.positionInSquad == 3)
		{
			reformPos.x = rallyPoint.transform.position.x + spacing;
			reformPos.z = rallyPoint.transform.position.z;
			seeker.StartPath (transform.position, reformPos, OnPathComplete);
		}
		else if(unit.positionInSquad == 4)
		{
			reformPos.x = rallyPoint.transform.position.x - spacing;
			reformPos.z = rallyPoint.transform.position.z - spacing;
			seeker.StartPath (transform.position, reformPos, OnPathComplete);
		}
		else if(unit.positionInSquad == 5)
		{
			reformPos.x = rallyPoint.transform.position.x;
			reformPos.z = rallyPoint.transform.position.z - spacing;
			seeker.StartPath (transform.position, reformPos, OnPathComplete);
		}
		else if(unit.positionInSquad == 6)
		{
			reformPos.x = rallyPoint.transform.position.x + spacing;
			reformPos.z= rallyPoint.transform.position.z - spacing;
			seeker.StartPath (transform.position, reformPos, OnPathComplete);
		}
		reformed = true;
	}
}
