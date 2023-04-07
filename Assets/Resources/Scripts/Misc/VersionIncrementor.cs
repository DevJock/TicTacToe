using UnityEngine;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class VersionIncrementor : MonoBehaviour
{
	public string versionText;
	//debugSystem ds;
   public  void Start()   
    {
        //the file name and path.  No path is the base of the Unity project directory (same level as Assets folder)
        string versionTextFileNameAndPath = "version.txt";
	//	ds = GameObject.FindGameObjectWithTag("debugSystem").GetComponent<debugSystem>();
        versionText = CommonUtils.ReadTextFile(versionTextFileNameAndPath);
 
        if (versionText != null)       
         {
            versionText = versionText.Trim(); //clean up whitespace if necessary
            string[] lines = versionText.Split('.');
            int MajorVersion = int.Parse(lines[0]);
            int MinorVersion = int.Parse(lines[1]);
            int SubMinorVersion = int.Parse(lines[2]) + 1; //increment here
            string SubVersionText = lines[3].Trim();
 
           // Debug.Log("Major, Minor, SubMinor, SubVerLetter: " + MajorVersion + " " + MinorVersion + " " + SubMinorVersion + " " + SubVersionText);
 
            versionText = MajorVersion.ToString("0") + "." +
                          MinorVersion.ToString("0") + "." +
                          SubMinorVersion.ToString("000") + "." +
                          SubVersionText;
 
            Debug.Log("Version Incremented " + versionText);
            //save the file (overwrite the original) with the new version number
            CommonUtils.WriteTextFile(versionTextFileNameAndPath, versionText);
            //save the file to the Resources directory so it can be used by Game code
            CommonUtils.WriteTextFile("Assets/Resources/version.txt", versionText);
            //tell unity the file changed (important if the versionTextFileNameAndPath is in the Assets folder)
            #if UNITY_EDITOR
            AssetDatabase.Refresh();
            #endif
        }
        else 
        {
            //no file at that path, make it
            CommonUtils.WriteTextFile(versionTextFileNameAndPath, "0.0.0.a");
        }
       // ds.updateVersion(versionText);
    }
}