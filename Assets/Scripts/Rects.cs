using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rects : MonoBehaviour
{
    public Color rectColor = Color.white;

    private SpriteRenderer sRenderer;
    private bool collision = false;
    private float counter = 0.0f;
    private float threshold = 3.0f;


    // Start is called before the first frame update
    void Start()
    {
        sRenderer = GetComponent<SpriteRenderer>();
        sRenderer.color = rectColor;
    }

    // Update is called once per frame
    void Update()
    {
        // Start timer for circle hovering over rectangles
        if( collision )
        {
            counter += Time.deltaTime;
            if ( counter >= threshold )
            {
                collision = false;
                counter = 0.0f;
                SetColors();

            }
        }
    }

    private void SetColors()
    {
        // Finds all other rectangles of the same color through tags
        GameObject[] others = GameObject.FindGameObjectsWithTag(gameObject.tag);
        string newTag = "";

        // Generates a new color that's different from the existing one
        do
        {
            newTag = GenerateColor();

        } while (newTag == gameObject.tag);

        switch( newTag )
        {
            case "Red":
                rectColor = Color.red;
                break;
            case "Green":
                rectColor = Color.green;
                break;
            case "Blue":
                rectColor = Color.blue;
                break;
            default:
                rectColor = Color.blue;
                break;
        }

        // If there are other rectangles of the same color, set their new color and tag
        if( others.Length > 0 )
        {
            foreach( GameObject obj in others)
            {
                obj.tag = newTag;
                obj.GetComponent<SpriteRenderer>().color = rectColor;
            }
        }
        // Set rectangle with new color and tag
        gameObject.tag = newTag;
        sRenderer.color = rectColor;
    }

    private string GenerateColor()
    {
        string newTag = "";
        switch (Random.Range(0, 2))
        {
            case 0:
                newTag = "Red";
                break;
            case 1:
                newTag = "Green";
                break;
            case 2:
                newTag = "Blue";
                break;
            default:
                newTag = "Blue";
                break;
        }

        return newTag;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        collision = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        collision = false;
        counter = 0.0f;
    }
}
