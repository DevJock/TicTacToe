//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine.UI;
//using UnityEngine.SceneManagement;

//public class MUFGame
//{
//	public int xScore = 0;
//	public int oScore = 0;
//	public int catWins = 0;
//	public bool playerOneTurn;
//	public bool gameOver;
//	public bool animating;
//	public int moves;
//	public bool GameStarted;
//	public int[] data = new int[9];
//	public GameObject[] objs = new GameObject[9];
//	public BoxCollider[] colls = new BoxCollider[9];
//	public Vector3[] winPosses = new Vector3[3];
//	public int[] winPos = new int[3];
//	Text xText;
//	Text oText;
//	int playerWon = -2;


//	void Start ()
//	{
//		xText = GameObject.FindGameObjectWithTag("xScoreText").GetComponent<Text>();
//		oText = GameObject.FindGameObjectWithTag("oScoreText").GetComponent<Text>();
//		colls = GameObject.Find("Colliders").GetComponentsInChildren<BoxCollider>();
//		set();
//	}

//	public void endGame()
//	{	
//		//Networking.Disconnect(2624);
//		Debug.Log("Disconnected");
//		SceneManager.LoadScene("mainMenu");
//	}

//	public void reset()
//	{
//		GameObject wOBJS = GameObject.FindGameObjectWithTag("gameOverPanel");
//		for(int i=0;i<wOBJS.transform.childCount;i++)
//		{
//			wOBJS.transform.GetChild(i).gameObject.SetActive(false);
//		}
//		//RPC("resetANIM");
//	}

//	protected override void NetworkStart()
//	{
//		base.NetworkStart();
//		Debug.Log("Connected");
//		GameStarted = true;
//	}

//	[BRPC]
//	void resetANIM()
//	{
//		StopAllCoroutines();
//		StartCoroutine(RESTART());
//	}

//	void set()
//	{
//		if(!gameObject)
//			return;

//		for (int i = 0; i < objs.Length; i++)
//		{
//			data[i] = -1;
//			if(objs[i])
//			{
//				Destroy(objs[i]);
//			}
//		}
			
//		for(int i=0;i<colls.Length;i++)
//		{
//			colls[i].enabled = true;
//		}
//		GameObject wOBJS = GameObject.FindGameObjectWithTag("gameOverPanel");
//		for(int i=0;i<wOBJS.transform.childCount;i++)
//		{
//			wOBJS.transform.GetChild(i).gameObject.SetActive(false);
//		}
//		moves = 0;
//		gameOver = false;
//		animating = false;
//	}

//	void Update()
//	{
//		xText.text = "X: " + xScore;
//		oText.text = "O: " + oScore;
//		if(GameStarted)
//		{
//			RPC("validate");
		
//			if(!animating)
//			{
//				if(playerWon == -1 && moves >= 9)
//				{
//					GameStarted = false;
//					gameOver = true;
//					StartCoroutine(paintRED());
//					RPC("endGame",3);
//				}
//				else if(playerWon == 0)
//				{
//					gameOver = true;
//					GameStarted = false;
//					generateWinPoses();
//					StartCoroutine(paintGREEN());
//					RPC("endGame",0);
//					playerOneTurn = true;
//				}
//				else if(playerWon == 1)
//				{
//					gameOver = true;
//					GameStarted = false;
//					generateWinPoses();
//					StartCoroutine(paintGREEN());
//					RPC("endGame",1);
//					playerOneTurn = false;
//				}

