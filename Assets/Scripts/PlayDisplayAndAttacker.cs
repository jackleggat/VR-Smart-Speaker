using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;


public class PlayDisplayAndAttacker : MonoBehaviour
{

    public AudioSource attackerSound;
    public GameObject canvasObject;
    private InputDevice targetDevice;
    private bool timerIsRunning = false;

    // Start is called before the first frame update
    void Start()
    {
        canvasObject.SetActive(false);

        List<InputDevice> devices = new List<InputDevice>();
        InputDeviceCharacteristics rightControllerCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(rightControllerCharacteristics, devices);
        
        if(devices.Count > 0) {
            targetDevice = devices[0];
        }
    }

    // Update is called once per frame
    void Update()
    {
        targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue);
        if (primaryButtonValue) {
            if (timerIsRunning == false) {
                StartCoroutine(WaitTimer());
            }
        }
    }

    IEnumerator WaitTimer()
    {
        timerIsRunning = true;
        int wait = Random.Range(3,6);
        yield return new WaitForSeconds (wait);
        attackerSound.Play();
        yield return new WaitForSeconds (4);
        canvasObject.SetActive(true);
        yield return new WaitForSeconds (5);
        canvasObject.SetActive(false);
        timerIsRunning = false;
    }
}