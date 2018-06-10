using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    float diagonalRatio = 1 / Mathf.Sqrt(2);

    public float movementSpeed = 1.0f;
    public float rotationSpeed = 1.0f;

    float testVertical;
    float testHorizontal;
    float testMouseX;
    bool testMouseRight;

    GUIStyle style;

    private void Start()
    {
        style = new GUIStyle();
        style.normal.textColor = Color.black;
    }

    // Update is called once per frame
    void Update () {
        var vertical = Input.GetAxisRaw("Vertical");
        var horizontal = Input.GetAxisRaw("Horizontal");
        var mouseX = Input.GetAxisRaw("Mouse X");
        var mouseRight= Input.GetMouseButton(1);

        float verticalMove = 0.0f;
        float horizontalMove = 0.0f;
        float rotation = 0.0f;

        testVertical = vertical;
        testHorizontal = horizontal;
        testMouseX = mouseX;
        testMouseRight = mouseRight;

        // Vertical movement
        verticalMove = vertical;
        if (horizontal != 0 && mouseRight) verticalMove *= diagonalRatio;

        // Horizontal movement
        if (vertical == 0 && mouseRight) horizontalMove = horizontal;
        if (vertical != 0 && mouseRight) horizontalMove = horizontal * diagonalRatio;

        // Rotation
        if (mouseRight) rotation = -Mathf.Sign(mouseX) * Mathf.Abs(mouseX * Mathf.Sqrt(Mathf.Abs(mouseX)));
        if (!mouseRight) rotation = -horizontal;

        // Adding multipliers
        verticalMove *= movementSpeed * Time.deltaTime;
        horizontalMove *= movementSpeed * Time.deltaTime;
        rotation *= rotationSpeed * Time.deltaTime;

        // Moving and rotating the character
        transform.Translate(horizontalMove, verticalMove, 0.0f);
        transform.Rotate(0.0f, 0.0f, rotation);
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("OnCollisionEnter2D");
        foreach (var e in col.contacts)
        {
            Debug.Log(e.normal);
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        //Debug.Log("OnCollisionStay2D");
    }

    void OnCollisionExit2D(Collision2D col)
    {
        Debug.Log("OnCollisionExit2D");
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 10), "Vertical:     " + testVertical, style);
        GUI.Label(new Rect(10, 25, 100, 10), "Horizontal:   " + testHorizontal, style);
        GUI.Label(new Rect(10, 40, 100, 10), "MouseX:       " + testMouseX, style);
        GUI.Label(new Rect(10, 55, 100, 10), "MouseRight:  " + testMouseRight, style);
    }
}
