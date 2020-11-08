using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ClientManager : MonoBehaviour
{
    public Client[] allClients;
    public Client[] tier2Clients;
    public Client currentClient;

    private Queue<Client> clients = new Queue<Client>();
    private Cat cat;

    private void Start()
    {
        cat = FindObjectOfType<Cat>();
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
            cat.Count++;
            currentClient.MakeGood();
            if (currentClient.isBoss)
            {
                GlobalVariables.instance.tier++;
                FillQueue();
            }
        }
        else
        {
            cat.Count--;
            if (currentClient.isBoss)
                FillQueue();
            DialogueManager.Internal.StartLose();
        }
    }

    bool CheckDish(string[] reqFeatures, Dish dish)
    {
        return reqFeatures.Count(f => dish.features.Contains(f)) == reqFeatures.Length;

    }

    void FillQueue()
    {
        var _clients = allClients;
        if (GlobalVariables.instance.tier == 2)
            _clients = tier2Clients;

        foreach (var client in _clients)
        {
            clients.Enqueue(client);
        }
    }

    public void NextClient()
    {
        if (currentClient)
        {
            var monoName = currentClient.NextMonologue;
            if (currentClient.isBoss)
                AudioManager.instance.StartClip(1);
            Destroy(currentClient.gameObject);
            if (monoName != "")
            {
                DialogueManager.Internal.MonologueStart(monoName);
                FindObjectOfType<Conveyer>().ClearConveyer();
                return;
            }
        }
        currentClient = Instantiate(clients.Dequeue(), transform);
        if (currentClient.isBoss)
            AudioManager.instance.StartClip(2);
        StartOrder();
    }
}
