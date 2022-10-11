using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	[Header("Visuals")]
	public GameObject model;
	
	[Header("Movement")]
	public float speed = 5f;
	public float jumpingVelocity = 5f;
	public float movingVelocity;
	public float rotationSpeed;
	private Rigidbody rb;
	private bool canJump;
    // Start is called before the first frame update
    void Start()
	{
		rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
	{
		//Raycast to identify if player can jump
		RaycastHit hit;
		if(Physics.Raycast(transform.position, Vector3.down, out hit, 1.01f)){
			canJump = true;
		}
		
		ProcessInput();
	}
    
	void ProcessInput(){
		//Move in the XZ plane.
		rb.velocity = new Vector3(
			0f,
			rb.velocity.y,
			0f);
		
		if(Input.GetKey(KeyCode.D)){
			rb.velocity = new Vector3(
				movingVelocity,
				rb.velocity.y,
				rb.velocity.z
			);
			model.transform.rotation = Quaternion.Lerp(
				model.transform.rotation, 
				Quaternion.Euler(0, 90, 0), 
				Time.deltaTime * rotationSpeed);
		}
		if(Input.GetKey(KeyCode.A)){
			rb.velocity = new Vector3(
				-movingVelocity,
				rb.velocity.y,
				rb.velocity.z
			);
			model.transform.rotation = Quaternion.Lerp(
				model.transform.rotation, 
				Quaternion.Euler(0, 270, 0), 
				Time.deltaTime * rotationSpeed);
			
		}
		if(Input.GetKey(KeyCode.W)){
			rb.velocity = new Vector3(
				rb.velocity.x,
				rb.velocity.y,
				movingVelocity
			);
			model.transform.rotation = Quaternion.Lerp(
				model.transform.rotation, 
				Quaternion.Euler(0, 0, 0), 
				Time.deltaTime * rotationSpeed);
		}
		if(Input.GetKey(KeyCode.S)){
			rb.velocity = new Vector3(
				rb.velocity.x,
				rb.velocity.y,
				-movingVelocity
			);
			model.transform.rotation = Quaternion.Lerp(
				model.transform.rotation, 
				Quaternion.Euler(0, 180, 0), 
				Time.deltaTime * rotationSpeed);
		}
		
		//Process Jump
		if(canJump && Input.GetKeyDown(KeyCode.Space)){
			canJump = false;
			//rb.AddForce(0f, jumpingVelocity, 0f);
			rb.velocity = new Vector3(
				rb.velocity.x,
				jumpingVelocity,
				rb.velocity.z
			);
		}
	}
}
