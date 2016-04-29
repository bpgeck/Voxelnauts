using UnityEngine;
using System.Collections;

public class SetFlagColor : MonoBehaviour
{
    ArrayList thingsToColor = new ArrayList();
    Color color;

    // Use this for initialization
    void Start()
    {
        if (this.gameObject.name.Contains("Flag"))
        {
            thingsToColor.Add(this.transform.Find("Flag").gameObject.GetComponent<Renderer>().material);
            thingsToColor.Add(this.transform.Find("Pole").gameObject.GetComponent<Renderer>().material);
        }

        /* change the color according to team tag */
        if (this.gameObject.tag.Contains("Cerulean"))
        {
            float r = 0f / 255;
            float g = 123f / 255;
            float b = 167f / 255;
            color = new Color(r, g, b);
        }
        else if (this.gameObject.tag.Contains("Burgundy"))
        {
            float r = 125f / 255;
            float g = 0f / 255;
            float b = 29f / 255;
            color = new Color(r, g, b);
        }

        /* apply that color to all the appropriate materials */
        for (int i = 0; i < thingsToColor.Count; i++)
        {
            ((Material)thingsToColor[i]).SetColor("_Color", color);
        }
    }
}
