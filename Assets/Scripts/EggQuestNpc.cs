using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class EggQuestNPC : MonoBehaviour
{
    public NPCConversation needEggsConversation;         // Di�logo cuando faltan huevos
    public NPCConversation eggsCollectedConversation;    // Di�logo cuando tienes los huevos
    public EggCollector playerEggCollector;              // Referencia al jugador que recoge huevos

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (playerEggCollector.GetEggCount() >= 6)
            {
                ConversationManager.Instance.StartConversation(eggsCollectedConversation);
            }
            else
            {
                ConversationManager.Instance.StartConversation(needEggsConversation);
            }
        }
    }
}