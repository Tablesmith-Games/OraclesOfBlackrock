using UnityEngine;
using System.Collections;
using Pathfinding;

public class AStarPath : MonoBehaviour {

	public Vector3 targetPosition;
	private Seeker seeker;
	private CharacterController controller;
	private Path path;

	public float speed;

	//The max distance from AI to a waypoint for it to continue
	public float nextWaypointDistance = 10;

	//Current waypoint
	private int currentWaypoint = 0;

	public void Start () {
		//Get the seeker component attached to this GameObject
		seeker = GetComponent<Seeker>();
		targetPosition = GameObject.Find ("TargetPos").transform.position;
		controller = GetComponent<CharacterController> ();
		//When the path has been calculated, it will be returned to the function OnPathComplete unless it was canceled by another path request
		seeker.StartPath (transform.position, targetPosition, OnPathComplete);
	}
	public void OnPathComplete (Path p) {
		//We got our path back
		if (!p.error) {
			path = p;
			//Reset waypoint counter
			currentWaypoint = 0;
		} else {
		}
	}


	public void FixedUpdate()
	{
		if (path == null)
			return;
		if (currentWaypoint >= path.vectorPath.Count)
			return;

		Vector3 dir = (path.vectorPath [currentWaypoint] - transform.position).normalized;
		dir *= speed * Time.fixedDeltaTime;
		controller.SimpleMove (dir);

		if(Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]) < nextWaypointDistance)
		{
			currentWaypoint++;
			return;
		}
	}
}