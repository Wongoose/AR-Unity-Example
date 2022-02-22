// >>> Start by importing certain packages and classes
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]

// >>> Create 'spawnMultipleObjectsOnPlane' class (All your code for this feature will wrapped inside this class)
public class spawnMultipleObjectsOnPlane : MonoBehaviour
{
  // >>> Then, initialize a few variables before coding the logic
  private ARRaycastManager raycastManager;
  private GameObject spawnObject;

  // >>> ADDED CODE: This variable stores a 'list' of the multiple objects the user will place later
  private List<GameObject> placedPrefabList = new List<GameObject>();
  // <<<

  [SerializeField]
  // ADDED CODE: These variables below will allow you to set the 'maximum' number of objects that can be placed
  private int maxPrefabSpawnCount = 0;
  private int placedPrefabCount = 0;

  [SerializeField]
  private GameObject PlaceablePrefab;

  static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

  // >>> Next, create the 'Awake()' function (in CSharp, 'Awake()' will always be called first before any Start functions)
  private void Awake()
  {
    raycastManager = GetComponent<ARRaycastManager>();
  }

  // >>> Also, create the 'TryGetTouchPosition()' function that returns a boolean (boolean is either "true", or "false")
  bool TryGetTouchPosition(out Vector2 touchPosition)

  {
    // >>> NOTE: This "IF" is different from the previous
    // >>> This if statement will try to detect if a user touches the phone screen
    if (Input.GetTouch(0).phase == TouchPhase.Began)
    {
      touchPosition = Input.GetTouch(0).position;
      // >>> if a touch was detected, this function returns the value "true"
      return true;
    }
    else
    {
      // >>> Else, if no touch was detected, this function returns the value "false"
      touchPosition = default;
      return false;
    }
  }

  // >>> Then, create the 'Update()' function (this function will automatically be called everytime something changes)
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

      // >>> Then, check if the number of placed object exceeds the maximum number allowed (max number can be set in the code as previously mentioned above)
      if (placedPrefabCount < maxPrefabSpawnCount)
      {
        // >>> Creates a new spawnObject and displays it on the screen where the user touched
        spawnObject = Instantiate(PlaceablePrefab, hitPose.position, hitPose.rotation);
        placedPrefabList.Add(spawnObject);
        placedPrefabCount++;
      }
    }
  }

  public void SetPrefabType(GameObject prefabType)
  {
    PlaceablePrefab = prefabType;
  }
}
// END OF CODE
// You should be able to place MORE THAN ONE objects on the screen
