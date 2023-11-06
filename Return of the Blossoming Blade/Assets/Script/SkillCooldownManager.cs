using System;
using System.Collections;
using UnityEngine;
using Modularify.LoadingBars3D;

public class SkillCooldownManager : MonoBehaviour
{
    public LoadingBarSegments QBar;
    public LoadingBarSegments WBar;
    public LoadingBarSegments EBar;

    public GameObject QSkillObject;
    public GameObject WSkillObject;
    public GameObject ESkillObject;

    public float QCooldownTime = 5f;
    public float WCooldownTime = 10f;
    public float ECooldownTime = 30f;

    private bool isQCooldown = false;
    private bool isWCooldown = false;
    private bool isECooldown = false;

    private bool isQActive = false;
    private bool isWActive = false;
    private bool isEActive = false;

    public float QSkillDuration = 1.9f;
    public float WSkillDuration = 1.9f;
    public float ESkillDuration = 4.4f;

    public GameObject QSkillCol; // Q�� Skill Col ����

    public GameObject WSkillCol;
    public float WSkillDistance = 400f; // W ��ų ���󰡴� �Ÿ�
    private Vector2 lastDirection = Vector2.left;  // �⺻������ ������ �ٶ󺸰� ����
    public GameObject WSkillParticle;

    public GameObject ESkillCol;
    public GameObject ESkillCol2;


    private void Update()
    {
        // ��ų�� Ȱ��ȭ ������ üũ
        bool anySkillActive = isQActive || isWActive || isEActive;

        // �÷��̾��� ������ ������ �����մϴ�.
        if (Input.GetKeyDown(KeyCode.UpArrow)) lastDirection = Vector2.up;
        else if (Input.GetKeyDown(KeyCode.DownArrow)) lastDirection = Vector2.down;
        else if (Input.GetKeyDown(KeyCode.LeftArrow)) lastDirection = Vector2.left;
        else if (Input.GetKeyDown(KeyCode.RightArrow)) lastDirection = Vector2.right;

        // Q ��ų ���
        if (Input.GetKeyDown(KeyCode.Q) && !isQCooldown && !anySkillActive)
        {
            StartCoroutine(UseSkill(QCooldownTime, QSkillDuration, QBar, QSkillObject, () => isQActive = true, () => isQActive = false, () => isQCooldown = true, () => isQCooldown = false));
            StartCoroutine(QSkillEffect()); // QSkillEffect �ڷ�ƾ ȣ��

        }

        // W ��ų ���
        if (Input.GetKeyDown(KeyCode.W) && !isWCooldown && !anySkillActive)
        {
            StartCoroutine(UseSkill(WCooldownTime, WSkillDuration, WBar, WSkillObject, () => isWActive = true, () => isWActive = false, () => isWCooldown = true, () => isWCooldown = false));
            StartCoroutine(WSkillEffect());  // ��ƼŬ ȿ�� �ڷ�ƾ ȣ��
        }

        // E ��ų ���
        if (Input.GetKeyDown(KeyCode.E) && !isECooldown && !anySkillActive)
        {
            StartCoroutine(UseSkill(ECooldownTime, ESkillDuration, EBar, ESkillObject, () => isEActive = true, () => isEActive = false, () => isECooldown = true, () => isECooldown = false));
            StartCoroutine(ESkillEffect());
        } 
    }

    private IEnumerator UseSkill(float cooldownTime, float skillDuration, LoadingBarSegments bar, GameObject skillObject, Action onStart, Action onEnd, Action onCooldownStart, Action onCooldownEnd)
    {
        onStart();
        skillObject.SetActive(true);

        PlayerManager.instance.notMove = true; // ��ų�� Ȱ��ȭ�Ǹ� �̵��� �����ϴ�.

        // ��ų ���� �ð� ���� Ȱ��ȭ ���� ����
        yield return new WaitForSeconds(skillDuration);

        PlayerManager.instance.notMove = false; // ��ų ���� �ð��� ������ �̵��� ����մϴ�.

        skillObject.SetActive(false);
        onEnd();

        onCooldownStart();
        float timeElapsed = 0;
        while (timeElapsed < cooldownTime)
        {
            timeElapsed += Time.deltaTime;
            float percentage = timeElapsed / cooldownTime;
            bar.SetPercentage(percentage);
            yield return null;
        }
        bar.SetPercentage(0);
        onCooldownEnd();
    }

    private IEnumerator QSkillEffect()
    {
        for (int i = 0; i < 3; i++) // 3�� �ݺ�
        {
            QSkillCol.SetActive(true);  // Skill Col Ȱ��ȭ
            yield return new WaitForSeconds(0.3f); // 0.1�� ���. �� ���� �����Ͽ� ON/OFF ������ ������ �� �ֽ��ϴ�.
            QSkillCol.SetActive(false); // Skill Col ��Ȱ��ȭ
            yield return new WaitForSeconds(0.15f); // 0.1�� ���
        }
    }

    private IEnumerator WSkillEffect()
    {
        WSkillCol.SetActive(true);  // ��ų �ݶ��̴� Ȱ��ȭ

        Vector2 originalPos = WSkillCol.transform.position;
        Vector2 targetPos = originalPos + lastDirection * WSkillDistance;

        if (lastDirection == Vector2.left) // ������ �� z �� -90
        {
            WSkillParticle.transform.rotation = Quaternion.Euler(0, 0, -90);
        }
        else if (lastDirection == Vector2.down) // �Ʒ����� �� z �� 0
        {
            WSkillParticle.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (lastDirection == Vector2.right) // �������� �� z �� 90
        {
            WSkillParticle.transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else if (lastDirection == Vector2.up) // ������ �� z �� 180
        {
            WSkillParticle.transform.rotation = Quaternion.Euler(0, 0, 180);
        }

        float startTime = Time.time;
        float journeyLength = Vector2.Distance(originalPos, targetPos);

        float fractionOfJourney = 0f; // �ʱ� ������ 0���� ����

        while (fractionOfJourney < 1.0f) // 1�� ������ ������ �ݺ�
        {
            float timeSinceStarted = (Time.time - startTime) / WSkillDuration; // ���� �ð��� ��ų ���� �ð����� ������ ������ ���մϴ�.
            fractionOfJourney = 1 - Mathf.Pow(1 - timeSinceStarted, 3); // ������ ������, ���� �������� ȿ���� ����ϴ�.

            WSkillCol.transform.position = Vector2.Lerp(originalPos, targetPos, fractionOfJourney); // �ε巴�� ������ ��ġ�� �ݶ��̴� �̵�


            yield return null;
        }

        WSkillCol.transform.position = targetPos;  // ���� �ð��� ������ ��ǥ �������� �ݶ��̴� ��ġ ����

        WSkillCol.SetActive(false);  // ��ų ȿ�� ���� �� �ݶ��̴� ��Ȱ��ȭ

        WSkillCol.transform.position = originalPos;  // �ݶ��̴��� ������ ��ġ�� �缳��
    }
    private IEnumerator ESkillEffect()
    {
        ESkillCol.SetActive(true); // ù ��° �ݶ��̴� Ȱ��ȭ
        ESkillCol2.SetActive(true); // �� ��° �ݶ��̴� Ȱ��ȭ

        yield return new WaitForSeconds(ESkillDuration); // 4.4�� ���� ���

        ESkillCol.SetActive(false); // ù ��° �ݶ��̴� ��Ȱ��ȭ
        ESkillCol2.SetActive(false); // �� ��° �ݶ��̴� ��Ȱ��ȭ
    }
}