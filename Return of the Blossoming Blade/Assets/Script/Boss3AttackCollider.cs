using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss3AttackCollider : MonoBehaviour
{
    public float SkillDamage;

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"���� ���� �ݶ��̵� ���� 1");

        if (collision.CompareTag("PlayerCombatCol"))
        {
            Debug.Log($"���� ���� �ݶ��̵� ���� 2");

            // PlayerStatus ��ũ��Ʈ�� �����ɴϴ�. (�̶� PlayerCombatCol�� �θ��� Player ������Ʈ�κ��� �����ɴϴ�.)
            PlayerStatus playerStatus = collision.transform.parent.GetComponent<PlayerStatus>();

            playerStatus.TakeDamage(SkillDamage);
        }
    }
}
