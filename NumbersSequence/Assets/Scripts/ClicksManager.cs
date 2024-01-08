using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickHandler : MonoBehaviour
{
    private void Update()
    {
        // Check for mouse click
        if (Input.GetMouseButtonDown(0))
        {
            // Raycast to check if the mouse click hits this object
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Check if the hit object is this GameObject
                if (hit.collider.gameObject == gameObject)
                {
                    // Print the name of the clicked object to the console
                    Debug.Log("Clicked on: " + gameObject.name);
                }
            }
        }
    }
}
