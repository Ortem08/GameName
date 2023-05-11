using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PressedButtonActivate : MonoBehaviour
{
    private Inventory _inventory;
    public int Id;
    
    private List<string> tutorialMessages;
    private int currentTutorMessageIndex;

    void Start()
    {
        _inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        tutorialMessages = new List<string>() 
        { 
            "Для передвижения используйте левую кнопку мыши", 
            "Чтобы подабрать предмет, подойдите к нему достаточно близко и нажмите кнопку F", 
            "Чтобы использовать предметы в первой, второй и третьей ячейках нажимайте клавиши Q, W, E соответственно", 
            "Для некоторых предметов, после их использования, вам понадобится указать область действия с помощью правой кнопки мыши", 
            "Для того, чтобы пройти обучающий уровень, используйте знания, полученные в этом обучении и нанесите урон одному из нпс и уменьшите его хп до нуля", 
            ""
        };
        currentTutorMessageIndex = 0;
    }
    
    public void DropItem()
    {
        _inventory.isFull[Id] = false;
        if (transform.childCount > 0)
            transform.GetChild(0).GetComponent<Spawn>().SpawnDroppedItem();
    }

    public void GiveNextTutorialMessage()
    {
        var textBox = GameObject.FindGameObjectWithTag("TutorialMessage");
        var buttonText = textBox.GetComponentInChildren<Button>();
        var text = textBox.GetComponentInChildren<TextMeshProUGUI>();
        text.text = tutorialMessages[currentTutorMessageIndex];
        currentTutorMessageIndex++;
        if (currentTutorMessageIndex == tutorialMessages.Count - 1)
        {
            buttonText.GetComponentInChildren<TextMeshProUGUI>().text = "Закрыть";
        }
        if (currentTutorMessageIndex >= tutorialMessages.Count)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>().TutorialFinished = true;
            Destroy(textBox);
        }
    }
}
