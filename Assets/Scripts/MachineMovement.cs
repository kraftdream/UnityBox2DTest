using UnityEngine;
using System.Collections;

public class MachineMovement : MonoBehaviour {

	public Transform target;
	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		Vector3 dir = target.position - transform.position;
		float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
		if (Input.GetMouseButton (0)) {
			Vector3 forceVector = transform.TransformDirection(new Vector3(1000, 0, 0));//если нос ракеты направлен по оси x то new Vector3(force, 0, 0) 
			rigidbody2D.AddForce(new Vector2(forceVector.x, forceVector.y));
		}

		if (Input.GetMouseButton (1)) {
			Vector3 forceVector = transform.TransformDirection(new Vector3(-1000, 0, 0));//если нос ракеты направлен по оси x то new Vector3(force, 0, 0) 
			rigidbody2D.AddForce(new Vector2(forceVector.x, forceVector.y));
		}
	}
	
}
