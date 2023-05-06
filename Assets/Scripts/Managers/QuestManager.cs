using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    [Header("Quest")]
    [SerializeField] private QuestSO _questData;

    
    
    [Header("Actor GO")] 
    [SerializeField] private TextMeshProUGUI _actorNameTxt;
    [SerializeField] private Image _actorImg;
    
    [Header("Other GO")]
    [SerializeField] private Button _nextBtn;
    [SerializeField] private Button _choice1Btn;
    [SerializeField] private Button _choice2Btn;
    [SerializeField] private Button _quitButtonWin;
    [SerializeField] private Button _quitButtonLose;
    [SerializeField] private Button _restartButton;


    [SerializeField] private TextMeshProUGUI _contentTxt;
    [SerializeField] private TextMeshProUGUI _questionContentTxt;
    [SerializeField] private TextMeshProUGUI _choice1Txt;
    [SerializeField] private TextMeshProUGUI _choice2Txt;

    [SerializeField] private GameObject _chatGO;
    [SerializeField] private GameObject _questionGO;
    [SerializeField] private GameObject _questGO;
    [SerializeField] private GameObject _winGO;
    [SerializeField] private GameObject _loseGo;
    
    
    
    [SerializeField] private Slider _slider;
    
    private int _currentActionIndex;
    private int _score;
   
    private void Awake()
    {
        _nextBtn.onClick.RemoveAllListeners();
        _nextBtn.onClick.AddListener(NextAction);
        _score = _questData.InitialScore;
        
        _quitButtonLose.onClick.RemoveAllListeners();
        _quitButtonLose.onClick.AddListener(Quit);
        
        _quitButtonWin.onClick.RemoveAllListeners();
        _quitButtonWin.onClick.AddListener(Quit);

        _restartButton.onClick.RemoveAllListeners();
        _restartButton.onClick.AddListener(Restart);
        
    }

    public void StartExecute()
    {
        _questGO.SetActive(true);
        _currentActionIndex = 0;
        ExecuteContent();
    }

    private void Quit()
    {
        SceneManager.LoadSceneAsync(StaticVar.MAINMENUSCENE, LoadSceneMode.Additive);
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    

    private void ExecuteContent()
    {
        _actorImg.sprite = _questData.ActionList[_currentActionIndex].Actor.Photo;
        _actorNameTxt.SetText(_questData.ActionList[_currentActionIndex].Actor.Name);
        
        switch (_questData.ActionList[_currentActionIndex].Type)
        {
            case ActionType.Chat:
                Chat(_questData.ActionList[_currentActionIndex].Content);
                break;
            case ActionType.Question:
                Question(_questData.ActionList[_currentActionIndex]);
                break;
        }
    }

    private void Chat(string content)
    {
        _chatGO.SetActive(true);
        _questionGO.SetActive(false);
        
        _contentTxt.SetText(content);
        
    }

    private void Question(ActionSO data)
    {
        _chatGO.SetActive(false);
        _questionGO.SetActive(true);
        
        _questionContentTxt.SetText(data.QuestionContent);
        _choice1Txt.SetText(data.ChoiceContent[0].Content);
        _choice2Txt.SetText(data.ChoiceContent[1].Content);
        
        _choice1Btn.onClick.RemoveAllListeners();
        _choice1Btn.onClick.AddListener(()=>OnAnswerClicked(0));

        _choice2Btn.onClick.RemoveAllListeners();
        _choice2Btn.onClick.AddListener(()=>OnAnswerClicked(1));
        
    }

    private void OnAnswerClicked(int choice)
    {
        Chat(_questData.ActionList[_currentActionIndex].ChoiceContent[choice].QuestionReaction);
        UpdateScore(_questData.ActionList[_currentActionIndex].ChoiceContent[choice].Score);
    }

    private void UpdateScore(int score)
    {
        _score += score;
        _slider.value = _score;
    }

    private void NextAction()
    {
        _currentActionIndex += 1;
        if (_currentActionIndex < _questData.ActionList.Length)
        {
            ExecuteContent();
        }
        else
        {
            if (_score < _questData.MinimumScore)
            {
                Lose();
            }
            else
            {
                Win();
            }
        }
        
    }

    private void Win()
    {
        StaticVar.LevelIndex += 1;
        _winGO.SetActive(true);
    }

    private void Lose()
    {
        _loseGo.SetActive(true);
    }




}
