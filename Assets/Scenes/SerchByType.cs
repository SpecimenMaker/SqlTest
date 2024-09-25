using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class SerchByType : MonoBehaviour
{
    public ScrollRect scrollRect; // Scroll View �� ScrollRect ���
    public GameObject content; // Scroll View �е� Content ����
    public Text organizationPrefab; // ����ʵ������ Text Ԥ����

    public Dropdown SearchTypeDropdown;

    public void CallSearch()
    {
        StartCoroutine(Search());
    }

    IEnumerator Search()
    {
        WWWForm form = new WWWForm();
        form.AddField("type", SearchTypeDropdown.options[SearchTypeDropdown.value].text);
        UnityWebRequest www = UnityWebRequest.Post("http://localhost/sqlconnect/searchbytype.php",form);
        yield return www.SendWebRequest();
        if (www.error != null)
        {
            Debug.Log("��ȡ����ʧ��: " + www.error);
        }
        else
        {
            // ��� Content �е������Ӷ���
            foreach (Transform child in content.transform)
            {
                Destroy(child.gameObject);
            }
            // ���� PHP ����� HTML �������
            string tableHtml = www.downloadHandler.text;
            // �ָ�������Ϊ��
            string[] rows = tableHtml.Split('\n', System.StringSplitOptions.RemoveEmptyEntries);
            // ��ʼ�� Text ����Ĵ�ֱλ��ƫ����
            float yOffset = 0f;

            // ѭ������ Text �������ʾÿ������
            foreach (string row in rows)
            {
                // �����µ� Text �������ø���Ϊ Content
                Text newText = Instantiate(organizationPrefab, content.transform);
                // ���� Text ���ı�����Ϊ��ǰ������
                newText.text = row;

                // ���� Text ��λ��ƫ��
                RectTransform rectTransform = newText.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = new Vector2(0, 100 - yOffset);

                // ���´�ֱλ��ƫ�������Ա���һ�� Text �����λ��
                yOffset += 30f; // ������Ե�����࣬�� 30f ��ÿ�� Text ����֮��Ĵ�ֱ���
            }

            // ���¼��� Content �Ĵ�С��ȷ������Ԫ�ؿɼ�
            LayoutRebuilder.ForceRebuildLayoutImmediate(content.GetComponent<RectTransform>());
        }
    }
    public void Exit()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(4);
    }
}
