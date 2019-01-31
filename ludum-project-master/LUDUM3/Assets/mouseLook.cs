using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

//public class mouseLook : MonoBehaviour {
//
//	// Use this for initialization
//	void Start () {
//		
//	}
//	
//	// Update is called once per frame
//	void Update () {
//		
//	}
//}
//
//[AddComponentMenu("Camera-Control/Smooth Mouse Look")]
//public class mouseLook : MonoBehaviour {
// 
//	public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
//	public RotationAxes axes = RotationAxes.MouseXAndY;
//	public float sensitivityX = 15F;
//	public float sensitivityY = 15F;
// 
//	public float minimumX = -360F;
//	public float maximumX = 360F;
// 
//	public float minimumY = -60F;
//	public float maximumY = 60F;
// 
//	float rotationX = 0F;
//	float rotationY = 0F;
// 
//	private List<float> rotArrayX = new List<float>();
//	float rotAverageX = 0F;	
// 
//	private List<float> rotArrayY = new List<float>();
//	float rotAverageY = 0F;
// 
//	public float frameCounter = 20;
// 
//	Quaternion originalRotation;
// 
//	void Update ()
//	{
//		if (axes == RotationAxes.MouseXAndY)
//		{			
//			rotAverageY = 0f;
//			rotAverageX = 0f;
// 
//			rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
//			rotationX += Input.GetAxis("Mouse X") * sensitivityX;
// 
//			rotArrayY.Add(rotationY);
//			rotArrayX.Add(rotationX);
// 
//			if (rotArrayY.Count >= frameCounter) {
//				rotArrayY.RemoveAt(0);
//			}
//			if (rotArrayX.Count >= frameCounter) {
//				rotArrayX.RemoveAt(0);
//			}
// 
//			for(int j = 0; j < rotArrayY.Count; j++) {
//				rotAverageY += rotArrayY[j];
//			}
//			for(int i = 0; i < rotArrayX.Count; i++) {
//				rotAverageX += rotArrayX[i];
//			}
// 
//			rotAverageY /= rotArrayY.Count;
//			rotAverageX /= rotArrayX.Count;
// 
//			rotAverageY = ClampAngle (rotAverageY, minimumY, maximumY);
//			rotAverageX = ClampAngle (rotAverageX, minimumX, maximumX);
// 
//			Quaternion yQuaternion = Quaternion.AngleAxis (rotAverageY, Vector3.left);
//			Quaternion xQuaternion = Quaternion.AngleAxis (rotAverageX, Vector3.up);
// 
//			transform.localRotation = originalRotation * xQuaternion * yQuaternion;
//		}
//		else if (axes == RotationAxes.MouseX)
//		{			
//			rotAverageX = 0f;
// 
//			rotationX += Input.GetAxis("Mouse X") * sensitivityX;
// 
//			rotArrayX.Add(rotationX);
// 
//			if (rotArrayX.Count >= frameCounter) {
//				rotArrayX.RemoveAt(0);
//			}
//			for(int i = 0; i < rotArrayX.Count; i++) {
//				rotAverageX += rotArrayX[i];
//			}
//			rotAverageX /= rotArrayX.Count;
// 
//			rotAverageX = ClampAngle (rotAverageX, minimumX, maximumX);
// 
//			Quaternion xQuaternion = Quaternion.AngleAxis (rotAverageX, Vector3.up);
//			transform.localRotation = originalRotation * xQuaternion;			
//		}
//		else
//		{			
//			rotAverageY = 0f;
// 
//			rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
// 
//			rotArrayY.Add(rotationY);
// 
//			if (rotArrayY.Count >= frameCounter) {
//				rotArrayY.RemoveAt(0);
//			}
//			for(int j = 0; j < rotArrayY.Count; j++) {
//				rotAverageY += rotArrayY[j];
//			}
//			rotAverageY /= rotArrayY.Count;
// 
//			rotAverageY = ClampAngle (rotAverageY, minimumY, maximumY);
// 
//			Quaternion yQuaternion = Quaternion.AngleAxis (rotAverageY, Vector3.left);
//			transform.localRotation = originalRotation * yQuaternion;
//		}
//	}
// 
//	void Start ()
//	{		
//                Rigidbody rb = GetComponent<Rigidbody>();	
//		if (rb)
//			rb.freezeRotation = true;
//		originalRotation = transform.localRotation;
//	}
// 
//	public static float ClampAngle (float angle, float min, float max)
//	{
//		angle = angle % 360;
//		if ((angle >= -360F) && (angle <= 360F)) {
//			if (angle < -360F) {
//				angle += 360F;
//			}
//			if (angle > 360F) {
//				angle -= 360F;
//			}			
//		}
//		return Mathf.Clamp (angle, min, max);
//	}
//}



