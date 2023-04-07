using UnityEngine;
using System.Collections;

public class SPPlayer : MonoBehaviour 
{
	public int playerID;
	public Vector3 position;
	public GameObject obj;
	string pName;
	public bool canPlay;
	SPGameManager gm;
	GameObject placer;

	public void Init () 
	{
		if(playerID == 0)
		{
			obj  = Resources.Load("Prefabs/SinglePlayer/SPX") as GameObject;
			pName = "X";
		}
		else
		{
			obj  = Resources.Load("Prefabs/SinglePlayer/SPO") as GameObject;
			pName = "O";
		}
		if(gm == null)
		{
			gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<SPGameManager>();
		}
	}

	
	// Update is called once per frame
	void Update ()
	{
		if(canPlay)
		{
		#if UNITY_IOS || UNITY_ANDROID
			if(Input.touchCount >0)
			{
				Touch touch = Input.GetTouch(0);
				if(touch.phase == TouchPhase.Began)
				{
					Ray screenRay = Camera.main.ScreenPointToRay(touch.position);
	                RaycastHit hit;
					if (Physics.Raycast(screenRay, out hit))
	              	 {
						if(hit.collider.tag == "TOUCHABLE")
						{
						placer = Instantiate(Resources.Load("Prefabs/Common/Placer") as GameObject);
						GameObject thisObject = (GameObject)Instantiate(obj);
						position = hit.collider.transform.position;
						thisObject.transform.parent = placer.transform;
						thisObject.transform.localPosition = new Vector3(position.x/5,position.y/5,0);
						thisObject.name = pName+"("+position+")";
						gm.data[int.Parse(hit.collider.name)] = playerID;
						gm.animating = true;
						canPlay = false;
						gm.turn = 1;
						gm.moves++;
						}
					}
				}
			}
			#else
			if(Input.GetMouseButtonDown(0))
			{
				Vector3 pos = Input.mousePosition;
				Ray screenRay = Camera.main.ScreenPointToRay(pos);
	            RaycastHit hit;
	            if (Physics.Raycast(screenRay, out hit))
	               {
					if(hit.collider.tag == "TOUCHABLE")
					{
						placer = Instantiate(Resources.Load("Prefabs/Common/Placer") as GameObject);
						GameObject thisObject = (GameObject)Instantiate(obj);
						position = hit.collider.transform.position;
						thisObject.transform.parent = placer.transform;
						thisObject.transform.localPosition = new Vector3(position.x/5,position.y/5,0);
						thisObject.name = pName+"("+position+")";
						gm.data[int.Parse(hit.collider.name)] = playerID;
						gm.animating = true;
						canPlay = false;
						gm.turn = 1;
						gm.moves++;
					}
				}
			}
			#endif
		}
	}
}
