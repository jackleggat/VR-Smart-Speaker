using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WriteToFile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FileWrite();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void FileWrite() {
        string sceneName = SceneManager.GetActiveScene().name;
        string fname = sceneName + ".txt";
        string path = Path.Combine(Application.persistentDataPath, fname);
        StreamWriter sw = new StreamWriter(path);

        sw.WriteLine("This Test Worked!");

        sw.Close();
    }
}
