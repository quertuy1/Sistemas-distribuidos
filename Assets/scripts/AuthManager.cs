using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System.Collections;

public class AuthManager : MonoBehaviour
{
    public TMP_InputField usernameField;
    public TMP_InputField passwordField;
    public TextMeshProUGUI resultText;

    private string apiUrl = "https://sid-restapi.onrender.com/";

    public void RegisterUser()
    {
        StartCoroutine(RegisterCoroutine());
    }

    public void LoginUser()
    {
        StartCoroutine(LoginCoroutine());
    }

    private IEnumerator RegisterCoroutine()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", usernameField.text);
        form.AddField("password", passwordField.text);

        UnityWebRequest request = UnityWebRequest.Post(apiUrl + "register", form);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            resultText.text = "Registration successful!";
        }
        else
        {
            resultText.text = "Error: " + request.error;
        }
    }

    private IEnumerator LoginCoroutine()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", usernameField.text);
        form.AddField("password", passwordField.text);

        UnityWebRequest request = UnityWebRequest.Post(apiUrl + "login", form);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string token = request.downloadHandler.text; // Handle token
            PlayerPrefs.SetString("authToken", token);  // Save token
            resultText.text = "Login successful!";
        }
        else
        {
            resultText.text = "Error: " + request.error;
        }
    }
}