//				if (playerOneTurn != OwningNetWorker.IsServer)
//				return;
//				#if UNITY_IOS || UNITY_ANDROID
//				if(Input.touchCount >0)
//				{
//					Touch touch = Input.GetTouch(0);
//					if(touch.phase == TouchPhase.Began)
//					{
//						Ray screenRay = Camera.main.ScreenPointToRay(touch.position);
//		                RaycastHit hit;
//						if (Physics.Raycast(screenRay, out hit))
//		              	 {
//							if(hit.collider.tag == "TOUCHABLE")
//							{
//								RPC("PlacePiece",int.Parse(hit.collider.name),hit.transform.position);
//							}
//						}
//					}
//				}
//				#else
//				if(Input.GetMouseButtonDown(0))
//				{
//					Vector3 pos = Input.mousePosition;
//					Ray screenRay = Camera.main.ScreenPointToRay(pos);
//			        RaycastHit hit;
//			        if (Physics.Raycast(screenRay, out hit))
//			        {
//						if(hit.collider.tag == "TOUCHABLE")
//						{
//							RPC("PlacePiece",int.Parse(hit.collider.name),hit.transform.position);
//						}
//					}
//				}
//				#endif
//			}
//		}
//	}

//	[BRPC]
//	private void PlacePiece(int pos,Vector3 POS)
//	{
//		if (gameOver)
//			return;
		
//		if (playerOneTurn)
//		{
//			GameObject myOBJ = Resources.Load("Prefabs/MultiPlayer/MUFX") as GameObject;
//			string pName = "X";
//			GameObject placer = Instantiate(Resources.Load("Prefabs/Common/Placer") as GameObject);
//			GameObject thisObject = (GameObject)Instantiate(myOBJ);
//			Vector3 position = POS;
//			thisObject.transform.parent = placer.transform;
//			thisObject.transform.localPosition = new Vector3(position.x/5,position.y/5,0);
//			thisObject.name = pName+"("+position+")";
//			objs[pos]=thisObject;
//			data[pos] = 0;
//			animating = true;
//		}
//		else
//		{
//			GameObject myOBJ = Resources.Load("Prefabs/MultiPlayer/MUFO") as GameObject;
//			string pName = "O";
//			GameObject placer = Instantiate(Resources.Load("Prefabs/Common/Placer") as GameObject);
//			GameObject thisObject = (GameObject)Instantiate(myOBJ);
//			Vector3 position = POS;
//			thisObject.transform.parent = placer.transform;
//			thisObject.transform.localPosition = new Vector3(position.x/5,position.y/5,0);
//			thisObject.name = pName+"("+position+")";
//			objs[pos]=thisObject;
//			data[pos] = 1;
//			animating = true;
//		}
//		playerOneTurn = !playerOneTurn;
//		moves++;
//	}

//	[BRPC]
//	void endGame(int opt)
//	{
//		if(opt == 0)
//		{
//			xScore++;
//			Debug.Log ("GameOver X Wins");
//			GameObject wOBJS = GameObject.FindGameObjectWithTag("gameOverPanel");
//			for(int i=0;i<wOBJS.transform.childCount;i++)
//			{
//				wOBJS.transform.GetChild(i).gameObject.SetActive(true);
//			}
//			GameObject.FindGameObjectWithTag("winInfo").GetComponent<Text>().text = ".. X Wins .." ;
//			GameObject.FindGameObjectWithTag("winInfo").GetComponent<Text>().color = Color.green;
//		}
//		else if(opt == 1)
//		{
//			oScore++;
//			Debug.Log ("GameOver O Wins");
//			GameObject wOBJS = GameObject.FindGameObjectWithTag("gameOverPanel");
//			for(int i=0;i<wOBJS.transform.childCount;i++)
//			{
//				wOBJS.transform.GetChild(i).gameObject.SetActive(true);
//			}
//			GameObject.FindGameObjectWithTag("winInfo").GetComponent<Text>().text = ".. O Wins .." ;
//			GameObject.FindGameObjectWithTag("winInfo").GetComponent<Text>().color = Color.green;
//		}
//		else if(opt == 3)
//		{
//			GameObject wOBJS = GameObject.FindGameObjectWithTag("gameOverPanel");
//			for(int i=0;i<wOBJS.transform.childCount;i++)
//			{
//				wOBJS.transform.GetChild(i).gameObject.SetActive(true);
//			}
//			GameObject.FindGameObjectWithTag("winInfo").GetComponent<Text>().text = "!! Tie Game !!" ;
//			GameObject.FindGameObjectWithTag("winInfo").GetComponent<Text>().color = Color.red;
//		}

//	}


