using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(InputField))]
public class TabBetween : MonoBehaviour
{
    public InputField nextFied;
    InputField myField;
    private void Start()
    {
        if (nextFied == null)
        {
            Destroy(this);
            return;
        }
        myField = GetComponent<InputField>();
    }
    private void Update()
    {
        if (myField.isFocused && Input.GetKeyDown(KeyCode.Tab))
        {
            nextFied.ActivateInputField();
        }
    }
}
