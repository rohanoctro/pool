using System.Collections;
using System.Collections.Generic;
using UnityEngine;
class Velocity 
{
	public float x;
	public float y;
	public float z;
	public Velocity(float x=0,float y=0,float z=0)
	{
		this.x = x;
		this.y = y;
		this.z = z;
	}
	public Velocity(Vector3 velocity)
	{
		this.x = velocity.x;
		this.y = velocity.y;
		this.z = velocity.z;
	}

}
public class StayInBound : MonoBehaviour {
	private Vector3 orignalPosition;
	private GameObject obj;
	private bool outOfBound;

	public static string isCollidingWith;
	public static Vector3 direction;
	public static float factor;
	private Rigidbody rb;

	private Ray ray;
	private RaycastHit hit;
	private LineRenderer line;

	private Velocity velocityBeforeCollisionCueBall;
	private Velocity velocityAfterCollisionCueBall;
	private Velocity velocityAfterCollisionRedBall;


	void Awake()
	{
		direction = Vector3.zero;
		factor = 0;
		isCollidingWith = "";
		rb = gameObject.GetComponent<Rigidbody> ();
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
	}


	// Use this for initialization
	void Start () {
		
		outOfBound = false;
		obj = GameObject.FindGameObjectWithTag ("Finish");
		if (obj != null) {
			Debug.Log("Found cube");
			Debug.Log(obj.transform.position);

			Debug.Log (obj.transform.localPosition);
		}
		orignalPosition = gameObject.transform.position;
		line = gameObject.GetComponent<LineRenderer> ();

		
	}
	
	// Update is called once per frame
	void Update () {
		if(gameObject.transform.position.z>=40.0f || gameObject.transform.position.z<=-40 || gameObject.transform.position.x>=22 || gameObject.transform.position.x<=-22)
		{
			gameObject.transform.position = orignalPosition;
			//gameObject.GetComponent<Rigidbody> ().Sleep ();
		}

		string name = "";

		if (PoolGameController.GameInstance.gameState== StatesOfGame.WAITING_FOR_STRIKE_STATE) {
			line.enabled = true;
			ray = new Ray ();
			ray.origin = gameObject.transform.position;
			ray.direction = PoolGameController.GameInstance.strikeDirection;
			Vector3 direction = Vector3.zero;
			if (Physics.SphereCast (ray, 0.35f, out hit)) {
					
				if (hit.collider.gameObject.tag == "redball") {
					line.SetPosition (0, gameObject.transform.position);
					line.SetPosition (1, hit.point);
					direction = (hit.collider.gameObject.transform.position - hit.point).normalized;

					Vector3 desiredPoint = hit.collider.gameObject.transform.position + 10.0f * direction;
					line.SetPosition (2, desiredPoint);


					line.startWidth = 0.3f;
					line.endWidth = 0.0f;
				
					name = hit.collider.gameObject.name;

				} else {
					line.SetPosition (0, gameObject.transform.position);
					line.SetPosition (1, gameObject.transform.position + PoolGameController.GameInstance.strikeDirection * 100.0f);
					line.SetPosition (2, line.GetPosition (1));
					line.startWidth = 0.3f;
					line.endWidth = 0.0f;
				}
			} else {
				line.SetPosition (0, gameObject.transform.position);
				line.SetPosition (1, gameObject.transform.position + PoolGameController.GameInstance.strikeDirection * 100.0f);
				line.SetPosition (2, line.GetPosition (1));
				line.startWidth = 0.3f;
				line.endWidth = 0.0f;
			}
			if (Input.GetMouseButtonUp (0	)) {
				StayInBound.isCollidingWith = name;
				StayInBound.direction = direction;
			}

		}
		else 
		{
			line.startWidth = 0;
			line.endWidth = 0;
		}




		
	}

	public void OnCollisionEnter(Collision col)
	{
		if (col.collider.gameObject.tag == "redball") {
			PoolGameController.GameInstance.check2 = 0;
		}
		if (col.collider.gameObject.tag == "redball" && col.collider.gameObject.name==isCollidingWith) {
			//Add force Logic 
			velocityBeforeCollisionCueBall=new Velocity(gameObject.GetComponent<Rigidbody>().velocity);
			Rigidbody rb = col.collider.gameObject.GetComponent<Rigidbody> ();
			if (rb != null) {
				//rb.velocity = Vector3.zero;
			//	rb.angularVelocity = Vector3.zero;
			}
		
		}
	}
	public void OnCollisionStay(Collision col)
	{
		if(col.gameObject.tag=="redball" && col.collider.gameObject.name==isCollidingWith){
		
			Rigidbody rb = col.collider.gameObject.GetComponent<Rigidbody> ();	
		//	rb.velocity = Vector3.zero;
		//	rb.angularVelocity = Vector3.zero;
		//	float force = (PoolGameController.MAX_DISTANCE - PoolGameController.MIN_DISTANCE) * factor
		//		+ PoolGameController.MIN_DISTANCE;

		//	rb.AddForce (force * direction);
		
		}

	}
	public void OnCollisionExit(Collision col)
	{
		if (col.collider.tag == "redball" && col.collider.gameObject.name==isCollidingWith) {
//			Debug.LogError ("Object exited succesfully ");
			velocityAfterCollisionCueBall=new Velocity(gameObject.GetComponent<Rigidbody>().velocity);




			Rigidbody rb = col.collider.gameObject.GetComponent<Rigidbody> ();	

			float massOfCueBall = gameObject.GetComponent<Rigidbody> ().mass;
			float massOfRedBall=rb.mass;
			float constantK = massOfCueBall / massOfRedBall;
			velocityAfterCollisionRedBall = new Velocity ();

			velocityAfterCollisionRedBall.x = (velocityBeforeCollisionCueBall.x - velocityAfterCollisionCueBall.x) * constantK;
			velocityAfterCollisionRedBall.y = (velocityBeforeCollisionCueBall.y - velocityAfterCollisionRedBall.y) * constantK;
			velocityAfterCollisionRedBall.z = (velocityBeforeCollisionCueBall.z - velocityAfterCollisionCueBall.z) * constantK;

			float magnitude = Mathf.Sqrt (velocityAfterCollisionRedBall.x*velocityAfterCollisionRedBall.x+velocityAfterCollisionRedBall.z*velocityAfterCollisionRedBall.z);





			float force = (PoolGameController.MAX_DISTANCE - PoolGameController.MIN_DISTANCE) * factor
				+ PoolGameController.MIN_DISTANCE;
			force = force * factor*3.0f;
		//	rb.velocity =magnitude*3.0f * direction;
			rb.velocity=rb.velocity.magnitude*direction;
//			Debug.LogError ("Rb velocity "+rb.velocity.x+"    "+rb.velocity.y+"       "+rb.velocity.z+"Magnitude =="+rb.velocity.magnitude+"    Mag =="+magnitude);
			direction = Vector3.zero;
				isCollidingWith = null;
		}
	}
}
