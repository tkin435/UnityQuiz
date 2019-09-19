﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Quiz : MonoBehaviour {
    [Serializable]
    public class QuestionPanelSet {
        public RectTransform text;
        public RectTransform image;
        public RectTransform video;
    }

    public static Quiz instance;

    public QuestionPanelSet questionPanels;
    public Question[] questions;
    public RectTransform pnlIntroduction;
    public RectTransform pnlQuestion;
    public RectTransform pnlAnswerButtons;
    public FinishScreen finishScreen;
    public Button btnStart;
    public Button[] answerButtons;
    public Button btnBack;
    public Text txtQuestion;
    public Text txtQuestionCount;
    public bool isAnswered { get; set; }

    public enum QuestionType {
        Text,
        Image,
        Video
    }

    public QuestionType type;

    public int progress    { get; set; }
    public int answerCount { get; set; }

    private void Awake() {
        instance = this;

        StaticMethods.AssignButtonAction(btnStart, StartQuiz);
        answerButtons = pnlAnswerButtons.GetComponentsInChildren<Button>(true);
    }

    void StartQuiz() {
        StartCoroutine(AnswerFlow());
    }

    private bool isAwaitingResponse;

    public IEnumerator AnswerFlow() {
        //Run through the array of questions
        //If one question is answerd, move on 

        for (int i = 0; i < questions.Length; i++) {
            questions[i].AskQuestion();

            while (!isAnswered) {
                yield return null;
            }

            isAnswered = false;
        }

        EndQuiz();
    }

    public void EndQuiz() {
        //Check pls"
        gameObject.SetActive(false);
        finishScreen.gameObject.SetActive(true);
        Text _finishText = finishScreen.GetComponentInChildren<Text>();

        Debug.Log("Fin");
        if (answerCount == questions.Length) {
            _finishText.text = "win";
            Debug.Log("Win");
        } else {
            _finishText.text = "Lose";
            Debug.Log("Lose");
        }
    }
}
