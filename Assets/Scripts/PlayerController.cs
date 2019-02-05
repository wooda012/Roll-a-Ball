using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
	public float speed;
	public float jump;

	private Rigidbody rb;
	private bool grounded;

	private int count;

	public Text countText;
	public Text winText;

	public AudioSource collectSound;

	void Start(){
		rb = GetComponent<Rigidbody>();
		count = 0;
		SetCountText();
		winText.text = "";
		grounded = true;
		collectSound = GetComponent<AudioSource>();
	}

	void FixedUpdate(){

		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0, moveVertical);
		rb.AddForce(movement * speed);
		if(Input.GetKeyDown(KeyCode.Space) && grounded == true){
			rb.AddForce(new Vector3(0,jump,0), ForceMode.Impulse);
		}
	}

	void OnTriggerEnter(Collider other){
		if(other.gameObject.CompareTag("Pick Up")){
			other.gameObject.SetActive(false);
			count = count + 1;
			collectSound.Play();
			SetCountText();
		}
	}

	void OnCollisionEnter(Collision gr){
		if(gr.gameObject.CompareTag("Ground")){
			grounded = true;
		}
	}

	void OnCollisionExit(Collision gr){
		if(gr.gameObject.CompareTag("Ground")){
			grounded = false;
		}
	}

	void SetCountText(){
		countText.text = "Count: " + count.ToString();
		if(count >= 12){
			winText.text = "You Win!";
		}
	}


}
