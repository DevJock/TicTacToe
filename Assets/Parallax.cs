using UnityEngine;
using System.Collections;

public class Parallax : MonoBehaviour 
{
	public float speed;
	public bool simulateParallax;
	public Vector3 camPos;
	public Vector3 gyroData;
	public bool simulating;
	Gyroscope gyro;

	Vector3 maxPos;


	void Start ()
	{
		if (SystemInfo.supportsGyroscope) 
		{
			simulateParallax = false;
			gyro = Input.gyro;
			gyro.enabled = true;
			gyro.updateInterval = 0.1f;
			Debug.Log ("Gyro Enabled");
		}
		else
		{
			Debug.Log ("Simulating Gyro");
			simulateParallax = true;
			gyro = null;
		}
		int x = Screen.currentResolution.width;
		int y = Screen.currentResolution.height;
		Vector3 tVect = new Vector3 (x , y , transform.position.z);
		maxPos = Camera.main.ScreenToViewportPoint(tVect);
	}
		
	void Update () 
	{
		if(!simulateParallax)
		{
			gyroData = Input.gyro.userAcceleration;
		}
		else
		{
			if(!simulating)
			{
				int randX = Random.Range (-(int)maxPos.x, (int)maxPos.x);
				int randY = Random.Range (-(int)maxPos.y, (int)maxPos.y);
				gyroData = new Vector3 (randX,randY,maxPos.z);
				simulating = true;
			}
			else
			{
				transform.position = Vector3.LerpUnclamped(transform.position,gyroData,Time.deltaTime * speed);
				if(Vector3.Distance (transform.position,gyroData) < 0.3f)
				{
					simulating = false;
					transform.position = gyroData;
				}
			}
		}
	}
}
