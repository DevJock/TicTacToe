using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SPGameManager : MonoBehaviour
 {
	public bool GameStarted;
	public int GameDifficulty;
	public bool initialized;
	public int xScore;
	public int oScore;
	public int moves;
	public int turn;
	SPPlayer player;
	SPAI ai;
	public int[] data = new int[9];
	public GameObject[] objs = new GameObject[9];
	public BoxCollider[] colls = new BoxCollider[9];
	Text xText;
	Text oText;
	int[] winPos = new int[3];
	public bool animating;

	bool stop;

	public int meID;

	void initialize()
	{
		for (int i = 0; i < 9; i++)
		{
			data[i] = -1;
		}
		xText = GameObject.FindGameObjectWithTag("xScoreText").GetComponent<Text>();
		oText = GameObject.FindGameObjectWithTag("oScoreText").GetComponent<Text>();
		colls = GameObject.Find("Colliders").GetComponentsInChildren<BoxCollider>();
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<SPPlayer>();
		ai = GetComponent<SPAI>();
		player.playerID = 0; // default starts with X;
		meID = player.playerID;
		GameStarted = true;
		initialized = true;
		player.canPlay = true;
		player.Init();
		xScore = 0;
		oScore = 0;
		turn = 0; // 0: me  1: other
		moves = 0;
		stop = false;
	}

	public void reset ()
	{
		StopAllCoroutines();
		GameObject wOBJS = GameObject.FindGameObjectWithTag("gameOverPanel");
		for(int i=0;i<wOBJS.transform.childCount;i++)
		{
			wOBJS.transform.GetChild(i).gameObject.SetActive(false);
		}
		StartCoroutine(RESTART());
	}

	int validate ()
	{
		int win = -1;
		if (data [0] != -1 && ((data [0] == data [1]) && (data [1] == data [2]) && (data [0] == data [2]))) 
		{
			win = data [0];
			winPos[0] = 0;
			winPos[1] = 1;
			winPos[2] = 2;
		}
		else if (data[3] != -1 &&((data[3] == data[4]) && (data[4] == data[5]) && (data[3] == data[5])))
		{
			win = data [3];
			winPos[0] = 3;
			winPos[1] = 4;
			winPos[2] = 5;
		}
		else if (data[6] != -1 &&((data[6] == data[7]) && (data[7] == data[8]) && (data[6] == data[8])))
		{
			win = data [6];
			winPos[0] = 6;
			winPos[1] = 7;
			winPos[2] = 8;
		}
		else if (data[0] != -1 &&((data[0] == data[4]) && (data[4] == data[8]) && (data[0] == data[8])))
		{
			win = data [0];
			winPos[0] = 0;
			winPos[1] = 4;
			winPos[2] = 8;
		}
		else if (data[2] != -1 &&((data[2] == data[4]) && (data[4] == data[6]) && (data[2] == data[6])))
		{
			win = data [2];
			winPos[0] = 2;
			winPos[1] = 4;
			winPos[2] = 6;
		}
		else if (data[0] != -1 &&((data[0] == data[3]) && (data[3] == data[6]) && (data[0] == data[6])))
		{
			win = data [0];
			winPos[0] = 0;
			winPos[1] = 3;
			winPos[2] = 6;
		}
		else if (data[1] != -1 &&((data[1] == data[4]) && (data[4] == data[7]) && (data[1] == data[7])))
		{
			win = data [1];
			winPos[0] = 1;
			winPos[1] = 4;
			winPos[2] = 7;
		}
		else if (data[2] != -1 &&((data[2] == data[5]) && (data[5] == data[8]) && (data[2] == data[8])))
		{
			win = data [2];
			winPos[0] = 2;
			winPos[1] = 5;
			winPos[2] = 8;
		}
   		return win ;

	}

	void Update ()
	{
		xText.text = "X: " + xScore;
		oText.text = "O: " + oScore;
	
		if (GameStarted) 
		{
			int playerWon = validate ();

			if (playerWon > -1) 
			{
				player.canPlay = false;
				stop = true;
			}

			if (moves >= 9 && playerWon == -1) 
			{
				Debug.Log ("GameOver !! TIE !!");
				player.canPlay = false;
				stop = true;
			}

			if(!animating)
			{
				if(playerWon == -1 && moves >= 9)
				{
					GameObject wOBJS = GameObject.FindGameObjectWithTag("gameOverPanel");
					for(int i=0;i<wOBJS.transform.childCount;i++)
					{
						wOBJS.transform.GetChild(i).gameObject.SetActive(true);
					}
					GameObject.FindGameObjectWithTag("winInfo").GetComponent<Text>().text = "!! Tie Game !!" ;
					GameObject.FindGameObjectWithTag("winInfo").GetComponent<Text>().color = Color.red;
					StartCoroutine(paintRED());
					GameStarted = false;
				}
				else if(playerWon == 0)
				{
					StartCoroutine(paintGREEN());
					xScore++;
					Debug.Log ("GameOver X Wins");
					GameObject wOBJS = GameObject.FindGameObjectWithTag("gameOverPanel");
					for(int i=0;i<wOBJS.transform.childCount;i++)
					{
						wOBJS.transform.GetChild(i).gameObject.SetActive(true);
					}
					GameObject.FindGameObjectWithTag("winInfo").GetComponent<Text>().text = ".. X Wins .." ;
					GameObject.FindGameObjectWithTag("winInfo").GetComponent<Text>().color = Color.green;
					GameStarted = false;
				}
				else if(playerWon == 1)
				{
					StartCoroutine(paintGREEN());
					oScore++;
					Debug.Log ("GameOver O Wins");
					GameObject wOBJS = GameObject.FindGameObjectWithTag("gameOverPanel");
					for(int i=0;i<wOBJS.transform.childCount;i++)
					{
						wOBJS.transform.GetChild(i).gameObject.SetActive(true);
					}
					GameObject.FindGameObjectWithTag("winInfo").GetComponent<Text>().text = ".. O Wins .." ;
					GameObject.FindGameObjectWithTag("winInfo").GetComponent<Text>().color = Color.green;
					GameStarted = false;
				}
				if(!stop)
				{
					if(turn == 1)
					{
						player.canPlay = false;
						ai.makeMove(GameDifficulty);
					}
					else
					{
						player.canPlay = true;
					}
				}
			}
		}

	}

	IEnumerator paintRED()
	{
		Vector3[] poss = new Vector3[9];
		float speed = 10.0F;
    	for(int i=0;i<9;i++)
    	{
			poss[i] = objs[i].transform.position;
    	}

		for(int i=0;i<9;i++)
		{
    		float journeyLength;
			Vector3 pos = objs[i].transform.position;
			Vector3 newPos = pos;
			newPos.z = -10;
			float startTime = Time.time;
        	journeyLength = Vector3.Distance(pos, newPos);
			do
			{
				if(moves == 0)
					 yield break;
				float distCovered = (Time.time - startTime) * speed;
       			float fracJourney = distCovered / journeyLength;
				objs[i].transform.position = Vector3.Lerp(objs[i].transform.position,newPos, fracJourney);
				yield return new WaitForSeconds(0);
			}while(Vector3.Distance(objs[i].transform.position,newPos) > 0.5f);
			objs[i].transform.position = newPos;
			objs[i].GetComponentInChildren<Renderer>().material.color = Color.red;
			newPos = poss[i];
			do
			{
				if(moves == 0)
					yield break;
				float distCovered = (Time.time - startTime) * speed;
       			float fracJourney = distCovered / journeyLength;
				objs[i].transform.position = Vector3.Lerp(objs[i].transform.position,newPos, fracJourney);
				yield return new WaitForSeconds(0);
			}while(Vector3.Distance(objs[i].transform.position,newPos) > 0.5f);
			objs[i].transform.position = newPos;
		}
	}

	IEnumerator paintGREEN()
	{
    	Vector3[] poss = new Vector3[3];
		float speed = 5.0F;
		int first = Random.Range(1,3);
		int last = Random.Range(0,2);
		poss[0] = objs[winPos[first]].transform.position;
		if(last == first && first == 1)
		{
			poss[1] = objs[winPos[2]].transform.position;
			poss[2] = objs[winPos[0]].transform.position;
		}
		else
		{
			poss[1] = objs[winPos[0]].transform.position;
			poss[2] = objs[winPos[1]].transform.position;
		}
		


		for(int i=0;i<3;i++)
		{
    		float journeyLength;
			Vector3 pos = objs[winPos[i]].transform.position;
			Vector3 newPos = pos;
			newPos.z = -10;
			float startTime = Time.time;
        	journeyLength = Vector3.Distance(pos, newPos);
			do
			{
				if(moves == 0)
					yield break;
				float distCovered = (Time.time - startTime) * speed;
       			float fracJourney = distCovered / journeyLength;
				objs[winPos[i]].transform.position = Vector3.Lerp(objs[winPos[i]].transform.position,newPos, fracJourney);
				yield return new WaitForSeconds(0);
			}while(Vector3.Distance(objs[winPos[i]].transform.position,newPos) > 0.5f);
			objs[winPos[i]].transform.position = newPos;
			objs[winPos[i]].GetComponentInChildren<Renderer>().material.color = Color.green;
		}

		for(int i=0;i<3;i++)
		{
    		float journeyLength;
			Vector3 pos = objs[winPos[i]].transform.position;
			Vector3 newPos = poss[i];
			newPos.z = -10;
			float startTime = Time.time;
        	journeyLength = Vector3.Distance(pos, newPos);
			do
			{
				if(moves == 0)
					yield break;
				float distCovered = (Time.time - startTime) * speed;
       			float fracJourney = distCovered / journeyLength;
				objs[winPos[i]].transform.position = Vector3.Lerp(objs[winPos[i]].transform.position,newPos, fracJourney);
				yield return new WaitForSeconds(0);
			}while(Vector3.Distance(objs[winPos[i]].transform.position,newPos) > 0.5f);
			objs[winPos[i]].transform.position = newPos;
		}

		for(int i=0;i<3;i++)
		{
    		float journeyLength;
			Vector3 pos = objs[winPos[i]].transform.position;
			Vector3 newPos = pos;
			newPos.z = 0;
			float startTime = Time.time;
        	journeyLength = Vector3.Distance(pos, newPos);
			do
			{
				if(moves == 0)
					yield break;
				float distCovered = (Time.time - startTime) * speed;
       			float fracJourney = distCovered / journeyLength;
				objs[winPos[i]].transform.position = Vector3.Lerp(objs[winPos[i]].transform.position,newPos, fracJourney);
				yield return new WaitForSeconds(0);
			}while(Vector3.Distance(objs[winPos[i]].transform.position,newPos) > 0.5f);
			objs[winPos[i]].transform.position = newPos;
		}
	}

	IEnumerator RESTART()
	{
		float speed = 20.0f;
		for(int i=0;i<9;i++)
		{
			if(objs[i] == null)
			{
				continue;
			}
			else
			{
	    		float journeyLength;
				Vector3 pos = objs[i].transform.position;
				Vector3 newPos = new Vector3(0,-4.3f,0);
				float startTime = Time.time;
	        	journeyLength = Vector3.Distance(pos, newPos);
				do
				{
					if(moves == 0)
						yield break;
					float distCovered = (Time.time - startTime) * speed;
	       			float fracJourney = distCovered / journeyLength;
					objs[i].transform.position = Vector3.Lerp(objs[i].transform.position,newPos, fracJourney);
					yield return new WaitForSeconds(0);
				}while(Vector3.Distance(objs[i].transform.position,newPos) > 0.5f);
				objs[i].transform.position = newPos;
			}
		}
		nowRESET();
	}

	void nowRESET ()
	{
		for (int i = 0; i < objs.Length; i++)
		{
			data[i] = -1;
			if(objs[i])
			{
				Destroy(objs[i]);
			}
		}
			

		for(int i=0;i<colls.Length;i++)
		{
			colls[i].enabled = true;
		}
		turn = 0;
		moves = 0;
		initialized = true;
		player.canPlay = true;
		GameStarted = true;
		stop = false;
	}

	public void endGame()
	{
		StopAllCoroutines ();
		SceneManager.LoadScene("mainMenu");
	}
}

