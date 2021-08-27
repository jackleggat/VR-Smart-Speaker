using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class PlayListOfDisplays : MonoBehaviour
{

    private InputDevice targetDevice;
    public List<GameObject> AIObjects;
    public List<GameObject> instructionObjects;
    private float timeRemaining = 5;
    private bool timerIsRunning = false;
    private int cond = 0;

    // Start is called before the first frame update
    void Start()
    {
        for (int i=0; i<AIObjects.Count; i++){
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
    }

    // Update is called once per frame
    void Update()
    {
        targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue);
        if (primaryButtonValue) {
            AIObjects[cond].SetActive(true);
            timerIsRunning = true;
        }

        //targetDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerButtonValue);
        //if (triggerButtonValue) {
        //    cond++;
        //}

        if (timerIsRunning == true) {
            if (timeRemaining > 0) {
                timeRemaining -= Time.deltaTime;
            }
            else {
                AIObjects[cond].SetActive(false);
                timerIsRunning = false;
                timeRemaining = 5;
                cond++;
                if (cond<AIObjects.Count) {
                    instructionObjects[cond].SetActive(true);
                }
            }
        }
    }
}
