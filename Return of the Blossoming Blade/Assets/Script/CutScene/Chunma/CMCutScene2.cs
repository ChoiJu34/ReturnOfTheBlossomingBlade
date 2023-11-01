using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMCutScene2 : MonoBehaviour
{
    public Dialogue dialogue_1;

    private DialogueManager theDM;
    private OrderManager theOrder;
    private PlayerManager thePlayer;
    private ChoiceManager theChoice;

    //private bool flag;
    private bool can = false;
    private bool one = true;

    public bool finish = false;

    // Start is called before the first frame update
    void Start()
    {
        theDM = FindObjectOfType<DialogueManager>();
        theOrder = FindObjectOfType<OrderManager>();
        thePlayer = FindObjectOfType<PlayerManager>();
        theChoice = FindObjectOfType<ChoiceManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (one && finish)
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

        //û�� HP 5% ����, +100 ����
        //(û�� HP 5% �̻�) (ȭ�� ���� 100%)(+250�̻�)
        //(���� %>80% && û�� HP <5%)(+250�̻�)
        //(���� %>90% && û�� HP <5% && �纸 ����)(+300�̻�)
        //(õ�� ������� ��) (ȭ�� ���� 0%)
        //(+325)(������� �� û�� HP 90% �̻�)

        theOrder.Move();
    }
}

