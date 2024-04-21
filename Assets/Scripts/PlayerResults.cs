using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerResults : SceneEffects
{
    [SerializeField] private Questions questions;
    [SerializeField] private Transform positionSoon, positionWife, positionFather;
    [SerializeField] private GameObject soon, positionSoonGO, wife, positionWifeGO, father, positionFatherGO, textFinal;
    private void Update()
    {
        if (AnswerCounter.totalAnswer == 9)
        {
            PositionsPeopleToFinal();
        }
        else if (questions.currentQuestionTime <= 0 && questions.inGame)
        {
            PositionsPeopleToFinal();
        }
    }
    private void PositionsPeopleToFinal()
    {
        positionFatherGO.SetActive(true);
        positionSoonGO.SetActive(true);
        positionWifeGO.SetActive(true);
        soon.SetActive(true);
        wife.SetActive(true);
        father.SetActive(true);
        textFinal.SetActive(true);
        soon.transform.position = positionSoon.position;
        wife.transform.position = positionWife.position;
        father.transform.position = positionFather.position;
    }

}
