//using UnityEngine;
//using System.Collections;

//public class MUFDustEffector : MonoBehaviour
// {
//	public GameObject effect;
//	MUFGame gm;
//	void Start () 
//	{
//		effect = Resources.Load("Prefabs/Common/dustEffect") as GameObject;
//		gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<MUFGame>();
//	}
	
//	void OnTriggerEnter(Collider col)
//	{
//		GameObject particle = Instantiate(effect,col.transform.position,new Quaternion(180,0,0,0)) as GameObject;
//		particle.transform.parent = gameObject.transform;
//		Destroy(gameObject.transform.parent.gameObject);
//		gameObject.transform.parent = col.transform;
//		gm.animating = false;
//		gm.colls[int.Parse(col.name)].enabled = false;
//		Destroy(this);
//	}
//}
