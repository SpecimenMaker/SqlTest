using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class DeleteButton : MonoBehaviour
{
    public Button deleteButton;
    public InputField IDInputField;

    public void DeleteVerifyInputs()
    {
        deleteButton.interactable = (IDInputField.text.Length > 0);
    }
    public void CallDelete()
    {
        StartCoroutine(DeleteOrganization());
    }
    IEnumerator DeleteOrganization()
    {
        WWWForm form = new WWWForm();
        form.AddField("id", IDInputField.text);
        UnityWebRequest www = UnityWebRequest.Post("http://localhost/sqlconnect/deleteorganization.php", form);
        yield return www.SendWebRequest();
        if (www.downloadHandler.text == "0")
        {
            Debug.Log("Orggnization deleted successfully.");
        }
        else
        {
            Debug.Log("Orggnization deletes failed. error #" + www.downloadHandler.text);
        }
    }
}
