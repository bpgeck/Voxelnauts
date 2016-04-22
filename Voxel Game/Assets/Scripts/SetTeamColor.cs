using UnityEngine;
using System.Collections;

public class SetTeamColor : MonoBehaviour {
    ArrayList thingsToColor = new ArrayList();
    Color color;

    // Use this for initialization
    void Start()
    {
        /* fill thingsToColor with all the gameobjects that need to be colored */
        if (this.gameObject.name.Contains("Flag"))
        {
            thingsToColor.Add(this.transform.Find("Flag").gameObject.GetComponent<Renderer>().material);
            thingsToColor.Add(this.transform.Find("Pole").gameObject.GetComponent<Renderer>().material);
        }
        else if (this.gameObject.name.Contains("Geck"))
        {
            thingsToColor.Add(this.transform.Find("geckstronautAnimatedWithGun/SpaceAR:SpaceAR:Mesh").GetComponent<Renderer>().materials[0]);
            thingsToColor.Add(this.transform.Find("geckstronautAnimatedWithGun/geckstronaut:skinned_GEO_GRP/geckstronaut:headBodySkinned").GetComponent<Renderer>().materials[3]);
            thingsToColor.Add(this.transform.Find("geckstronautAnimatedWithGun/geckstronaut:skinned_GEO_GRP/geckstronaut:headBodySkinned").GetComponent<Renderer>().materials[0]);
        }
        else
        {
            Debug.Log("I don't regognize that name to color");
            return;
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
