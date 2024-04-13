using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerCounter : ButtonsAnswer
{
    public static float numCorrecctAnswers;
    public static float numIncorrectAnswers;
    public static bool questionAnswered;
    public static int totalAnswer;

    private void Update()
    {
        totalAnswer = (int) (numCorrecctAnswers + numIncorrectAnswers);
        Debug.Log(totalAnswer);
    }
    public void CorrectAnswer()
    {
        if (!questionAnswered)
        {
            if (gameObject.tag == "CorrectAnswer")
            {
                numCorrecctAnswers++;
            }
            questionAnswered = true;
        }
    }
    public void IncorrectAnswer()
    {
        if (!questionAnswered)
        {
            if (gameObject.tag == "IncorrectAnswer")
            {
                numIncorrectAnswers++;
            }
            questionAnswered = true;
        }
        
    }
    public static void NullAnswer()
    {
        if (!questionAnswered)
        {
            numIncorrectAnswers++;
            questionAnswered = true;
        }
    }


}