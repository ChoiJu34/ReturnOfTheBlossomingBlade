using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OOMCutScene5 : MonoBehaviour
{
    public Dialogue dialogue_1;
    public Dialogue dialogue_2;
    public Dialogue dialogue_3;
    public Dialogue dialogue_4;

    private DialogueManager theDM;
    private OrderManager theOrder;
    private PlayerManager thePlayer;
    private ChoiceManager theChoice;
    private OOMCutScene6 oomCutScene6;

    //private bool flag;
    private bool can = false;
    private bool one = true;

    // Start is called before the first frame update
    void Start()
    {
        theDM = FindObjectOfType<DialogueManager>();
        theOrder = FindObjectOfType<OrderManager>();
        thePlayer = FindObjectOfType<PlayerManager>();
        theChoice = FindObjectOfType<ChoiceManager>();
        oomCutScene6 = FindObjectOfType<OOMCutScene6>();
        theOrder.Turn("DangBo", "LEFT");
        theOrder.Turn("Master3", "DOWN");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        can = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        can = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (one && can && Input.GetKeyDown(KeyCode.C))
        {
            one = false;
            StartCoroutine(EventCoroutine());
        }
    }

    IEnumerator EventCoroutine()
    {
        theOrder.PreLoadCharacter();
        theOrder.NotMove();
        yield return new WaitForSeconds(0.2f);
        theDM.ShowDialogue(dialogue_1);
        theOrder.Move("Master3", "DOWN");
        theOrder.Move("Master3", "DOWN");
        theOrder.Move("Master3", "DOWN");
        yield return new WaitForSeconds(0.2f);
        theOrder.Appear("Sit", true);
        theOrder.Appear("Master3", false);
        theOrder.Appear("DangBo", false);
        theOrder.Appear("Player", false);
        yield return new WaitUntil(() => !theDM.talking);

        if (PlayerPrefs.HasKey("duringTime1"))
        {
            if (PlayerPrefs.GetInt("duringTime1") < 2)
            {
                theDM.ShowDialogue(dialogue_2);
                yield return new WaitUntil(() => !theDM.talking);
            }
            else if (PlayerPrefs.GetInt("duringTime1") < 4)
            {
                theDM.ShowDialogue(dialogue_3);
                yield return new WaitUntil(() => !theDM.talking);
            }
            else if (PlayerPrefs.GetInt("duringTime1") < 5)
            {
                theDM.ShowDialogue(dialogue_4);
                yield return new WaitUntil(() => !theDM.talking);
            }
        }
        else
        {
            Debug.Log("duringTime1 ���� ����");
        }
        oomCutScene6.enable = true;
        theOrder.Appear("Sit", false);
        theOrder.Appear("Player", true);
        theOrder.Appear("DangBo", true);
        theOrder.Appear("Master3", true);

        theOrder.Move();
    }
}
