﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ClientManager : MonoBehaviour
{
    public Client[] allClients;
    public Client currentClient;

    private Queue<Client> clients = new Queue<Client>();

    private void Start()
    {
        FillQueue();
    }
    public void StartOrder()
    {
        FindObjectOfType<Conveyer>().StartCooking(currentClient);
        var reqFeats = currentClient.requiredFeaturesForFirst.Concat(currentClient.requiredFeaturesForSecond).Concat(currentClient.requiredFeaturesForDrink).ToArray();
        DialogueManager.Internal.DialogueStart(currentClient.DialogueName, reqFeats);
    }

    public void CompleteOrder(Dish first, Dish second, Dish drink)
    {
        var result = CheckDish(currentClient.requiredFeaturesForFirst, first)
            && CheckDish(currentClient.requiredFeaturesForSecond, second)
             && CheckDish(currentClient.requiredFeaturesForDrink, drink);

        if (result)
        {
            first.AddVisibleFeatures(currentClient.requiredFeaturesForFirst);
            second.AddVisibleFeatures(currentClient.requiredFeaturesForSecond);
            drink.AddVisibleFeatures(currentClient.requiredFeaturesForDrink);
            DialogueManager.Internal.StartWin();
        }
        Debug.Log(result);
        DialogueManager.Internal.StartLose();
    }

    bool CheckDish(string[] reqFeatures, Dish dish)
    {
        return reqFeatures.Count(f => dish.features.Contains(f)) == reqFeatures.Length;

    }

    void FillQueue()
    {
        foreach (var client in allClients)
        {
            clients.Enqueue(client);
        }
    }

    public void NextClient()
    {
        if (currentClient) Destroy(currentClient);
        currentClient = Instantiate(clients.Dequeue(), transform);
        StartOrder();
    }
}
