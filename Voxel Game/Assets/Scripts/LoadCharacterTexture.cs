using UnityEngine;
using System.Collections;

public class LoadCharacterTexture : MonoBehaviour {

    /* Load the skin from the user */
    public Texture skinRef;
    
    /* Set uv's for head */
    // Params: x, y, width, height
    // (0, 0, 0, 0) is bottom left corner of texture file -- Yeah, they're upside-down. WTF right?
    Rect uvsHeadFront = new Rect(0.125F, 0.875F, 0.125F, 0.125F);
    Rect uvsHeadBack = new Rect(0.375F, 0.875F, 0.125F, 0.125F);
    Rect uvsHeadLeft = new Rect(0.25F, 0.875F, 0.125F, 0.125F);
    Rect uvsHeadRight = new Rect(0.0F, 0.875F, 0.125F, 0.125F);
    Rect uvsHeadTop = new Rect(0.125F, 1.0F, 0.125F, 0.125F);
    Rect uvsHeadBot = new Rect(0.25F, 1.0F, 0.125F, 0.125F);

    /* Set uv's for torso */
    Rect uvsTorsoFront = new Rect(0.3125F, 0.6875F, 0.125F, 0.1875F);
    Rect uvsTorsoBack = new Rect(0.5F, 0.6875F, 0.125F, 0.1875F);
    Rect uvsTorsoLeft = new Rect(0.4375F, 0.6875F, 0.0625F, 0.1875F);
    Rect uvsTorsoRight = new Rect(0.25F, 0.6875F, 0.0625F, 0.1875F);
    Rect uvsTorsoTop = new Rect(0.3125F, 0.75F, 0.125F, 0.0625F);
    Rect uvsTorsoBot = new Rect(0.4375F, 0.75F, 0.125F, 0.0625F);

    /* Set uv's for arms */
    Rect uvsLeftArmFront = new Rect(0.5625F, 0.1875F, 0.0625F, 0.1875F);
    Rect uvsLeftArmBack = new Rect(0.6875F, 0.1875F, 0.0625F, 0.1875F);
    Rect uvsLeftArmLeft = new Rect(0.625F, 0.1875F, 0.0625F, 0.1875F);
    Rect uvsLeftArmRight = new Rect(0.5F, 0.1875F, 0.0625F, 0.1875F);
    Rect uvsLeftArmTop = new Rect(0.5625F, 0.25F, 0.0625F, 0.0625F);
    Rect uvsLeftArmBot = new Rect(0.625F, 0.25F, 0.0625F, 0.0625F);

    Rect uvsRightArmFront = new Rect(0.6875F, 0.6875F, 0.0625F, 0.1875F);
    Rect uvsRightArmBack = new Rect(0.8125F, 0.6875F, 0.0625F, 0.1875F);
    Rect uvsRightArmLeft = new Rect(0.75F, 0.6875F, 0.0625F, 0.1875F);
    Rect uvsRightArmRight = new Rect(0.625F, 0.6875F, 0.0625F, 0.1875F);
    Rect uvsRightArmTop = new Rect(0.6875F, 0.75F, 0.0625F, 0.0625F);
    Rect uvsRightArmBot = new Rect(0.75F, 0.75F, 0.0625F, 0.0625F);

    /* Set uv's for legs */
    Rect uvsLeftLegFront = new Rect(0.3125F, 0.1875F, 0.0625F, 0.1875F);
    Rect uvsLeftLegBack = new Rect(0.4375F, 0.1875F, 0.0625F, 0.1875F);
    Rect uvsLeftLegLeft = new Rect(0.375F, 0.1875F, 0.0625F, 0.1875F);
    Rect uvsLeftLegRight = new Rect(0.25F, 0.1875F, 0.0625F, 0.1875F);
    Rect uvsLeftLegTop = new Rect(0.3125F, 0.25F, 0.0625F, 0.0625F);
    Rect uvsLeftLegBot = new Rect(0.375F, 0.25F, 0.0625F, 0.0625F);

    Rect uvsRightLegFront = new Rect(0.0625F, 0.6875F, 0.0625F, 0.1875F);
    Rect uvsRightLegBack = new Rect(0.1875F, 0.6875F, 0.0625F, 0.1875F);
    Rect uvsRightLegLeft = new Rect(0.125F, 0.6875F, 0.0625F, 0.1875F);
    Rect uvsRightLegRight = new Rect(0.0F, 0.6875F, 0.0625F, 0.1875F);
    Rect uvsRightLegTop = new Rect(0.0625F, 0.75F, 0.0625F, 0.0625F);
    Rect uvsRightLegBot = new Rect(0.125F, 0.75F, 0.0625F, 0.0625F);

