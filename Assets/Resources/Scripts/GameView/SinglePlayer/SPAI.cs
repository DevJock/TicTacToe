using UnityEngine;
using System.Collections;

public class SPAI : MonoBehaviour 
{
	SPGameManager gm;
	public string pName = "O";
	void Start ()
	{
		gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<SPGameManager>();
	}
	public void makeMove (int toughness)
	{
		int move = -1;
		if(toughness == 0)
		{
			int i = 0;
			int count = 0;
			do
			{
				i = Random.Range(0,9);
				count ++;
				if(count > 10)
				{
					move = -2;
					break;
				}
			}while(gm.data[i] != -1); 
			move = i;
		}
		else if(toughness == 1)
		{
			move = CalculateMove();
		}
		placeOBJ(move,1);
		gm.data[move] = 1;
		gm.animating = true;
		gm.turn = 0;
		gm.moves++;
	}

	void placeOBJ (int gridPos, int ID)
	{
		GameObject obj;
		if(ID == 0)
		{
			obj = Resources.Load("Prefabs/SinglePlayer/SPX") as GameObject;
		}
		else
		{
			obj = Resources.Load("Prefabs/SinglePlayer/SPO") as GameObject;
		}
		GameObject placer = Instantiate(Resources.Load("Prefabs/Common/Placer") as GameObject);
		GameObject thisObject = (GameObject)Instantiate(obj);
		Vector3 position = gm.colls[gridPos].transform.position;
		thisObject.transform.parent = placer.transform;
		thisObject.transform.localPosition = new Vector3(position.x/5,position.y/5,0);
		thisObject.name = pName+"("+position+")";
	}

