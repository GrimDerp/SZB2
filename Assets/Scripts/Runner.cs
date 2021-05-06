using UnityEngine;
using MonoBehavior;
using UnityEngine.EventSystems;
using UnityStandardAssets.ImageEffects;

public class Runner : MonoBehaviour {

	public Vector3 offset, rotationVelocity;
	public static float distanceTraveled;
	public Vector3 boostVelocity, jumpVelocity;
	public float acceleration;
	public float gameOverY;
	private bool touchingPlatform, IsGUIVisible;
	private Vector3 startPosition;

	private static int boosts;
	
	void Awake(){
		//this should fix the "no input within 30 seconds crashes the game" bug...
		//six months later: it was *supposed* to fix the crash on launch bug...
        DontDestroyOnLoad(transform.Runner);
	 }

	void OnCollisionEnter(){
		touchingPlatform = true;
	}
	
	void OnCollisionExit(){
		touchingPlatform = false;
	}
	
	
	void Update () {
		transform.Rotate(rotationVelocity * Time.deltaTime);
		if (Input.GetButtonDown ("Jump")) {
					if (touchingPlatform) {
							GetComponent<Rigidbody>().AddForce (jumpVelocity, ForceMode.VelocityChange);
							touchingPlatform = false;
							
								
						}
						else if (boosts > 0) {
								GetComponent<Rigidbody>().AddForce (boostVelocity, ForceMode.VelocityChange);
								boosts -= 1;
								GUIManager.SetBoosts(boosts);
						}
				}
			distanceTraveled = transform.localPosition.x;
			GUIManager.SetDistance (distanceTraveled);


			if(transform.localPosition.y < gameOverY){
				GameEventManager.TriggerGameOver();
			}
		}
	
		private void GameStart () {
			boosts = 3;
			GUIManager.SetBoosts(boosts);
			distanceTraveled = 0f;
			GUIManager.SetDistance(distanceTraveled);
			transform.localPosition = startPosition;
			GetComponent<Renderer>().enabled = true;
			GetComponent<Rigidbody>().isKinematic = false;
			enabled = true;
		}

	public static void AddBoost(){
		boosts += 1;
		GUIManager.SetBoosts(boosts);

	}

	private void GameOver () {
		GetComponent<Renderer>().enabled = false;
		GetComponent<Rigidbody>().isKinematic = true;
		enabled = false;
		
	}

	void fixedUpdate (){
		if (touchingPlatform) {
			GetComponent<Rigidbody>().AddForce(acceleration, 0f, 0f, ForceMode.Acceleration);		
		}
	}



	void Start () {
				GameEventManager.GameStart += GameStart;
				GameEventManager.GameOver += GameOver;
				startPosition = transform.localPosition;
				GetComponent<Renderer>().enabled = false;
				GetComponent<Rigidbody>().isKinematic = true;
				enabled = false;
		}

	}

	