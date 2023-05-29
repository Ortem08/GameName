using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PressedButtonActivate : MonoBehaviour
{
    public int Id;
    public bool IsTutorialButton;

    private Inventory inventory;
    private List<string> tutorialMessages;
    private int currentTutorMessageIndex;

    private GameObject TextBox { get; set; }

    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        tutorialMessages = new List<string>() 
        { 
            "�������� - ����� ������ ����", 
            "��������� ������� - F", 
            "������������ ������� - Q, W, E ��������������", 
            "������� ������� �������� - ������ ������ ����", 
            "��� ����, ����� ������ ��������� �������, ����������� ������, ���������� � ���� �������� � �������� ���� ������ �� ��� � ��������� ��� �� �� ����", 
            ""
        };
        TextBox = GameObject.FindGameObjectWithTag("TutorialMessage");
        TextBox.GetComponentInChildren<TextMeshProUGUI>().text = tutorialMessages[0];
        currentTutorMessageIndex = 1;
    }
    
    public void DropItem()
    {
        inventory.isFull[Id] = false;
        if (transform.childCount > 0)
            transform.GetChild(0).GetComponent<Spawn>().SpawnDroppedItem();
    }

    public void GiveNextTutorialMessage()
    {
        var buttonText = TextBox.GetComponentInChildren<Button>();

        var text = TextBox.GetComponentInChildren<TextMeshProUGUI>();
        text.text = tutorialMessages[currentTutorMessageIndex];
        currentTutorMessageIndex++;

        if (currentTutorMessageIndex == tutorialMessages.Count - 1)
            buttonText.GetComponentInChildren<TextMeshProUGUI>().text = "�������";

        if (currentTutorMessageIndex >= tutorialMessages.Count)
        {
            IsTutorialButton = false;
            Destroy(TextBox);
        }
    }
}
