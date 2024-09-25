using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class SerchByType : MonoBehaviour
{
    public ScrollRect scrollRect; // Scroll View 的 ScrollRect 组件
    public GameObject content; // Scroll View 中的 Content 对象
    public Text organizationPrefab; // 用于实例化的 Text 预制体

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
            Debug.Log("获取数据失败: " + www.error);
        }
        else
        {
            // 清空 Content 中的所有子对象
            foreach (Transform child in content.transform)
            {
                Destroy(child.gameObject);
            }
            // 解析 PHP 输出的 HTML 表格数据
            string tableHtml = www.downloadHandler.text;
            // 分割表格数据为行
            string[] rows = tableHtml.Split('\n', System.StringSplitOptions.RemoveEmptyEntries);
            // 初始化 Text 对象的垂直位置偏移量
            float yOffset = 0f;

            // 循环创建 Text 组件来显示每行数据
            foreach (string row in rows)
            {
                // 创建新的 Text 对象并设置父级为 Content
                Text newText = Instantiate(organizationPrefab, content.transform);
                // 设置 Text 的文本内容为当前行数据
                newText.text = row;

                // 设置 Text 的位置偏移
                RectTransform rectTransform = newText.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = new Vector2(0, 100 - yOffset);

                // 更新垂直位置偏移量，以便下一个 Text 对象的位置
                yOffset += 30f; // 这里可以调整间距，如 30f 是每个 Text 对象之间的垂直间距
            }

            // 重新计算 Content 的大小，确保所有元素可见
            LayoutRebuilder.ForceRebuildLayoutImmediate(content.GetComponent<RectTransform>());
        }
    }
    public void Exit()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(4);
    }
}
