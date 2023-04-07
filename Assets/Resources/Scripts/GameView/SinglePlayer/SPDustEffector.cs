using UnityEngine;
using System.Collections;

public class SPDustEffector : MonoBehaviour
 {
 	SPGameManager gm;
	public GameObject effect;
	void Start () 
	{
		gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<SPGameManager>();
		effect = Resources.Load("Prefabs/Common/dustEffect") as GameObject;
	}
	
	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.tag == "TOUCHABLE")
		{
			GameObject particle = Instantiate (effect, col.transform.position, new Quaternion (180, 0, 0, 0)) as GameObject;
			particle.transform.parent = gameObject.transform;
			gm.colls[int.Parse(col.name)].enabled = false;
			gm.objs[int.Parse(col.name)] = gameObject;
			gm.animating = false;
			Destroy (gameObject.transform.parent.gameObject);
			gameObject.transform.parent = col.transform;
			Destroy (GetComponent<BoxCollider> ());
			Destroy (GetComponent<Rigidbody>());
			Destroy(this);	
		}
	}
}