//	[BRPC]
//	void validate()
//	{
//		int win = -1;
//		if (data [0] != -1 && ((data [0] == data [1]) && (data [1] == data [2]) && (data [0] == data [2]))) 
//		{
//			win = data [0];
//			winPos[0] = 0;
//			winPos[1] = 1;
//			winPos[2] = 2;
//		}
//		else if (data[3] != -1 &&((data[3] == data[4]) && (data[4] == data[5]) && (data[3] == data[5])))
//		{
//			win = data [3];
//			winPos[0] = 3;
//			winPos[1] = 4;
//			winPos[2] = 5;
//		}
//		else if (data[6] != -1 &&((data[6] == data[7]) && (data[7] == data[8]) && (data[6] == data[8])))
//		{
//			win = data [6];
//			winPos[0] = 6;
//			winPos[1] = 7;
//			winPos[2] = 8;
//		}
//		else if (data[0] != -1 &&((data[0] == data[4]) && (data[4] == data[8]) && (data[0] == data[8])))
//		{
//			win = data [0];
//			winPos[0] = 0;
//			winPos[1] = 4;
//			winPos[2] = 8;
//		}
//		else if (data[2] != -1 &&((data[2] == data[4]) && (data[4] == data[6]) && (data[2] == data[6])))
//		{
//			win = data [2];
//			winPos[0] = 2;
//			winPos[1] = 4;
//			winPos[2] = 6;
//		}
//		else if (data[0] != -1 &&((data[0] == data[3]) && (data[3] == data[6]) && (data[0] == data[6])))
//		{
//			win = data [0];
//			winPos[0] = 0;
//			winPos[1] = 3;
//			winPos[2] = 6;
//		}
//		else if (data[1] != -1 &&((data[1] == data[4]) && (data[4] == data[7]) && (data[1] == data[7])))
//		{
//			win = data [1];
//			winPos[0] = 1;
//			winPos[1] = 4;
//			winPos[2] = 7;
//		}
//		else if (data[2] != -1 &&((data[2] == data[5]) && (data[5] == data[8]) && (data[2] == data[8])))
//		{
//			win = data [2];
//			winPos[0] = 2;
//			winPos[1] = 5;
//			winPos[2] = 8;
//		}
//   		playerWon = win ;
//   	}

//   	void generateWinPoses()
//   	{
//		int first = Random.Range(1,3);
//		int last = Random.Range(0,2);
//		winPosses[0] = objs[winPos[first]].transform.position;
//		if(last == first && first == 1)
//		{
//			winPosses[1] = objs[winPos[2]].transform.position;
//			winPosses[2] = objs[winPos[0]].transform.position;
//		}
//		else
//		{
//			winPosses[1] = objs[winPos[0]].transform.position;
//			winPosses[2] = objs[winPos[1]].transform.position;
//		}
//   	}

//	IEnumerator paintRED()
//	{
//		Vector3[] poss = new Vector3[9];
//		float speed = 10.0F;
//    	for(int i=0;i<9;i++)
//    	{
//			poss[i] = objs[i].transform.position;
//    	}

//		for(int i=0;i<9;i++)
//		{
//    		float journeyLength;
//			Vector3 pos = objs[i].transform.position;
//			Vector3 newPos = pos;
//			newPos.z = -10;
//			float startTime = Time.time;
//        	journeyLength = Vector3.Distance(pos, newPos);
//			do
//			{
//				if(moves == 0)
//					 yield break;
//				float distCovered = (Time.time - startTime) * speed;
//       			float fracJourney = distCovered / journeyLength;
//				objs[i].transform.position = Vector3.Lerp(objs[i].transform.position,newPos, fracJourney);
//				yield return new WaitForSeconds(0);
//			}while(Vector3.Distance(objs[i].transform.position,newPos) > 0.5f);
//			objs[i].transform.position = newPos;
//			objs[i].GetComponentInChildren<Renderer>().material.color = Color.red;
//			newPos = poss[i];
//			do
//			{
//				if(moves == 0)
//					yield break;
//				float distCovered = (Time.time - startTime) * speed;
//       			float fracJourney = distCovered / journeyLength;
//				objs[i].transform.position = Vector3.Lerp(objs[i].transform.position,newPos, fracJourney);
//				yield return new WaitForSeconds(0);
//			}while(Vector3.Distance(objs[i].transform.position,newPos) > 0.5f);
//			objs[i].transform.position = newPos;
//		}
//	}

