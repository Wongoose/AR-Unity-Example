﻿// >>> Start by importing certain packages and classes
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]

// >>> Create 'spawnObjectsOnPlane' class (All your code for this feature will wrapped inside this class)
public class spawnObjectsOnPlane : MonoBehaviour
{
  // >>> Then, initialize a few variables before coding the logic
  private ARRaycastManager raycastManager;
  private GameObject spawnObject;

  [SerializeField]
  private GameObject placeablePrefab;

  static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();
  // <<<

  // >>> Next, create the 'Awake()' function (in CSharp, 'Awake()' will always be called first before any Start functions)
  private void Awake()
  {
    raycastManager = GetComponent<ARRaycastManager>();
  }

  // >>> Also, create the 'TryGetTouchPosition()' function that returns a boolean (boolean is either "true", or "false")
  bool TryGetTouchPosition(out Vector2 touchPosition)

  {
    // >>> This if statement will try to detect if a user touches the phone screen
    if (Input.touchCount > 0)
    {
      // >>> if a touch was detected, this function returns the value "true"
      touchPosition = Input.GetTouch(0).position;
      return true;
    }
    else
    {
      // >>> Else, if no touch was detected, this function returns the value "false"
      touchPosition = default;
      return false;
    }
  }

  // >>> Lastly, create the 'Update()' function (this function will automatically be called everytime something changes)
  private void Update()
  {
    if (!TryGetTouchPosition(out Vector2 touchPosition))
    {
      return;
    }

    // >>> Also checks if the user touched on a valid plane/surface
    if (raycastManager.Raycast(touchPosition, s_Hits, TrackableType.PlaneWithinPolygon))
    {
      var hitPose = s_Hits[0].pose;

      if (spawnObject == null)
      {
        // >>> If the user have not placed an object before, this code will run
        spawnObject = Instantiate(placeablePrefab, hitPose.position, hitPose.rotation);

      }
      else
      {
        // >>> Else, just change the 'position' and 'rotation' of the "spawnedObject"
        spawnObject.transform.position = hitPose.position;
        spawnObject.transform.rotation = hitPose.rotation;
      }
    }
  }
}
// END OF CODE
// You should be able to place ONE object on the screen
