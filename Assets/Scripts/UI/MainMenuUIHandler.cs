using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIHandler : MonoBehaviour
{
    [Header("Panels")]
    public GameObject playerDetailPanel;
    public GameObject sessionBrowserPanel;
    public GameObject createSessionPanel;
    public GameObject statusPanel;

    [Header("Player settings")]
    public TMP_InputField playerNameInputField;
    [Header("New game Session")]
    public TMP_InputField sessionNameInputField;

    void Start(){
        if(PlayerPrefs.HasKey("PlayerNickname")){
            playerNameInputField.text = PlayerPrefs.GetString("PlayerNickname");
        }
    }
    void HideAllPanel(){
        playerDetailPanel.SetActive(false);
        sessionBrowserPanel.SetActive(false);
        createSessionPanel.SetActive(false);
        statusPanel.SetActive(false);
    }
    public void OnFindGameClicked(){
        PlayerPrefs.SetString("PlayerNickname",playerNameInputField.text);
        PlayerPrefs.Save();

        GameManager.instance.playerNickName = playerNameInputField.text;

        NetworkRunnerHandler networkRunnerHandler = FindObjectOfType<NetworkRunnerHandler>();

        networkRunnerHandler.OnJoinLobby();
        
        HideAllPanel();

        sessionBrowserPanel.SetActive(true);

        FindObjectOfType<SessionListUIHandler>(true).OnLookingForGameSessions();
    }

    // Session List Panel���� New Session Button
    public void OnCreateNewGameClicked(){
        HideAllPanel();
        createSessionPanel.SetActive(true);
    }

    // Create Game Panel���� �� �̸� �Է� �� �� ���� ��
    public void OnStartNewSessionClicked(){
        NetworkRunnerHandler networkRunnerHandler = FindObjectOfType<NetworkRunnerHandler>();
        networkRunnerHandler.CreateGame(sessionNameInputField.text,"World1");
        OnJoiningServer();
    }

    public void OnJoiningServer(){
        HideAllPanel();
        statusPanel.SetActive(true);
    }

    // Session List Panel���� Random Matching Button
    public void OnStartRandomMatching(){
        NetworkRunnerHandler networkRunnerHandler = FindObjectOfType<NetworkRunnerHandler>();
        SessionListUIHandler sessionListUIHandler = FindObjectOfType<SessionListUIHandler>();
        if(sessionListUIHandler.GetSessionListCount()==0){
            // Session�� �ϳ��� �������� ���� �� �� ����
            networkRunnerHandler.CreateGame(Utils.GetRandomSessionName(),"World1");
        }else{
            // Session�� ������ ��� Random ����
            var randomSession = sessionListUIHandler.GetRandomSession();
            networkRunnerHandler.JoinGame(randomSession);
        }
        OnJoiningServer();
    }
}
