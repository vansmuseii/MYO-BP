using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {

	public GameObject Sphere; //reference to the sphere
	private Vector3 throwSpeed = new Vector3(0, 26, 40); //This value is a sure basket, we'll modify this using the forcemeter
	public Vector3 spherePos; //starting ball position
	private bool thrown = false; //if ball has been thrown, prevents 2 or more balls
	private GameObject sphereClone; //we don't use the original prefab
	
//	public GameObject availableShotsGO; //ScoreText game object reference
//	private int availableShots = 5;
	
	public GameObject meter; //references to the force meter
	public GameObject arrow;
	private float arrowSpeed = 0.3f; //Difficulty, higher value = faster arrow movement
	private bool right = true; //used to revers arrow movement

//	public GUITexture graphics;
//	public Texture2D texture2d;
	
//	public GameObject gameOver; //game over text

	private int availableShots = 2;




	// Use this for initialization
	void Start () {
		/* Increase Gravity */
		//Physics.gravity = new Vector3(0, 0, 0);

	
	}
	
	// Update is called once per physics thing
	void FixedUpdate () {
		/* Move Meter Arrow */
	
		
/*		if (arrow.transform.position.x < 4.7f && right)
		{
			arrow.transform.position += new Vector3(arrowSpeed, 0, 0);
		}
		if (arrow.transform.position.x >= 4.7f)
		{
			right = false;
		}
		if (right == false)
		{
			arrow.transform.position -= new Vector3(arrowSpeed, 0, 0);
		}
		if ( arrow.transform.position.x <= -4.7f)
		{
			right = true;
		}  */



		/* Shoot ball on Tap */
		
		if (Input.GetKeyDown(KeyCode.Space)) //&& !thrown && availableShots > 0)//Input.GetButton("Fire1") && !thrown) //&& availableShots > 0)
		{
			Debug.Log("Hello");


			thrown = true;
			availableShots--;
//			availableShotsGO.GetComponent().text = availableShots.ToString();
			
			sphereClone = Instantiate(Sphere, spherePos, transform.rotation) as GameObject;
			throwSpeed.y = throwSpeed.y + arrow.transform.position.x;
			throwSpeed.z = throwSpeed.z + arrow.transform.position.x;

//			sphereClone.rigidbody.AddForce(throwSpeed, ForceMode.Impulse);

			
			sphereClone.GetComponent<Rigidbody>().AddForce(throwSpeed, ForceMode.Impulse);
//			audio.Play(); //play shoot sound
		}

//***** THIS IS WHERE WE WILL CHECK TO SEE IF THE BALL HIT THE COLLIDER OR IF IT HIT THE TABLE. 
		/* Remove Ball when it hits the floor */
		
		if (sphereClone != null && sphereClone.transform.position.y < -1.57f)
		{
			Debug.Log("Destroy!!!!!");
			Destroy(sphereClone);
			thrown = false;
			throwSpeed = new Vector3(0, 26, 40);//Reset perfect shot variable

//***** We can update this to check if it is the next players turn or is the player won. 
			/* Check if out of shots */
			
		//	if (availableShots == 0)
		//	{
		//		arrow.renderer.enabled = false;
		//		Instantiate(gameOver, new Vector3(0.31f, 0.2f, 0), transform.rotation);
		//		Invoke("restart", 2);       // THIS CALLES RESTART() 2 SECONDS AFTER THE CODE REACES THIS LINE
		//	}

		}

	}

	// This will restart the game 
	void restart()
	{
		Application.LoadLevel(Application.loadedLevel);
	}

}