	int CalculateMove()
	{
		int myMove = -1;

		//WINNING MOVES
		//HORIZONTAL
		if (gm.data [0] == gm.data [1] && gm.data[1] == 0 && gm.data[2] == -1)
		{
			myMove = 2;
		}
		else if (gm.data [1] == gm.data [2] && gm.data[2] == 0 && gm.data[0] == -1)
		{
			myMove = 0;
		}
		else if (gm.data [0] == gm.data [2] && gm.data[2] == 0 && gm.data[1] == -1)
		{
			myMove = 1;
		}
		else if (gm.data [3] == gm.data [4] && gm.data[4] == 0 && gm.data[5] == -1)
		{
			myMove = 5;
		}
		else if (gm.data [4] == gm.data [5] && gm.data[5] == 0 && gm.data[3] == -1)
		{
			myMove = 3;
		}
		else if (gm.data [3] == gm.data [5] && gm.data[5] == 0 && gm.data[4] == -1)
		{
			myMove = 4;
		}
		else if (gm.data [6] == gm.data [7] && gm.data[7] == 0 && gm.data[8] == -1)
		{
			myMove = 8;
		}
		else if (gm.data [7] == gm.data [8] && gm.data[8] == 0 && gm.data[6] == -1)
		{
			myMove = 6;
		}
		else if (gm.data [6] == gm.data [8] && gm.data[8] == 0 && gm.data[7] == -1)
		{
			myMove = 7;
		}
		// VERTICAL 
		else if (gm.data [0] == gm.data [3] && gm.data[3] == 1 && gm.data[6] == -1)
		{
			myMove = 6;
		}
		else if (gm.data [3] == gm.data [6] && gm.data[6] == 1 && gm.data[0] == -1)
		{
			myMove = 0;
		}
		else if (gm.data [0] == gm.data [6] && gm.data[6] == 1 && gm.data[3] == -1)
		{
			myMove = 3;
		}
		else if (gm.data [1] == gm.data [4] && gm.data[4] == 1 && gm.data[7] == -1)
		{
			myMove = 7;
		}
		else if (gm.data [4] == gm.data [7] && gm.data[7] == 1 && gm.data[1] == -1)
		{
			myMove = 1;
		}
		else if (gm.data [1] == gm.data [7] && gm.data[7] == 1 && gm.data[4] == -1)
		{
			myMove = 4;
		}
		else if (gm.data [2] == gm.data [5] && gm.data[5] == 1 && gm.data[8] == -1)
		{
			myMove = 8;
		}
		else if (gm.data [5] == gm.data [8] && gm.data[8] == 1 && gm.data[2] == -1)
		{
			myMove = 2;
		}
		else if (gm.data [2] == gm.data [8] && gm.data[8] == 1 && gm.data[5] == -1)
		{
			myMove = 5;
		}
		// DIAGONAL 
		else if (gm.data [0] == gm.data [4] && gm.data[4] == 1 && gm.data[8] == -1)
		{
			myMove = 8;
		}
		else if (gm.data [4] == gm.data [8] && gm.data[8] == 1 && gm.data[0] == -1)
		{
			myMove = 0;
		}
		else if (gm.data [0] == gm.data [8] && gm.data[8] == 1 && gm.data[4] == -1)
		{
			myMove = 4;
		}
		else if (gm.data [2] == gm.data [4] && gm.data[4] == 1 && gm.data[6] == -1)
		{
			myMove = 6;
		}
		else if (gm.data [4] == gm.data [6] && gm.data[6] == 1 && gm.data[3] == -1)
		{
			myMove = 2;
		}
		else if (gm.data [2] == gm.data [6] && gm.data[6] == 1 && gm.data[4] == -1)
		{
			myMove = 4;
		}
		// BLOCKING MOVES
		// HORIZONTAL
		else if (gm.data [0] == gm.data [1] && gm.data[1] == 0 && gm.data[2] == -1)
		{
			myMove = 2;
		}
		else if (gm.data [1] == gm.data [2] && gm.data[2] == 0 && gm.data[0] == -1)
		{
			myMove = 0;
		}
		else if (gm.data [0] == gm.data [2] && gm.data[2] == 0 && gm.data[1] == -1)
		{
			myMove = 1;
		}
		else if (gm.data [3] == gm.data [4] && gm.data[4] == 0 && gm.data[5] == -1)
		{
			myMove = 5;
		}
		else if (gm.data [4] == gm.data [5] && gm.data[5] == 0 && gm.data[3] == -1)
		{
			myMove = 3;
		}
		else if (gm.data [3] == gm.data [5] && gm.data[5] == 0 && gm.data[4] == -1)
		{
			myMove = 4;
		}
		else if (gm.data [6] == gm.data [7] && gm.data[7] == 0 && gm.data[8] == -1)
		{
			myMove = 8;
		}
		else if (gm.data [7] == gm.data [8] && gm.data[8] == 0 && gm.data[6] == -1)
		{
			myMove = 6;
		}
		else if (gm.data [6] == gm.data [8] && gm.data[8] == 0 && gm.data[7] == -1)
		{
			myMove = 7;
		}
		// VERTICAL 
		else if (gm.data [0] == gm.data [3] && gm.data[3] == 0 && gm.data[6] == -1)
		{
			myMove = 6;
		}
		else if (gm.data [3] == gm.data [6] && gm.data[6] == 0 && gm.data[0] == -1)
		{
			myMove = 0;
		}
		else if (gm.data [0] == gm.data [6] && gm.data[6] == 0 && gm.data[3] == -1)
		{
			myMove = 3;
		}
		else if (gm.data [1] == gm.data [4] && gm.data[4] == 0 && gm.data[7] == -1)
		{
			myMove = 7;
		}
		else if (gm.data [4] == gm.data [7] && gm.data[7] == 0 && gm.data[1] == -1)
		{
			myMove = 1;
		}
		else if (gm.data [1] == gm.data [7] && gm.data[7] == 0 && gm.data[4] == -1)
		{
			myMove = 4;
		}
		else if (gm.data [2] == gm.data [5] && gm.data[5] == 0 && gm.data[8] == -1)
		{
			myMove = 8;
		}
		else if (gm.data [5] == gm.data [8] && gm.data[8] == 0 && gm.data[2] == -1)
		{
			myMove = 2;
		}
		else if (gm.data [2] == gm.data [8] && gm.data[8] == 0 && gm.data[5] == -1)
		{
			myMove = 5;
		}
		// DIAGONAL 
		else if (gm.data [0] == gm.data [4] && gm.data[4] == 0 && gm.data[8] == -1)
		{
			myMove = 8;
		}
		else if (gm.data [4] == gm.data [8] && gm.data[8] == 0 && gm.data[0] == -1)
		{
			myMove = 0;
		}
		else if (gm.data [0] == gm.data [8] && gm.data[8] == 0 && gm.data[4] == -1)
		{
			myMove = 4;
		}
		else if (gm.data [2] == gm.data [4] && gm.data[4] == 0 && gm.data[6] == -1)
		{
			myMove = 6;
		}
		else if (gm.data [4] == gm.data [6] && gm.data[6] == 0 && gm.data[3] == -1)
		{
			myMove = 2;
		}
		else if (gm.data [2] == gm.data [6] && gm.data[6] == 0 && gm.data[4] == -1)
		{
			myMove = 4;
		}
		else 
		{
			do
			{
				myMove = Random.Range(0,9);
			}while(gm.data[myMove] != -1); 	
		}
		Debug.Log("Im moving to: "+myMove);
		return myMove;
	}

}
