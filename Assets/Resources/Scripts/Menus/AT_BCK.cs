using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class AT_BCK : MonoBehaviour 
{	
	public int speed;
	GameObject[] GRID;
	float fPOS;

	public int numberOfGrids;
	Vector3[] gridCenters;
	List<int> ranList;


	void Start ()
	{
		ranList = new List<int>();
		makeRandomRange(15);
		gridCenters = new Vector3[numberOfGrids];
		GRID = new GameObject[numberOfGrids];
		float x = -100;
		for(int i=0;i<5;i++)
		{
			x += 40;
			gridCenters[i].x = x;
			gridCenters[i].y = -40;
		}
		x = -100;
		for(int i=5;i<10;i++)
		{
			x += 40;
			gridCenters[i].x = x;
			gridCenters[i].y = 0;
		}
		x=-100;
		for(int i=10;i<15;i++)
		{
			x += 40;
			gridCenters[i].x = x;
			gridCenters[i].y = 40;
		}
		StartCoroutine(animateGrids());
	}

	IEnumerator animateGrids()
	{
		int i = 0;
		for(i=0;i<numberOfGrids;i++)
		{
			StartCoroutine(MakeGRID(i));
			yield return new WaitForSeconds(5.0f);
		}

	}

	 IEnumerator MakeGRID (int ID)
	{
		GRID[ID] = (GameObject)Instantiate (Resources.Load ("Prefabs/Common/GRID") as GameObject, gridCenters[ranList[ID]], Quaternion.identity);
		GRID[ID].AddComponent<MENUCheck>();
		for (int i = 0; i < GRID[ID].transform.childCount; i++) 
		{
			Vector3 tPOS = GRID[ID].transform.GetChild(i).localPosition;
			fPOS = tPOS.z;
			GRID[ID].transform.GetChild(i).localPosition = new Vector3(tPOS.x,tPOS.y,-500);
			GRID[ID].transform.GetChild(i).gameObject.SetActive(false);
		}
		while(true)
		{
			yield return StartCoroutine(buildGRID(ID));
			yield return StartCoroutine(playGRID(ID));
			yield return StartCoroutine(playCHECK(ID));
			yield return StartCoroutine(REMOVE(ID));
			yield return StartCoroutine(destroyGRID(ID));
		}

	}

	IEnumerator buildGRID(int ID)
	{
		GameObject theGRID = GRID[ID];
		for(int i=0;i<theGRID.transform.childCount;i++)
		{
			Vector3 finalPos = theGRID.transform.GetChild(i).localPosition;
			finalPos.z = fPOS;
			float journeyLength;
			float startTime = Time.time;
			journeyLength = Vector3.Distance(theGRID.transform.GetChild(i).localPosition, finalPos);
			theGRID.transform.GetChild(i).gameObject.SetActive(true);
			do
			{
				float distCovered = (Time.time - startTime) * speed;
       			float fracJourney = distCovered / journeyLength;
				theGRID.transform.GetChild(i).localPosition = Vector3.Lerp(theGRID.transform.GetChild(i).localPosition,finalPos, fracJourney);
				yield return new WaitForSeconds(0);
			}while(Vector3.Distance(theGRID.transform.GetChild(i).localPosition,finalPos) > 0.5f);
			theGRID.transform.GetChild(i).localPosition = finalPos;
		}
		yield return new WaitForSeconds(Random.Range(1,4));
	}


	IEnumerator playGRID (int ID)
	{
		for(int i=0;i<9;i++)
		{
			GameObject placer = Instantiate(Resources.Load("Prefabs/Common/Placer") as GameObject);
			GameObject thisObject;
			MENUCheck mc = GRID[ID].GetComponent<MENUCheck>();
			if(mc.wonGUY != -1)
			{	
				break;
			}
			if(i%2 ==0)
			{
				thisObject  = (GameObject)Instantiate(Resources.Load("Prefabs/Common/GX"));

			}
			else
			{
				thisObject  = (GameObject)Instantiate(Resources.Load("Prefabs/Common/GO"));
			}
			int pos;
			do
			{
				pos = Random.Range(0,9);
			}while(GRID[ID].transform.Find("Colliders").GetChild(pos).childCount != 0);
			if(i%2 ==0)
			{
				mc.data[pos] = 0;
			}
			else
			{
				mc.data[pos] = 1;
			}
			Vector3 position =GRID[ID].transform.Find("Colliders").GetChild(pos).transform.position;
			thisObject.transform.parent = placer.transform;
			thisObject.transform.localPosition = new Vector3(position.x/5,position.y/5,0);
			yield return new WaitForSeconds(Random.Range(0,3));
		}
		yield return new WaitForSeconds(Random.Range(0,2));
	}


	IEnumerator playCHECK(int ID)
	{
		MENUCheck mc = GRID[ID].GetComponent<MENUCheck>();
		if(mc.wonGUY == 0 || mc.wonGUY == 1)
		{
			for(int i=0;i<3;i++)
			{
				GameObject obj = GRID[ID].transform.Find("Colliders").GetChild(mc.winPos[i]).GetChild(0).gameObject;
				Vector3 finalPos =obj.transform.localPosition;
				finalPos.y = -20;
				float journeyLength;
				float startTime = Time.time;
				journeyLength = Vector3.Distance(obj.transform.localPosition, finalPos);
				do
				{
					float distCovered = (Time.time - startTime) * speed;
	       			float fracJourney = distCovered / journeyLength;
					obj.transform.localPosition = Vector3.Lerp(obj.transform.localPosition,finalPos, fracJourney);
					yield return new WaitForSeconds(0);
				}while(Vector3.Distance(obj.transform.localPosition,finalPos) > 0.5f);
				obj.transform.localPosition = finalPos;
				obj.GetComponent<Renderer>().material.color = Color.green;

				finalPos.y = 0;
				startTime = Time.time;
				journeyLength = Vector3.Distance(obj.transform.localPosition, finalPos);
				do
				{
					float distCovered = (Time.time - startTime) * speed;
	       			float fracJourney = distCovered / journeyLength;
					obj.transform.localPosition = Vector3.Lerp(obj.transform.localPosition,finalPos, fracJourney);
					yield return new WaitForSeconds(0);
				}while(Vector3.Distance(obj.transform.localPosition,finalPos) > 0.5f);
				obj.transform.localPosition = finalPos;
			}
		}
		else
		{
			GameObject[] Obs = new GameObject[9];
			Vector3[] poss = new Vector3[9];
			float speed = 10.0F;
	    	for(int i=0;i<9;i++)
	    	{
				Obs[i] = GRID[ID].transform.Find("Colliders").GetChild(i).GetChild(0).gameObject;
				poss[i] = GRID[ID].transform.Find("Colliders").GetChild(i).GetChild(0).localPosition;
	    	}

			for(int i=0;i<9;i++)
			{
	    		float journeyLength;
				Vector3 pos = Obs[i].transform.localPosition;
				Vector3 newPos = pos;
				newPos.y = -20;
				float startTime = Time.time;
	        	journeyLength = Vector3.Distance(pos, newPos);
				do
				{
					float distCovered = (Time.time - startTime) * speed;
	       			float fracJourney = distCovered / journeyLength;
					Obs[i].transform.localPosition = Vector3.Lerp(Obs[i].transform.localPosition,newPos, fracJourney);
					yield return new WaitForSeconds(0);
				}while(Vector3.Distance(Obs[i].transform.localPosition,newPos) > 0.5f);
				Obs[i].transform.localPosition = newPos;
				Obs[i].GetComponent<Renderer>().material.color = Color.red;
				newPos = poss[i];
				do
				{
					float distCovered = (Time.time - startTime) * speed;
	       			float fracJourney = distCovered / journeyLength;
					Obs[i].transform.localPosition = Vector3.Lerp(Obs[i].transform.localPosition,newPos, fracJourney);
					yield return new WaitForSeconds(0);
				}while(Vector3.Distance(Obs[i].transform.localPosition,newPos) > 0.5f);
				Obs[i].transform.localPosition = newPos;
			}
		}
		yield return new WaitForSeconds(Random.Range(1,4));
	}


	IEnumerator REMOVE(int ID)
	{
		float speed = 5.0f;
		for(int i=0;i<9;i++)
		{
			GameObject obj;
			if(GRID[ID].transform.Find("Colliders").GetChild(i).childCount <= 0)
			{
				continue;
			}
			else
			{
				obj = GRID[ID].transform.Find("Colliders").GetChild(i).GetChild(0).gameObject;
	    		float journeyLength;
				Vector3 pos = obj.transform.position;
				Vector3 newPos = GRID[ID].transform.Find("Colliders").GetChild(i).position;
				float startTime = Time.time;
	        	journeyLength = Vector3.Distance(pos, newPos);
				do
				{
					float distCovered = (Time.time - startTime) * speed;
	       			float fracJourney = distCovered / journeyLength;
					obj.transform.position = Vector3.Lerp(obj.transform.position,newPos, fracJourney);
					yield return new WaitForSeconds(0);
				}while(Vector3.Distance(obj.transform.position,newPos) > 0.5f);
				obj.transform.position = newPos;
			}
		}
		yield return new WaitForSeconds(Random.Range(1,5));
	}


	IEnumerator destroyGRID(int ID)
	{
		for(int i=0;i<9;i++)
		{
			GameObject obj = null;
			if(GRID[ID].transform.Find("Colliders").GetChild(i).childCount == 0)
				continue;
			else
				obj = GRID[ID].transform.Find("Colliders").GetChild(i).GetChild(0).gameObject;
			Destroy(obj);
		}
		yield return new WaitForSeconds(1.0f);
		for(int i=0;i<GRID[ID].transform.childCount;i++)
		{
			Vector3 finalPos = GRID[ID].transform.GetChild(i).localPosition;
			finalPos.z = -200;
			float journeyLength;
			float startTime = Time.time;
			journeyLength = Vector3.Distance(GRID[ID].transform.GetChild(i).localPosition, finalPos);
			do
			{
				float distCovered = (Time.time - startTime) * speed;
       			float fracJourney = distCovered / journeyLength;
				GRID[ID].transform.GetChild(i).localPosition = Vector3.Lerp(GRID[ID].transform.GetChild(i).localPosition,finalPos, fracJourney);
				yield return new WaitForSeconds(0);
			}while(Vector3.Distance(GRID[ID].transform.GetChild(i).localPosition,finalPos) > 0.5f);
			GRID[ID].transform.GetChild(i).localPosition = finalPos;
			GRID[ID].transform.GetChild(i).gameObject.SetActive(false);
		}
		yield return new WaitForSeconds(Random.Range(5,30));
	}



	void makeRandomRange(int maxLength)
	{
		List<int> uniqueNumbers = new List<int>();
		for(int i = 0; i < maxLength; i++)
		{
    	   uniqueNumbers.Add(i);
    	}
		for(int i = 0; i< maxLength; i ++)
    	{
    		 int ranNum = uniqueNumbers[Random.Range(0,uniqueNumbers.Count)];
    		 ranList.Add(ranNum);
     		 uniqueNumbers.Remove(ranNum);
   		} 
   }


	List<int> makeRandomMoves()
	{
		List <int> t = new List<int>();
		List<int> uniqueNumbers = new List<int>();
		for(int i = 0; i < 9; i++)
		{
    	   uniqueNumbers.Add(i);
    	}
		for(int i = 0; i< 9; i ++)
    	{
    		 int ranNum = uniqueNumbers[Random.Range(0,uniqueNumbers.Count)];
    		 ranList.Add(ranNum);
     		 uniqueNumbers.Remove(ranNum);
   		} 
   		return t;
   }
}
