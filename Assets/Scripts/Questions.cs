using System.Collections;
using UnityEngine;
using TMPro;
using System;
using System.Xml.Linq;

public class Questions : ReadExcel
{
    [HideInInspector] public int lineIndex = 0;
    [HideInInspector] public bool inDialogue;
    [HideInInspector] public bool questionComplete;
    [SerializeField] TextMeshProUGUI quiestionText;
    [SerializeField] private GameObject buttonQuestion;
    [SerializeField] private TextAsset excelText;
    [SerializeField] int numRowExcel;
    [SerializeField] private float movementSpeed;
    [SerializeField] private Transform finalPositionInside, finalPositionExit;
    private SceneEffects sceneEffects;
    private float timeToChars = 0.04f;
    public float initialQuestionTime;
    public float currentQuestionTime;
    public float timeElapsed = 0;
    private bool changeTimeElapsed;
    public bool inGame;
    private bool  answerSectionInGame;
    [SerializeField] private PeopleController peopleController;



    private void Start()
    {
        sceneEffects = GetComponent<SceneEffects>();
        inGame = false;
        inDialogue = false;
        ReadExcelDialogues(excelText, numRowExcel);
        currentQuestionTime = initialQuestionTime;
        DeactivateButtonQuestion();
    }

    private void Update()
    {
        ModifyTimeQuestion();
        CallNullAnswers();
        ChangeTimeElpased();
        MovementInScene();
        MovementExitScene();


        if (finalPositionInside.position == transform.position && !inGame)
        {
            inGame = true;
            ActivateButtonQuestion();
        }

        if (ButtonsAnswer.numAnswers == (int)AnswerCounter.totalAnswer + 1 || currentQuestionTime <= 0)
        {
            answerSectionInGame = true;
        }
    }

    private void MovementInScene()
    {
        if (!inGame)
        {
            sceneEffects.MovementsInGame(finalPositionInside.position, movementSpeed);
        }
    }
    private void MovementExitScene()
    {
        if (answerSectionInGame)
        {
            sceneEffects.MovementsInGame(finalPositionExit.position, movementSpeed);
            DeactivateButtonQuestion();
        }
        
    }

    private void CallNullAnswers()
    {
        if (questionComplete)
        {
            timeElapsed += Time.deltaTime;

            if (timeElapsed > currentQuestionTime && !AnswerCounter.questionAnswered)
            {
                timeElapsed = 0;
                AnswerCounter.NullAnswer();
            }
            if (timeElapsed >= initialQuestionTime)
            {
                ActivateButtonQuestion();
                timeElapsed = 0;
            }
        }
    }
    private void ChangeTimeElpased()
    {
        if (AnswerCounter.questionAnswered && !changeTimeElapsed)
        {
            timeElapsed = initialQuestionTime * 3 / 4;
            changeTimeElapsed = true;
        }
    }
    public void ShowQuestionButton()
    {
        if (currentQuestionTime > 0)
        {
            StartDialogue(question, quiestionText);
            questionComplete = false;
            changeTimeElapsed = false;
            timeElapsed = 0;
            peopleController.Active();
            peopleController.Deactive();
        }
    }

    public void StartDialogue(string[] dialoguesChar, TextMeshProUGUI dialogueText)
    {

        if (lineIndex < dialoguesChar.Length && PeopleController.allowQuestion)
        {
            if (!inDialogue)
            {
                StartCoroutine(DialogueSystem(dialoguesChar, dialogueText));
            }
            else if (dialogueText.text == dialoguesChar[lineIndex])
            {
                NextDialogue(dialoguesChar, dialogueText);
            }
        }
    }

    private void NextDialogue(string[] dialoguesChar, TextMeshProUGUI dialogueText)
    {
            lineIndex++;

        if (lineIndex < dialoguesChar.Length)
        {
            Debug.Log("si2");
            StartCoroutine(DialogueSystem(dialoguesChar, dialogueText));
        }
    }

    private IEnumerator DialogueSystem(string[] dialoguesChar, TextMeshProUGUI dialogueText)
    {
        DeactivateButtonQuestion();
        dialogueText.text = string.Empty;
        inDialogue = true;
        foreach (char ch in dialoguesChar[lineIndex])
        {
            dialogueText.text += ch;
            yield return new WaitForSeconds(timeToChars);
        }
        questionComplete = true;
        
    }

    private void DeactivateButtonQuestion()
    {
        buttonQuestion.SetActive(false);

    }
    private void ActivateButtonQuestion()
    {
        buttonQuestion.SetActive(true);
    }

    private void ModifyTimeQuestion()
    {
        currentQuestionTime = initialQuestionTime * (1.0f - AnswerCounter.numIncorrectAnswers / 5.0f);
    }

    

}
