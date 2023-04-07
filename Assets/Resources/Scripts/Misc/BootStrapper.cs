using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class BootStrapper : MonoBehaviour 
{
	static GameObject menuPC;
	static GameObject menuMobile;
	static GameObject gamePC;
	static GameObject gameMobile;
	static Menus m;
	static Camera UICAMERA;
	Camera MainCamera;

	public void Awake()
     {
         DontDestroyOnLoad(this);
         if (FindObjectsOfType(GetType()).Length > 1)
         {
             Destroy(gameObject);
         }
     }


	void Start () 
	{
		Application.targetFrameRate = 60;
		m = GetComponent<Menus>();
		menuPC = gameObject.transform.Find("mmCanvasPC").gameObject;
		menuMobile = gameObject.transform.Find("mmCanvasMobile").gameObject;
		gamePC = gameObject.transform.Find("gmCanvasPC").gameObject;
		gameMobile = gameObject.transform.Find("gmCanvasMobile").gameObject;
		UICAMERA = gameObject.transform.Find("UICAM").GetComponent<Camera>();
		switch(Application.platform)
		{
			case RuntimePlatform.Android:
			case RuntimePlatform.IPhonePlayer:
			{
				menuMobile.SetActive(true);
				menuPC.SetActive(false);
				gameMobile.SetActive(false);
				gamePC.SetActive(false);
				menuMobile.GetComponent<Canvas>().worldCamera = UICAMERA;
				m.page0Contents = menuMobile.transform.GetChild(0).GetChild(2).gameObject;
				m.page2Contents = menuMobile.transform.GetChild(0).GetChild(3).gameObject;
				m.page3Contents = menuMobile.transform.GetChild(0).GetChild(4).gameObject;
				m.page4Contents = menuMobile.transform.GetChild(0).GetChild(5).gameObject;
				m.page0Contents.SetActive(true);
				m.page2Contents.SetActive(false);
				m.page3Contents.SetActive(false);
				m.page4Contents.SetActive(false);
				break;
			}
			case RuntimePlatform.WindowsPlayer:
			case RuntimePlatform.OSXPlayer:
			case RuntimePlatform.OSXEditor:
			case RuntimePlatform.WindowsEditor:
			{
				menuMobile.SetActive(false);
				menuPC.SetActive(true);
				gameMobile.SetActive(false);
				gamePC.SetActive(false);
				menuPC.GetComponent<Canvas>().worldCamera = UICAMERA;
				m.page0Contents = menuPC.transform.GetChild(0).GetChild(2).gameObject;
				m.page2Contents = menuPC.transform.GetChild(0).GetChild(3).gameObject;
				m.page3Contents = menuPC.transform.GetChild(0).GetChild(4).gameObject;
				m.page4Contents = menuPC.transform.GetChild(0).GetChild(5).gameObject;
				m.page0Contents.SetActive(true);
				m.page2Contents.SetActive(false);
				m.page3Contents.SetActive(false);
				m.page4Contents.SetActive(false);
				break;
			}
		}
	}
	
	void OnLevelWasLoaded(int level) 
	{
		Debug.Log ("Loaded Level: "+level);
        if (level == 0)
        {
			switch(Application.platform)
			{
				case RuntimePlatform.Android:
				case RuntimePlatform.IPhonePlayer:
				{
					menuMobile.SetActive(true);
					menuPC.SetActive(false);
					gameMobile.SetActive(false);
					gamePC.SetActive(false);
					menuMobile.GetComponent<Canvas>().worldCamera = UICAMERA;
					m.page0Contents.SetActive(true);
					m.page2Contents.SetActive(false);
					m.page3Contents.SetActive(false);
					m.page4Contents.SetActive(false);
					break;
				}
				case RuntimePlatform.WindowsPlayer:
				case RuntimePlatform.OSXPlayer:
				case RuntimePlatform.OSXEditor:
				case RuntimePlatform.WindowsEditor:
				{
					menuMobile.SetActive(false);
					menuPC.SetActive(true);
					gameMobile.SetActive(false);
					gamePC.SetActive(false);
					menuPC.GetComponent<Canvas>().worldCamera = UICAMERA;
					m.page0Contents.SetActive(true);
					m.page2Contents.SetActive(false);
					m.page3Contents.SetActive(false);
					m.page4Contents.SetActive(false);
					break;
				}
			}
	     }
	     else
	     {
			switch(Application.platform)
			{
				case RuntimePlatform.Android:
				case RuntimePlatform.IPhonePlayer:
				{
					menuMobile.SetActive(false);
					menuPC.SetActive(false);
					gameMobile.SetActive(true);
					gamePC.SetActive(false);
					MainCamera = Camera.main;
					MainCamera.fieldOfView = 60;
					gameMobile.GetComponent<Canvas>().worldCamera = UICAMERA;
					Button bend = gameMobile.transform.Find("gameOverObjs").Find("END").GetComponent<Button>();
					Button bres = gameMobile.transform.Find("gameOverObjs").Find("playAgain").GetComponent<Button>();
					Button bexit = gameMobile.transform.Find("ExitGame").GetComponent<Button>();
					if(level == 1)
					{
						bend.onClick.AddListener(()=>{GameObject.FindGameObjectWithTag("GameController").GetComponent<SPGameManager>().endGame();});
						bres.onClick.AddListener(()=>{GameObject.FindGameObjectWithTag("GameController").GetComponent<SPGameManager>().reset();});
						bexit.onClick.AddListener(()=>{GameObject.FindGameObjectWithTag("GameController").GetComponent<SPGameManager>().endGame();});
					}
					else if(level == 2)
					{
						bend.onClick.AddListener(()=>{GameObject.FindGameObjectWithTag("GameController").GetComponent<COOPGameManager>().endGame();});
						bres.onClick.AddListener(()=>{GameObject.FindGameObjectWithTag("GameController").GetComponent<COOPGameManager>().reset();});
						bexit.onClick.AddListener(()=>{GameObject.FindGameObjectWithTag("GameController").GetComponent<COOPGameManager>().endGame();});
					}
					else if(level == 3)
					{
						//bend.onClick.AddListener(()=>{GameObject.FindGameObjectWithTag("GameController").GetComponent<MUFGame>().endGame();});
						//bres.onClick.AddListener(()=>{GameObject.FindGameObjectWithTag("GameController").GetComponent<MUFGame>().reset();});
						//bexit.onClick.AddListener(()=>{GameObject.FindGameObjectWithTag("GameController").GetComponent<MUFGame>().endGame();});
					}
					else
					{
						bend.onClick.RemoveAllListeners();
						bres.onClick.RemoveAllListeners();
						bexit.onClick.RemoveAllListeners();
					}
					break;
				}
				case RuntimePlatform.WindowsPlayer:
				case RuntimePlatform.OSXPlayer:
				case RuntimePlatform.OSXEditor:
				case RuntimePlatform.WindowsEditor:
				{
					menuMobile.SetActive(false);
					menuPC.SetActive(false);
					gameMobile.SetActive(false);
					gamePC.SetActive(true);
					MainCamera = Camera.main;
					MainCamera.fieldOfView = 50;
					gamePC.GetComponent<Canvas>().worldCamera = UICAMERA;
					Button bend = gamePC.transform.Find("gameOverObjs").Find("END").GetComponent<Button>();
					Button bres = gamePC.transform.Find("gameOverObjs").Find("playAgain").GetComponent<Button>();
					Button bexit = gamePC.transform.Find("ExitGame").GetComponent<Button>();
					if(level == 1)
					{
						bend.onClick.AddListener(()=>{GameObject.FindGameObjectWithTag("GameController").GetComponent<SPGameManager>().endGame();});
						bres.onClick.AddListener(()=>{GameObject.FindGameObjectWithTag("GameController").GetComponent<SPGameManager>().reset();});
						bexit.onClick.AddListener(()=>{GameObject.FindGameObjectWithTag("GameController").GetComponent<SPGameManager>().endGame();});
					}
					else if(level == 2)
					{
						bend.onClick.AddListener(()=>{GameObject.FindGameObjectWithTag("GameController").GetComponent<COOPGameManager>().endGame();});
						bres.onClick.AddListener(()=>{GameObject.FindGameObjectWithTag("GameController").GetComponent<COOPGameManager>().reset();});
						bexit.onClick.AddListener(()=>{GameObject.FindGameObjectWithTag("GameController").GetComponent<COOPGameManager>().endGame();});
					}
					else if(level == 3)
					{
						//bend.onClick.AddListener(()=>{GameObject.FindGameObjectWithTag("GameController").GetComponent<MUFGame>().endGame();});
						//bres.onClick.AddListener(()=>{GameObject.FindGameObjectWithTag("GameController").GetComponent<MUFGame>().reset();});
						//bexit.onClick.AddListener(()=>{GameObject.FindGameObjectWithTag("GameController").GetComponent<MUFGame>().endGame();});
					}
					else
					{
						bend.onClick.RemoveAllListeners();
						bres.onClick.RemoveAllListeners();
						bexit.onClick.RemoveAllListeners();
					}
					break;
				}
			}
        }
    }
}
