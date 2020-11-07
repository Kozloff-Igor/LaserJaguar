using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientManager : MonoBehaviour
{
    public Client currentClient;

    public void StartOrder()
    {
        FindObjectOfType<Conveyer>().StartCooking(currentClient);
        DialogueManager.Internal.DialogueStart("Example", currentClient.requiredFeaturesForFirst);
    }
}
