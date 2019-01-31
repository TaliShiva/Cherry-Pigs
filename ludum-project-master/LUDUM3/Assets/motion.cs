using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class motion : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Transform BackRightLeg = transform.Find("BackRightLeg");
        BackRightLeg.Rotate(Vector3.right);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
