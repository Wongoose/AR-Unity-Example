// >>> Start by importing certain packages and classes
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;


[RequireComponent(typeof(ARPlaneManager))]

// >>> Create 'PlaneDetectionToggle' class below (All your code for this feature will wrapped inside this class)
public class PlaneDetectionToggle : MonoBehaviour
{
  // >>> Then, initialize a few variables before coding the logic
  private ARPlaneManager planeManager;
  [SerializeField]
  private Text toggleButtonText;

  // >>> Next, create the 'Awake()' function (in CSharp, 'Awake()' will always be called first before any Start functions)
  private void Awake()
  {
    planeManager = GetComponent<ARPlaneManager>();
    // >>> When the program starts, set the button text to "Disable"
    toggleButtonText.text = "Disable";
  }

  // >>> Also, create the 'TogglePlaneDetection()' function (This function will run when the button is clicked)
  // >>> This function will show or hide the planes
  public void TogglePlaneDetection()
  {
    planeManager.enabled = !planeManager.enabled;
    string toggleButtonMessage = "";

    // >>> After toggled, change the text in the button to either "Disable" or "Enable"
    if (planeManager.enabled)
    {
      toggleButtonMessage = "Disable";
      SetAllPlanesActive(true);
    }
    else
    {
      toggleButtonMessage = "Enable";
      SetAllPlanesActive(false);
    }

    toggleButtonText.text = toggleButtonMessage;
  }

  private void SetAllPlanesActive(bool value)
  {
    // >>> Loops through each plane and either show or hide them based on "bool value"
    foreach (var plane in planeManager.trackables)
    {
      plane.gameObject.SetActive(value);
    }
  }
}
