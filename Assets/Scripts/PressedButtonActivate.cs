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
            "��� ������������ ����������� ����� ������ ����", 
            "����� ��������� �������, ��������� � ���� ���������� ������ � ������� ������ F", 
            "����� ������������ �������� � ������, ������ � ������� ������� ��������� ������� Q, W, E ��������������", 
            "��� ��������� ���������, ����� �� �������������, ��� ����������� ������� ������� �������� � ������� ������ ������ ����", 
            "��� ����, ����� ������ ��������� �������, ����������� ������, ���������� � ���� �������� � �������� ���� ������ �� ��� � ��������� ��� �� �� ����", 
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
            buttonText.GetComponentInChildren<TextMeshProUGUI>().text = "�������";
        }
        if (currentTutorMessageIndex >= tutorialMessages.Count)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>().TutorialFinished = true;
            Destroy(textBox);
        }
    }
}
