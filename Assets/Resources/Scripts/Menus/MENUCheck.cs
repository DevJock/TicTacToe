using UnityEngine;
using System.Collections;

public class MENUCheck : MonoBehaviour
 {
 	public int[] data;
 	public int[] winPos;
 	public bool canStop;
 	public int wonGUY;

 	void Start ()
	{
		wonGUY = -2;
		data = new int[9];
		winPos = new int[3];
		for (int i = 0; i < 9; i++) 
		{
			if(i < 3)
			{
				winPos[i] = -1;
			}
			data[i] = -1;
		}	

 	}

 	public void reset()
 	{
		wonGUY = -2;
		canStop = false;	
		data = new int[9];
		winPos = new int[3];
		for (int i = 0; i < 9; i++) 
		{
			if(i < 3)
			{
				winPos[i] = -1;
			}
			data[i] = -1;
		}
 	}

	void Update ()
	{
		if(!canStop)
		{
			wonGUY = validate();
			if(wonGUY == 0 || wonGUY == 1)
				canStop = true;
		}
	}

	
	public  int validate ()
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
}
