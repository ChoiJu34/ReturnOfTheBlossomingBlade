using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public float maxHP = 100f; // �ִ� HP
    public float currentHP;   // ���� HP

    public float maxMP = 50f;  // �ִ� MP
    public float currentMP;   // ���� MP

    private void Start()
    {
        // ���� ���۽� �÷��̾��� HP�� MP�� �ִ�ġ�� ����
        currentHP = maxHP;
        currentMP = maxMP;
    }

    // �������� �Ծ��� �� ȣ��Ǵ� �Լ�
    public void TakeDamage(float damage)
    {
        currentHP -= damage;

        // HP�� 0 �̸��̸� 0���� ����
        if (currentHP < 0f)
        {
            currentHP = 0f;
            // TODO: �÷��̾� ��� ���� ����
        }
    }

    // MP�� ������� �� ȣ��Ǵ� �Լ�
    public void UseMP(float amount)
    {
        currentMP -= amount;

        // MP�� 0 �̸��̸� 0���� ����
        if (currentMP < 0f)
        {
            currentMP = 0f;
        }
    }

    // HP�� MP�� ȸ���ϴ� �Լ��鵵 �߰��� �� �ֽ��ϴ�.
}