//	IEnumerator paintGREEN()
//	{
//		float speed = 5.0F;
//		for(int i=0;i<3;i++)
//		{
//    		float journeyLength;
//			Vector3 pos = objs[winPos[i]].transform.position;
//			Vector3 newPos = pos;
//			newPos.z = -10;
//			float startTime = Time.time;
//        	journeyLength = Vector3.Distance(pos, newPos);
//			do
//			{
//				if(moves == 0)
//					yield break;
//				float distCovered = (Time.time - startTime) * speed;
//       			float fracJourney = distCovered / journeyLength;
//				objs[winPos[i]].transform.position = Vector3.Lerp(objs[winPos[i]].transform.position,newPos, fracJourney);
//				yield return new WaitForSeconds(0);
//			}while(Vector3.Distance(objs[winPos[i]].transform.position,newPos) > 0.5f);
//			objs[winPos[i]].transform.position = newPos;
//			objs[winPos[i]].GetComponentInChildren<Renderer>().material.color = Color.green;
//		}

//		for(int i=0;i<3;i++)
//		{
//    		float journeyLength;
//			Vector3 pos = objs[winPos[i]].transform.position;
//			Vector3 newPos = winPosses[i];
//			newPos.z = -10;
//			float startTime = Time.time;
//        	journeyLength = Vector3.Distance(pos, newPos);
//			do
//			{
//				if(moves == 0)
//					yield break;
//				float distCovered = (Time.time - startTime) * speed;
//       			float fracJourney = distCovered / journeyLength;
//				objs[winPos[i]].transform.position = Vector3.Lerp(objs[winPos[i]].transform.position,newPos, fracJourney);
//				yield return new WaitForSeconds(0);
//			}while(Vector3.Distance(objs[winPos[i]].transform.position,newPos) > 0.5f);
//			objs[winPos[i]].transform.position = newPos;
//		}

//		for(int i=0;i<3;i++)
//		{
//    		float journeyLength;
//			Vector3 pos = objs[winPos[i]].transform.position;
//			Vector3 newPos = pos;
//			newPos.z = 0;
//			float startTime = Time.time;
//        	journeyLength = Vector3.Distance(pos, newPos);
//			do
//			{
//				if(moves == 0)
//					yield break;
//				float distCovered = (Time.time - startTime) * speed;
//       			float fracJourney = distCovered / journeyLength;
//				objs[winPos[i]].transform.position = Vector3.Lerp(objs[winPos[i]].transform.position,newPos, fracJourney);
//				yield return new WaitForSeconds(0);
//			}while(Vector3.Distance(objs[winPos[i]].transform.position,newPos) > 0.5f);
//			objs[winPos[i]].transform.position = newPos;
//		}
//	}

//	IEnumerator RESTART()
//	{
//		float speed = 20.0f;
//		for(int i=0;i<9;i++)
//		{
//			if(objs[i] == null)
//			{
//				continue;
//			}
//			else
//			{
//	    		float journeyLength;
//				Vector3 pos = objs[i].transform.position;
//				Vector3 newPos = new Vector3(0,-4.3f,0);
//				float startTime = Time.time;
//	        	journeyLength = Vector3.Distance(pos, newPos);
//				do
//				{
//					if(moves == 0)
//						yield break;
//					float distCovered = (Time.time - startTime) * speed;
//	       			float fracJourney = distCovered / journeyLength;
//					objs[i].transform.position = Vector3.Lerp(objs[i].transform.position,newPos, fracJourney);
//					yield return new WaitForSeconds(0);
//				}while(Vector3.Distance(objs[i].transform.position,newPos) > 0.5f);
//				objs[i].transform.position = newPos;
//			}
//		}
//		set ();
//	}
//}