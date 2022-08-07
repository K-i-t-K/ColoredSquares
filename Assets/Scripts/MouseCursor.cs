using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor : MonoBehaviour
{
    private Color cursorColor = Color.black;
    private int collisionCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Hide default cursor and set circle to initial color
        Cursor.visible = false;
        gameObject.GetComponent<SpriteRenderer>().color = cursorColor;
    }

    // Update is called once per frame
    void Update()
    {
        // If default cursor whenever it shows
        if( Cursor.visible )
        {
            Cursor.visible = false;
        }
        // Lock circle to mouse position
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePos;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        // Count number of rects overlapping for color mixing
        collisionCount++;
        ColorMixing(true, col.gameObject.GetComponent<SpriteRenderer>().color);

        // Set circle color
        gameObject.GetComponent<SpriteRenderer>().color = cursorColor;
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        // Count number of rects overlapping for color mixing
        collisionCount--;
        if( collisionCount <= 0)
        {
            cursorColor = Color.black;
        }
        else
        {
            ColorMixing(false, col.gameObject.GetComponent<SpriteRenderer>().color);
        }
        // Set circle color
        gameObject.GetComponent<SpriteRenderer>().color = cursorColor;
    }

    private void ColorMixing(bool addition, Color otherCol)
    {
        // Calculates the new color
        Color newCol = otherCol * 255;
        Color oldCol = cursorColor * 255;
        Vector4 result;
        if( addition )
        {
            result = ((oldCol * oldCol) + (newCol * newCol)) / Mathf.Max(collisionCount - 1, 1);
        }
        else
        {
            result = ((oldCol * oldCol) - (newCol * newCol)) / Mathf.Max(collisionCount - 1, 1);
        }

        cursorColor = new Color(Mathf.Sqrt(result.x) / 255, Mathf.Sqrt(result.y) / 255, Mathf.Sqrt(result.z) / 255, 1.0f);
    }
}
