using UnityEngine;
using System.Collections;
using Thalmic;
using Pose = Thalmic.Myo.Pose;

public class ObjectSelect : MonoBehaviour {
	GameObject MyoController;
	ForwardSync PlayerController;
	bool isObjectPicked = false;
	GameObject hitObject;
	GameObject Player;
	Vector3 lastRotation;
	bool objectFisted = false;
	// Use this for initialization
	void Start () {
		MyoController = GameObject.Find ("Joint");
		PlayerController = GetComponent ("ForwardSync") as ForwardSync;
		Player = GameObject.Find ("OVRPlayerController");
	}
	
	// Update is called once per frame
	void Update () {
		ThalmicMyo thalmicMyo = GameObject.Find ("Myo").GetComponent<ThalmicMyo> ();
		//Debug.Log (MyoController.transform.eulerAngles.y);
		//if fisted
		if ((thalmicMyo.pose == Pose.Fist) && !isObjectPicked) { 
			//thalmicMyo.NotifyUserAction();
			hitObject = PlayerController.hitObject;
			if (hitObject != null) {
				//hitObject.transform.parent = GameObject.Find ("PlayerForward").transform;
				hitObject.GetComponent<Rigidbody>().isKinematic = true;
				isObjectPicked = true;
				//hitObject.transform.position = new Vector3(
				//	0, hitObject.transform.localPosition.y, hitObject.transform.localPosition.z);
				lastRotation = MyoController.transform.eulerAngles;
			}
			//hitObject.transform.parent = null;
		}

		else if ((thalmicMyo.pose == Thalmic.Myo.Pose.Rest || thalmicMyo.pose == Thalmic.Myo.Pose.Unknown || thalmicMyo.pose == Thalmic.Myo.Pose.FingersSpread)
		         && isObjectPicked) {
			isObjectPicked = false;
			hitObject.GetComponent<Rigidbody>().isKinematic = false;
			hitObject.GetComponent<Rigidbody>().AddForce(MyoController.transform.forward * 10);
			hitObject = null;
		}
		Debug.Log (thalmicMyo.pose.ToString());

		if (thalmicMyo.pose == Thalmic.Myo.Pose.DoubleTap) {
			MyoController.transform.rotation = PlayerController.transform.rotation;
			GameObject.Find ("Hub - 1 Myo").transform.rotation = PlayerController.transform.rotation;

		}
		if (isObjectPicked && hitObject) {
			//hitObject.transform.eulerAngles = MyoController.transform.eulerAngles;
			float xDegreesDiff =  MyoController.transform.eulerAngles.x - lastRotation.x;
			float yDegreesDiff = MyoController.transform.eulerAngles.y - lastRotation.y;
			float zDegreesDiff =  MyoController.transform.eulerAngles.z - lastRotation.z;
			Debug.Log (yDegreesDiff);
			//Vector3 target = Vector3.Lerp (hitObject.transform.eulerAngles, MyoController.transform.eulerAngles, 2 * Time.deltaTime);
			//hitObject.transform.localPosition = new Vector3(
			//	0, hitObject.transform.localPosition.y, hitObject.transform.localPosition.z);
			//hitObject.transform.localRotation = new Vector3
			hitObject.transform.RotateAround (Player.transform.position, Player.transform.right, xDegreesDiff);
			hitObject.transform.RotateAround (Player.transform.position, Player.transform.up, yDegreesDiff);
			hitObject.transform.RotateAround (Player.transform.position, Player.transform.forward, zDegreesDiff);

			//hitObject.transform.localEulerAngles = MyoController.transform.eulerAngles;
			lastRotation = MyoController.transform.eulerAngles;
		}


	}
}
