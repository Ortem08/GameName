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
            buttonText.GetComponentInChildren<TextMeshProUGUI>().text = "�������";

        if (currentTutorMessageIndex >= tutorialMessages.Count)
        {
            IsTutorialButton = false;
            Destroy(textBox);
        }
    }
}
