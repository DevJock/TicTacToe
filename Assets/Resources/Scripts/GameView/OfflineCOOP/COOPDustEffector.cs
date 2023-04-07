using UnityEngine;
using System.Collections;

public class COOPDustEffector : MonoBehaviour
 {
	COOPGameManager gm;
	COOPPlayer p;
	public GameObject effect;
	void Start () 
	{
		gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<COOPGameManager>();
		effect = Resources.Load("Prefabs/Common/dustEffect") as GameObject;
		p = GameObject.FindGameObjectWithTag("Player").GetComponent<COOPPlayer>();
	}
	
	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.tag == "TOUCHABLE")
		{
			GameObject particle = Instantiate (effect, col.transform.position, new Quaternion (180, 0, 0, 0)) as GameObject;
			particle.transform.parent = gameObject.transform;
			Destroy (gameObject.transform.parent.gameObject);
			gameObject.transform.parent = col.transform;
			Destroy (GetComponent<BoxCollider> ());
			Destroy (GetComponent<Rigidbody>());
			gm.colls[int.Parse(col.name)].enabled = false;
			gm.objs[int.Parse(col.name)] = gameObject;
			gm.animating = false;
			p.canPlay = true;
			Destroy(this);
		}

	}
}
