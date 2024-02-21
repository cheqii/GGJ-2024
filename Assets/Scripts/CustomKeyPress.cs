using UnityEngine;
using UnityEngine.Events;

public class CustomKeyPress : MonoBehaviour
{
    public KeyCode customKey = KeyCode.Space;
    public UnityEvent onCustomKeyPress;

    // Update is called once per frame
    void Update()
    {
        // Check if the custom key is pressed
        if (Input.GetKeyDown(customKey))
        {
            // Invoke the UnityEvent when the key is pressed
            onCustomKeyPress.Invoke();
        }
    }
}