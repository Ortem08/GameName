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
    public bool IsTutorialButton;
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

        var buttCoord = buttonText.GetComponent<Transform>().position;
        Debug.Log((buttCoord, Input.mousePosition));
        var buttSize = buttonText.GetComponent<RectTransform>().rect;
        var mouse = Input.mousePosition;
        IsTutorialButton = mouse.x < buttCoord.x + buttSize.width / 2 && mouse.x > buttCoord.x - buttSize.width / 2
            && mouse.y < buttCoord.y + buttSize.height / 2 && mouse.y > buttCoord.y - buttSize.height / 2;

        var text = textBox.GetComponentInChildren<TextMeshProUGUI>();
        text.text = tutorialMessages[currentTutorMessageIndex];
        currentTutorMessageIndex++;

        if (currentTutorMessageIndex == tutorialMessages.Count - 1)
            buttonText.GetComponentInChildren<TextMeshProUGUI>().text = "Закрыть";

        if (currentTutorMessageIndex >= tutorialMessages.Count)
        {
            IsTutorialButton = false;
            Destroy(textBox);
        }
    }
}
