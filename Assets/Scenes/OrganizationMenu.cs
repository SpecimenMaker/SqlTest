using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class OrganizationMenu : MonoBehaviour
{
    public Dropdown addTypeDropdown;
    public Dropdown addRateDropdown;

    public InputField addNameField;

    public Button addSubmitButton;

    public Dropdown changeTypeDropdown;
    public Dropdown changeRateDropdown;

    public InputField changeNameField;
    public InputField changeIDField;

    public Button changeSubmitButton;



    public void CallAddOrganization()
    {
        StartCoroutine(AddOrganization());
    }

    IEnumerator AddOrganization()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", addNameField.text);
        form.AddField("type", addTypeDropdown.options[addTypeDropdown.value].text);
        form.AddField("rate", addRateDropdown.options[addTypeDropdown.value].text);

        UnityWebRequest www = UnityWebRequest.Post("http://localhost/sqlconnect/addorganization.php", form);
        yield return www.SendWebRequest();
        if (www.downloadHandler.text == "0")
        {
            Debug.Log("Orggnization created successfully.");
        }
        else
        {
            Debug.Log("Orggnization creats failed. error #" + www.downloadHandler.text);
        }
    }
    public void AddVerifyInputs()
    {
        addSubmitButton.interactable = (addNameField.text.Length >= 8);
    }

    public void CallChangeOrganization()
    {
        StartCoroutine(ChangeOrganization());
    }

    IEnumerator ChangeOrganization()
    {
        WWWForm form = new WWWForm();
        form.AddField("id", changeIDField.text);
        form.AddField("name", changeNameField.text);
        form.AddField("type", changeTypeDropdown.options[changeTypeDropdown.value].text);
        form.AddField("rate", changeRateDropdown.options[changeTypeDropdown.value].text);

        UnityWebRequest www = UnityWebRequest.Post("http://localhost/sqlconnect/changeorganization.php", form);
        yield return www.SendWebRequest();
        if (www.downloadHandler.text == "0")
        {
            Debug.Log("Orggnization changed successfully.");
        }
        else
        {
            Debug.Log("Orggnization changes failed. error #" + www.downloadHandler.text);
        }
    }
    public void ChangeVerifyInputs()
    {
        changeNameField.interactable = (changeIDField.text.Length > 0);
        changeTypeDropdown.interactable = (changeIDField.text.Length > 0);
        changeRateDropdown.interactable = (changeIDField.text.Length > 0);
        changeSubmitButton.interactable = (changeNameField.text.Length >= 8) ;
    }
    public void Back()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(3);
    }
    public void Search()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(5);
    }
}
