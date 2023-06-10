using UnityEditor;
using UnityEngine;

public class OpenHelper : MonoBehaviour
{
    private static string helpfile = Application.streamingAssetsPath + "/Helper.chm";
    public static void HelpMe()
    {
        Debug.Log(helpfile);
        if (System.IO.File.Exists(helpfile))
        {
            System.Diagnostics.Process.Start(helpfile);
        }
        else
        {
            Debug.Log("Файл руководства не найден");
        }
    }
}
