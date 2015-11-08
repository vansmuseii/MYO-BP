using UnityEngine;
using System.Collections;
using LockingPolicy = Thalmic.Myo.LockingPolicy;
using Pose = Thalmic.Myo.Pose;
using UnlockType = Thalmic.Myo.UnlockType;
using VibrationType = Thalmic.Myo.VibrationType;

public class ShootWithRender : MonoBehaviour {
	float xPos =0;
	float yPos = 0;
	float zPos = 0;
	public GameObject myo = null;
	
	// A reference angle representing how the armband is rotated about the wearer's arm, i.e. roll.
	// Set by making the fingers spread pose or pressing "r".
	private float _referenceRoll = 0.0f;
	
	// The pose from the last update. This is used to determine if the pose has changed
	// so that actions are only performed upon making them rather than every frame during
	// which they are active.
	private Pose _lastPose = Pose.Unknown;
	
	public GameObject Sphere; //reference to the sphere
	private Vector3 throwSpeed = new Vector3(0, 26, 40); //This value is a sure basket, we'll modify this using the forcemeter
	public Vector3 spherePos; //starting ball position
	private bool thrown = false; //if ball has been thrown, prevents 2 or more balls
	private GameObject sphereClone; //we don't use the original prefab
	public Renderer rende;
	public Renderer rendeMyo;
	public Renderer camM;
	public Renderer camF;
	public GameObject myoSphere;
	private bool temp = false;

	private float arrowSpeed = 0.3f; //Difficulty, higher value = faster arrow movement
	private bool right = true; //used to revers arrow movement
	private bool isThrown = false;
	public GameObject camMain;
	public GameObject camFollow;
	public GameObject myPlane;
	
	private int availableShots = 2;
	
	
	
	
	// Use this for initialization
	void Start () {
		/* Increase Gravity */
	//	Physics.gravity = new Vector3(0, -100, 0);
		
		
	}
	
	// Update is called once per physics thing
	void FixedUpdate () {

		
		// Access the ThalmicMyo component attached to the Myo object.
		ThalmicMyo thalmicMyo = myo.GetComponent<ThalmicMyo> ();
		
		// Update references when the pose becomes fingers spread or the q key is pressed.
		bool updateReference = false;
		if (thalmicMyo.pose != _lastPose) {
			_lastPose = thalmicMyo.pose;
			
			if (thalmicMyo.pose == Pose.DoubleTap) {
				updateReference = true;
				
				ExtendUnlockAndNotifyUserAction(thalmicMyo);
			}
			if (thalmicMyo.pose == Pose.Fist && !isThrown){
						
				xPos =myoSphere.transform.position.x;
				yPos =myoSphere.transform.position.y;
				zPos =myoSphere.transform.position.z;
				Vector3 tempVect = new Vector3(xPos,yPos,zPos);

				rendeMyo = GetComponent<Renderer>();

				rendeMyo.enabled= false;
				rende.enabled=true;

				Sphere.transform.position = tempVect;

				Rigidbody rigidBody = this.GetComponent<Rigidbody> ();
				rigidBody.AddForce (new Vector3 (0, 700, 500));
				isThrown = true;


				camMain.GetComponent<Camera>().enabled = false;
				camFollow.GetComponent<Camera>().enabled = true;




				
			}
		}

		if(Sphere.transform.position.y < -2f){
			isThrown = false;
			camMain.GetComponent<Camera> ().enabled = true;
			camFollow.GetComponent<Camera> ().enabled = false;
			rende.enabled = false;
		}


	}



	void OnTriggerEnter(Collider other) {
		if (other.name == myPlane.name) {
			isThrown = false;
			camMain.GetComponent<Camera> ().enabled = true;
			camFollow.GetComponent<Camera> ().enabled = false;
			rende.enabled = false;
		} else {
			Destroy (other.gameObject);
			isThrown = false;
			camMain.GetComponent<Camera> ().enabled = true;
			camFollow.GetComponent<Camera> ().enabled = false;
			rende.enabled = false;
		}


	}


	
	// This will restart the game 
	void restart()
	{
		Application.LoadLevel(Application.loadedLevel);
	}

	void ExtendUnlockAndNotifyUserAction (ThalmicMyo myo)
	{
		ThalmicHub hub = ThalmicHub.instance;
		
		if (hub.lockingPolicy == LockingPolicy.Standard) {
			myo.Unlock (UnlockType.Timed);
		}
		
		myo.NotifyUserAction ();
	}
	
}

