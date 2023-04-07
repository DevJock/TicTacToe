using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menus : MonoBehaviour 
{
	//MENUNetworkStarter mns;
	public int pageIndex; // 0: mainMenu 2: local multiplayer 3: wireless multiplayer 4: extras
	public GameObject page0Contents;
	public GameObject page2Contents;
	public GameObject page3Contents;
	public GameObject page4Contents;

	void Start()
	{
		//mns = GetComponent<MENUNetworkStarter>();
	}

	public void displayPage0 ()
	{
		pageIndex = 0;
		page0Contents.SetActive(true);
		page2Contents.SetActive(false);
		page3Contents.SetActive(false);
		page4Contents.SetActive(false);
	}

	public void displayPage1_SP()
	{
		pageIndex = 1;
		SceneManager.LoadScene("gameScene_SP");
	}

	public void displayPage1_COOP()
	{
		pageIndex = 1;
		SceneManager.LoadScene("gameScene_Local");
	}

	public void displayPage1_N()
	{
		pageIndex = 1;
		//mns.StartServer();
	}

	public void displayPage1_NJ()
	{
		pageIndex = 1;
		string IP = page3Contents.transform.Find("IP").transform.GetChild(0).GetComponent<InputField>().text;
		//mns.hostIP = IP;
		//mns.StartClientLan();
	}

	public void displayPage2 ()
	{
		pageIndex = 2;
		page0Contents.SetActive(false);
		page2Contents.SetActive(true);
		page3Contents.SetActive(false);
		page4Contents.SetActive(false);
	}

	public void displayPage3 ()
	{
		pageIndex = 3;
		page0Contents.SetActive(false);
		page2Contents.SetActive(false);
		page3Contents.SetActive(true);
		page4Contents.SetActive(false);
		//page3Contents.transform.Find("IP").transform.GetChild(0).GetComponent<InputField>().text = mns.hostIP;
	}

	public void displayPage4 ()
	{
		pageIndex = 4;
		page0Contents.SetActive(false);
		page2Contents.SetActive(false);
		page3Contents.SetActive(false);
		page4Contents.SetActive(true);
		//page4Contents.transform.Find("IP").GetComponent<Text>().text = "IP: "+mns.myIP;
	}


}
