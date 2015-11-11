using UnityEngine;
using UnityEngine.Networking; // I use the networking namespace in adition to normal engine sutff

public class NetworkingPlayerSetup : NetworkBehaviour { // Notive I inherit from NetWorkBehavior rather than the usual thing

    [SerializeField] // This makese componentsToDisable fillable from the Unity GUI rather than only from this script
    Behaviour[] componentsToDisable;
    
	void Start () {
        /* If we are not controlling this player (i.e. if it is beign controlled by another user),
            then we want to disable things like movement and sound reception, that way the things
            user controls do not interfere with other players */
	    if (!isLocalPlayer)
        {
            for (int i = 0; i < componentsToDisable.Length; i++)
            {
                componentsToDisable[i].enabled = false;
            }
        }    
	}
}
