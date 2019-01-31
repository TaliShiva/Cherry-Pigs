using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class HomelessOneScript : MonoBehaviour {

	// Use this for initialization
    public Transform goal;
       
    void Start () {
          UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
          agent.destination = goal.position; 
          agent.updateRotation = false;
          //agent.stoppingDistance = 30.0f;
    }

    void LateUpdate()
    {
        UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

		GameObject objectToChase = FindNearestPig();
	    if (objectToChase != null)
	    {
		    goal = objectToChase.transform;
		    agent.destination = goal.position;
		    agent.updateRotation = false;
	    }
	    else
	    {
		    goal = null;
		    return;
	    }

		if (agent.velocity.magnitude >= 0.1f)
	    {
		    transform.rotation = Quaternion.LookRotation(agent.velocity.normalized);
	    }

    }

	GameObject FindNearestPig()
	{
		GameObject newGoal = null;
		float minDistance = float.MaxValue;
		for (int i = 0; i < MouseController.pigs.Count; ++i)
		{
			float toPigDistance = Vector3.Distance(transform.position, MouseController.pigs[i].transform.position);
			if (minDistance > toPigDistance)
			{
				minDistance = toPigDistance;
				newGoal = MouseController.pigs[i];
			}
		}

		return newGoal;
	}
	
	// Update is called once per frame
	void Update () {
//		          UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
//          agent.destination = goal.position;

		Transform LeftFrontalLeg = transform.Find("LeftLeg");
		Transform LeftBackLeg = transform.Find("RigthLeg");
		LeftFrontalLeg.Rotate(Vector3.left, -40);
		LeftBackLeg.Rotate(Vector3.left, -40);
	}
}
