using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void ToBaselineUser() {
        SceneManager.LoadScene("BaselineUser");
    }

    public void ToBaselineAttacker() {
        SceneManager.LoadScene("BaselineAttacker");
    }

    public void ToSpatialAudioUser() {
        SceneManager.LoadScene("SpatialAudioUser");
    }

    public void ToSpatialAudioAttacker() {
        SceneManager.LoadScene("SpatialAudioAttacker");
    }

    public void ToDisplayUser() {
        SceneManager.LoadScene("DisplayUser");
    }

    public void ToDisplayAttacker() {
        SceneManager.LoadScene("DisplayAttacker");
    }

    public void ToARUser() {
        SceneManager.LoadScene("ARUser");
    }

    public void ToARAttacker() {
        SceneManager.LoadScene("ARAttacker");
    }

    public void ToMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }        
}

