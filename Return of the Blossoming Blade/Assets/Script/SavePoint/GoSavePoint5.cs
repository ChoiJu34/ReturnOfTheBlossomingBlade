using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GoSavePoint5 : MonoBehaviour
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
        SceneManager.LoadScene(PlayerPrefs.GetString("save5SceneName"));
        //û�� HP, MP
        PlayerPrefs.SetFloat("playerHP", PlayerPrefs.GetFloat("save5PlayerHP"));
        PlayerPrefs.SetFloat("playerMP", PlayerPrefs.GetFloat("save5PlayerMP"));
        //����
        PlayerPrefs.SetFloat("havePosion", PlayerPrefs.GetFloat("save5HavePosion"));
        PlayerPrefs.SetFloat("maxPosion", PlayerPrefs.GetFloat("save5MaxPosion"));

    }
}
