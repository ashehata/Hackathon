using UnityEngine;
using System.Collections;

public class ForwardSync : MonoBehaviour {
	GameObject player;
	GameObject myCamera;
	GameObject MyoObject;
	GameObject MyoDataObject;
	public GameObject hitObject;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("OVRPlayerController");
		myCamera = GameObject.Find ("PlayerForward");
		MyoObject = GameObject.Find ("Hub - 1 Myo");
		MyoDataObject = GameObject.Find ("Joint");
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = MyoObject.transform.position = MyoDataObject.transform.position = player.transform.position;
		this.transform.eulerAngles = new Vector3 (
			myCamera.transform.eulerAngles.x, player.transform.eulerAngles.y, myCamera.transform.eulerAngles.z);
		RaycastHit myHit;
		Vector3 direction = new Vector3 (
			MyoDataObject.transform.forward.x, MyoDataObject.transform.forward.y, MyoDataObject.transform.forward.z);
		Ray myRay = new Ray (transform.position, direction);
		Debug.DrawRay (transform.position, direction);
		if (Physics.Raycast(myRay, out myHit, 100)){
			if (myHit.transform.tag == "JediObject"){
				Debug.Log ("Hit");
				hitObject = myHit.transform.gameObject;
			}
			else{
				Debug.Log ("No Hit");
			}
		}
	}

}
