using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class Destroyer : MonoBehaviour 
{
	public GameObject p;

	void Start()
	{
		if(gameObject.name.Contains("X"))
			p = Resources.Load("Particles/xDestroy") as GameObject;
		else
			p = Resources.Load("Particles/oDestroy") as GameObject;
	}

	void OnDestroy()
	{
		Vector3 pos = transform.parent.parent.parent.position;
		GameObject o = Instantiate(p,pos,Quaternion.identity) as GameObject;
		Destroy(o,2.5f);
	}


}