    void Start () {

        /* Skin the head  */
        GameObject headObject = this.transform.Find("Head").gameObject;
        Vector2[] uvHeadAtlas = assignUvs(headObject, uvsHeadFront, uvsHeadBack, uvsHeadLeft, uvsHeadRight, uvsHeadTop, uvsHeadBot);
        headObject.GetComponent<Renderer>().material.mainTexture = skinRef;
        headObject.GetComponent<MeshFilter>().mesh.uv = uvHeadAtlas;

        /* Skin the torso */
        GameObject torsoObject = this.transform.Find("Torso").gameObject;
        Vector2[] uvTorsoAtlas = assignUvs(torsoObject, uvsTorsoFront, uvsTorsoBack, uvsTorsoLeft, uvsTorsoRight, uvsTorsoTop, uvsTorsoBot);
        torsoObject.GetComponent<Renderer>().material.mainTexture = skinRef;
        torsoObject.GetComponent<MeshFilter>().mesh.uv = uvTorsoAtlas;

        /* Skin the left arm */
        GameObject leftArmObject = this.transform.FindChild("Left Arm").gameObject;
        Vector2[] uvLeftArmAtlas = assignUvs(leftArmObject, uvsLeftArmFront, uvsLeftArmBack, uvsLeftArmLeft, uvsLeftArmRight, uvsLeftArmTop, uvsLeftArmBot);
        leftArmObject.GetComponent<Renderer>().material.mainTexture = skinRef;
        leftArmObject.GetComponent<MeshFilter>().mesh.uv = uvLeftArmAtlas;

        /* Skin the right arm */
        GameObject rightArmObject = this.transform.FindChild("Right Arm").gameObject;
        Vector2[] uvRightArmAtlas = assignUvs(rightArmObject, uvsRightArmFront, uvsRightArmBack, uvsRightArmLeft, uvsRightArmRight, uvsRightArmTop, uvsRightArmBot);
        rightArmObject.GetComponent<Renderer>().material.mainTexture = skinRef;
        rightArmObject.GetComponent<MeshFilter>().mesh.uv = uvRightArmAtlas;

        /* Skin the left leg */
        GameObject leftLegObject = this.transform.FindChild("Left Leg").gameObject;
        Vector2[] uvLeftLegAtlas = assignUvs(leftLegObject, uvsLeftLegFront, uvsLeftLegBack, uvsLeftLegLeft, uvsLeftLegRight, uvsLeftLegTop, uvsLeftLegBot);
        leftLegObject.GetComponent<Renderer>().material.mainTexture = skinRef;
        leftLegObject.GetComponent<MeshFilter>().mesh.uv = uvLeftLegAtlas;


        /* Skin the right leg */
        GameObject rightLegObject = this.transform.FindChild("Right Leg").gameObject;
        Vector2[] uvRightLegAtlas = assignUvs(rightLegObject, uvsRightLegFront, uvsRightLegBack, uvsRightLegLeft, uvsRightLegRight, uvsRightLegTop, uvsRightLegBot);
        rightLegObject.GetComponent<Renderer>().material.mainTexture = skinRef;
        rightLegObject.GetComponent<MeshFilter>().mesh.uv = uvRightLegAtlas;

    }

    /* Fills a Vector2 Array with all proper cube vertex UV values */
    Vector2[] assignUvs(GameObject objectToSkin, Rect uvsFront, Rect uvsBack, Rect uvsLeft, Rect uvsRight, Rect uvsTop, Rect uvsBot)
    {
        Vector2[] uvTempAtlas = new Vector2[objectToSkin.transform.GetComponent<MeshFilter>().mesh.uv.Length];

        // FRONT    2    3    0    1
        uvTempAtlas[2] = new Vector2(uvsFront.x, uvsFront.y);
        uvTempAtlas[3] = new Vector2(uvsFront.x + uvsFront.width, uvsFront.y);
        uvTempAtlas[0] = new Vector2(uvsFront.x, uvsFront.y - uvsFront.height);
        uvTempAtlas[1] = new Vector2(uvsFront.x + uvsFront.width, uvsFront.y - uvsFront.height);

        // BACK    6    7   10   11
        uvTempAtlas[6] = new Vector2(uvsBack.x, uvsBack.y - uvsBack.height);
        uvTempAtlas[7] = new Vector2(uvsBack.x + uvsBack.width, uvsBack.y - uvsBack.height);
        uvTempAtlas[10] = new Vector2(uvsBack.x, uvsBack.y);
        uvTempAtlas[11] = new Vector2(uvsBack.x + uvsBack.width, uvsBack.y);

        // LEFT   19   17   16   18
        uvTempAtlas[19] = new Vector2(uvsLeft.x, uvsLeft.y);
        uvTempAtlas[17] = new Vector2(uvsLeft.x + uvsLeft.width, uvsLeft.y);
        uvTempAtlas[16] = new Vector2(uvsLeft.x, uvsLeft.y - uvsLeft.height);
        uvTempAtlas[18] = new Vector2(uvsLeft.x + uvsLeft.width, uvsLeft.y - uvsLeft.height);

        // RIGHT   23   21   20   22
        uvTempAtlas[23] = new Vector2(uvsRight.x, uvsRight.y);
        uvTempAtlas[21] = new Vector2(uvsRight.x + uvsRight.width, uvsRight.y);
        uvTempAtlas[20] = new Vector2(uvsRight.x, uvsRight.y - uvsRight.height);
        uvTempAtlas[22] = new Vector2(uvsRight.x + uvsRight.width, uvsRight.y - uvsRight.height);

        // TOP    4    5    8    9
        uvTempAtlas[4] = new Vector2(uvsTop.x, uvsTop.y);
        uvTempAtlas[5] = new Vector2(uvsTop.x + uvsTop.width, uvsTop.y);
        uvTempAtlas[8] = new Vector2(uvsTop.x, uvsTop.y - uvsTop.height);
        uvTempAtlas[9] = new Vector2(uvsTop.x + uvsTop.width, uvsTop.y - uvsHeadTop.height);

        // BOTTOM   15   13   12   14
        uvTempAtlas[15] = new Vector2(uvsBot.x, uvsBot.y);
        uvTempAtlas[13] = new Vector2(uvsBot.x + uvsBot.width, uvsBot.y);
        uvTempAtlas[12] = new Vector2(uvsBot.x, uvsBot.y - uvsBot.height);
        uvTempAtlas[14] = new Vector2(uvsBot.x + uvsBot.width, uvsBot.y - uvsBot.height);

        return uvTempAtlas;
    }   
}
