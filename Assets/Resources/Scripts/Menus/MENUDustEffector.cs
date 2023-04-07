using UnityEngine;
using System.Collections;

public class MENUDustEffector : MonoBehaviour
 {
	public GameObject effect;
	void Start () 
	{
		effect = Resources.Load("Prefabs/Common/dustEffect") as GameObject;
	}
	
	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "TOUCHABLE") 
		{
			GameObject particle = Instantiate (effect, col.transform.position, new Quaternion (180, 0, 0, 0)) as GameObject;
			particle.transform.parent = gameObject.transform;
			Destroy (gameObject.transform.parent.gameObject);
			gameObject.transform.parent = col.transform;
			Destroy (GetComponent<BoxCollider> ());
			Destroy (GetComponent<Rigidbody>());
			Destroy (this);
		}
	}
}
