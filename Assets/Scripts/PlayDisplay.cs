using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class PlayDisplay : MonoBehaviour
{
    private InputDevice targetDevice;
    public GameObject canvasObject;
    private float timeRemaining = 5;
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
            canvasObject.SetActive(true);
            timerIsRunning = true;
        }

        if (timerIsRunning == true) {
            if (timeRemaining > 0) {
                timeRemaining -= Time.deltaTime;
            }
            else {
                canvasObject.SetActive(false);
                timerIsRunning = false;
                timeRemaining = 5;
            }
        }
    }
}