public class mouseLook : MonoBehaviour
{

    public bool isExecutionInProgress = false;
    private Transform previousCamrea;

	private Quaternion PreviousCamRotation;
	private Vector3 PreviousCamPosition;

	public Vector3 CameraOffset;
    public Transform LookTarget;
    
    public  int ScrollWidth { get { return 50; } }
    public  float ScrollSpeed { get { return 75; } }
    public  float RotateAmount { get { return 10; } }
    public  float RotateSpeed { get { return 100; } }
    public  float MinCameraHeight { get { return 10; } }
    public  float MaxCameraHeight { get { return 500; } }

	public GameObject censura;

	void Awake()
	{
		censura.SetActive(false);
	}
    // Use this for initialization
    void Start () {
    }
 
    // Update is called once per frame
    void Update () {
        if (!isExecutionInProgress)
        {
            MoveCamera();
            RotateCamera();
        }
    }

    public void StartExecution()
    {
	    if (isExecutionInProgress)
	    {
		    return;
	    }

	    PreviousCamRotation = transform.rotation;
	    PreviousCamPosition = transform.position;

		censura.SetActive(true);


	isExecutionInProgress = true;
        transform.position = LookTarget.position + CameraOffset;
        transform.LookAt(LookTarget.position); 
    }

    public void EndExecution()
    {
	    if (MouseController.pigs.Count == 0) {
		    return;
	    }
		censura.SetActive(false);
		transform.rotation = PreviousCamRotation;
        transform.position = PreviousCamPosition;
		isExecutionInProgress = false;

    }
 
    private void MoveCamera() {
        float xpos = Input.mousePosition.x;
        float ypos = Input.mousePosition.y;
        Vector3 movement = new Vector3(0,0,0);
 
        //horizontal camera movement
        if(xpos >= 0 && xpos < ScrollWidth) {
            movement.x -= ScrollSpeed;
        } else if(xpos <= Screen.width && xpos > Screen.width - ScrollWidth) {
            movement.x += ScrollSpeed;
        }
 
        //vertical camera movement
        if(ypos >= 0 && ypos < ScrollWidth) {
            movement.z -= ScrollSpeed;
        } else if(ypos <= Screen.height && ypos > Screen.height - ScrollWidth) {
            movement.z += ScrollSpeed;
        }
//        Debug.Log("Hello", gameObject);
        //make sure movement is in the direction the camera is pointing
        //but ignore the vertical tilt of the camera to get sensible scrolling
        movement = Camera.main.transform.TransformDirection(movement);
        movement.y = 0;
 
        //away from ground movement
        movement.y -= ScrollSpeed * Input.GetAxis("Mouse ScrollWheel");
 
        //calculate desired camera position based on received input
        Vector3 origin = Camera.main.transform.position;
        Vector3 destination = origin;
        destination.x += movement.x;
        destination.y += movement.y;
        destination.z += movement.z;
 
        //limit away from ground movement to be between a minimum and maximum distance
        if(destination.y > MaxCameraHeight) {
            destination.y = MaxCameraHeight;
        } else if(destination.y < MinCameraHeight) {
            destination.y = MinCameraHeight;
        }
 
        //if a change in position is detected perform the necessary update
        if(destination != origin) {
            Camera.main.transform.position = Vector3.MoveTowards(origin, destination, Time.deltaTime * ScrollSpeed);
        }
    }
 
    private void RotateCamera() {
        Vector3 origin = Camera.main.transform.eulerAngles;
        Vector3 destination = origin;
 
        //detect rotation amount if ALT is being held and the Right mouse button is down
        if((Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)) && Input.GetMouseButton(1)) {
            destination.x -= Input.GetAxis("Mouse Y") * RotateAmount;
            destination.y += Input.GetAxis("Mouse X") * RotateAmount;
        }
 
        //if a change in position is detected perform the necessary update
        if(destination != origin) {
            Camera.main.transform.eulerAngles = Vector3.MoveTowards(origin, destination, Time.deltaTime * RotateSpeed);
        }
    }
}