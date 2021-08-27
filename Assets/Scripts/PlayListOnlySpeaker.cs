using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Text;
using System.IO;

public class PlayListOnlySpeaker : MonoBehaviour
{

    public AudioSource AS;
    public List<AudioClip> clips;
    public List<GameObject> instructionObjects;
    //private float timeRemaining = 5;
    private bool timerIsRunning = false;
    private bool trigPressed = false;
    private int requestsMade = 0;
    private InputDevice targetDevice;
    private int ind = 0;
    private string sceneName;
    private string fname;
    private string path;
    private string request;
    private float time = 0.00f;
    private bool deltaStarted = false;
    StreamWriter sw;

    // Start is called before the first frame update
    void Start()
    {
        AS = GetComponent<AudioSource>();

        for (int i=0; i<instructionObjects.Count; i++){
            instructionObjects[i].SetActive(false);
        }
        instructionObjects[0].SetActive(true);
        
        List<InputDevice> devices = new List<InputDevice>();
        InputDeviceCharacteristics rightControllerCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(rightControllerCharacteristics, devices);
        
        if(devices.Count > 0) {
            targetDevice = devices[0];
        }

        sceneName = SceneManager.GetActiveScene().name;
        fname = sceneName + ".txt";
        path = Path.Combine(Application.persistentDataPath, fname);
        sw = new StreamWriter(path);
    }

    // Update is called once per frame
    void Update()
    {
        targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue);
        if (primaryButtonValue) {
            if (timerIsRunning == false && trigPressed == false) {
                StartCoroutine(PlayWaitTimer());
            }
        }

        targetDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerButtonValue);
        if (triggerButtonValue) {
            if (timerIsRunning == false && trigPressed == false) {
                StartCoroutine(NextWaitTimer());
            }
        }

        if (deltaStarted == true) {
            time += Time.deltaTime;
        }
    }

    IEnumerator PlayWaitTimer()
    {
        timerIsRunning = true;
        requestsMade += 1;
        yield return new WaitForSeconds (2);
        AS.PlayOneShot(clips[ind],1);
        yield return new WaitForSeconds(clips[ind].length);
        deltaStarted = true;
        timerIsRunning = false;
        //yield return new WaitForSeconds (3);
        //ind++;
        //if (ind<instructionObjects.Count) {
        //    instructionObjects[ind].SetActive(true);
        //}
    }

    IEnumerator NextWaitTimer()
    {
        trigPressed = true;
        deltaStarted = false;
        request = instructionObjects[ind].name;
        sw.WriteLine(request + ", " + requestsMade + ", " + time);
        requestsMade = 0;
        time = 0;
        yield return new WaitForSeconds(1);
        if (ind < (instructionObjects.Count) - 1) {
            ind++;
            instructionObjects[ind].SetActive(true);
        }
        else {
            sw.Close();
            SceneManager.LoadScene("Questionnaire");
        }
        trigPressed = false;
    }
}
