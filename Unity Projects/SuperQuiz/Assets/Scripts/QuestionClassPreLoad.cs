using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionPreLoad{

    public int index;
    public int questionType;
    public string text;
    public string answer0;  // Right answer
    public string answer1;
    public string answer2;
    public string answer3;
    public string answer4;
    public string userGroupString;
    public List<bool> userGroup;
    public string subject;

    public QuestionPreLoad(){
        index = 0;
        questionType = 0;
        text = "";
        answer0 = "";
        answer1 = "";
        answer2 = "";
        answer3 = "";
        answer4 = "";
        userGroupString = "";
        userGroup = new List<bool>();
        subject = "";
    }

    public QuestionPreLoad(int newIndex, int newQuestionType, string newT, string newA0, string newA1, string newA2, string newA3, string newA4, string newUserGroup, string newSubject)    {
        index = newIndex;
        questionType = newQuestionType;
        text = newT;
        answer0 = newA0;
        answer1 = newA1;
        answer2 = newA2;
        answer3 = newA3;
        answer4 = newA4;
        userGroupString = newUserGroup;
        subject = newSubject;
        userGroup = new List<bool>();
        if (userGroupString != null && userGroupString != "X"){
            if (userGroupString != ""){
                string[] questionUserGroups;
                string[] space = new string[] { " " };
                questionUserGroups = userGroupString.Split(space, System.StringSplitOptions.None);
                for (int i = 0; i < questionUserGroups.Length; i++){
                    if (questionUserGroups[i] == "T"){
                        userGroup.Add(true);
                    }
                    if (questionUserGroups[i] == "F"){
                        userGroup.Add(false);
                    }
                }
            }
        }
    }

    // public Question (Question baseQuestion){
    // index = baseQuestion.index;
    // questionType = baseQuestion.questionType;
    // text = baseQuestion.text;
    // answer0 = baseQuestion.answer0;
    // answer1 = baseQuestion.answer1;
    // answer2 = baseQuestion.answer2;
    // answer3 = baseQuestion.answer3;
    // answer4 = baseQuestion.answer4;
    // }
}

