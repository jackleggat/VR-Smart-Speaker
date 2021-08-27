using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;


public class PlaySpeakerAndAttacker : MonoBehaviour
{

    public AudioSource attackerSound;
    public AudioSource alexaSound;
    private InputDevice targetDevice;
    private bool timerIsRunning = false;

    // Start is called before the first frame update
    void Start()
    {
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
        alexaSound.Play();
        timerIsRunning = false;
    }
}
