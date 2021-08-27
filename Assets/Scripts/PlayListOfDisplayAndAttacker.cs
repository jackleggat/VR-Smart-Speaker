using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Text;
using System.IO;

public class PlayListOfDisplayAndAttacker : MonoBehaviour
{
    public AudioSource attacker;
    public List<AudioClip> attackerClips;
    public List<GameObject> AIObjects;
    public List<GameObject> instructionObjects;
    private InputDevice targetDevice;
    private bool timerIsRunning = false;
    private bool trigPressed = false;
    private bool requestMade = false;
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
        for (int i=0; i<instructionObjects.Count; i++){
            AIObjects[i].SetActive(false);
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
            if (timerIsRunning == false && requestMade == false) {
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
        requestMade = true;
        timerIsRunning = true;
        int wait = Random.Range(3,6);
        yield return new WaitForSeconds (wait);
        attacker.PlayOneShot(attackerClips[ind],1);
        yield return new WaitForSeconds((attackerClips[ind].length)+2);
        AIObjects[ind].SetActive(true);
        yield return new WaitForSeconds (5);
        AIObjects[ind].SetActive(false);
        deltaStarted = true;
        timerIsRunning = false;
/*         yield return new WaitForSeconds (5);
        ind++;
        if (ind < (instructionObjects.Count) - 1) {
            instructionObjects[ind].SetActive(true);
        }
        else {
            SceneManager.LoadScene("Questionnaire");
        } */
    }

    IEnumerator NextWaitTimer()
    {
        trigPressed = true;
        deltaStarted = false;
        request = instructionObjects[ind].name;
        sw.WriteLine(request + ", " + time);
        time = 0;
        requestMade = false;
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
