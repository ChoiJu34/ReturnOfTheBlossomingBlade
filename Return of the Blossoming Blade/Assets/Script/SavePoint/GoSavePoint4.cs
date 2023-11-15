using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GoSavePoint4 : MonoBehaviour
{
    private SaveContinueDialogueManager save;
    private PlayerManager player;

    // Start is called before the first frame update
    void Start()
    {
        save = FindObjectOfType<SaveContinueDialogueManager>();
        player = FindObjectOfType<PlayerManager>();
    }

    public void OnBtnClick()
    {
        //â �ݱ�
        player.allStop = false;
        player.notMove = false;
        save.CloseSaveDialogue();

        //�� �̵�
        SceneManager.LoadScene(PlayerPrefs.GetString("save4SceneName"));
        //û�� HP, MP
        PlayerPrefs.SetFloat("playerHP", PlayerPrefs.GetFloat("save4PlayerHP"));
        PlayerPrefs.SetFloat("playerMP", PlayerPrefs.GetFloat("save4PlayerMP"));
        //����
        PlayerPrefs.SetFloat("havePosion", PlayerPrefs.GetFloat("save4HavePosion"));
        PlayerPrefs.SetFloat("maxPosion", PlayerPrefs.GetFloat("save4MaxPosion"));

    }
}
