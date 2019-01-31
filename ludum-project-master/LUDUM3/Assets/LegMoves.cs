using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegMoves : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        Transform LeftFrontalLeg = transform.Find("LeftFrontalLeg");
        Transform LeftBackLeg = transform.Find("LeftBackLeg");
        Transform RightFrontalLeg = transform.Find("RightFrontalLeg");
        Transform RightBackLeg = transform.Find("RightBackLeg");
        Transform Nose = transform.Find("Nose");
        //LeftFrontalLeg.Rotate(Vector3.forward, 30);
        LeftFrontalLeg.Rotate(Vector3.forward, -40);
        LeftBackLeg.Rotate(Vector3.forward, -40);
        RightFrontalLeg.Rotate(Vector3.forward, -40);
        RightBackLeg.Rotate(Vector3.forward, -40);
        Nose.Rotate(-8, 100, 8);
    }
}
