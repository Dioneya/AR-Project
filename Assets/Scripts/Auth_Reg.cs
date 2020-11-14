using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
public class Auth_Reg : MonoBehaviour
{
    private readonly string tokenByPhone = GlobalVariables.link + "/api/auth/token_by_phone";
    private readonly string signupByPhone = GlobalVariables.link + @"/api/auth/signup_by_phone";

    public GameObject Profile;
    public GameObject Auth_reg_window;

    public GameObject Sign_btn;
    public GameObject Reg_btn;

    //public GameObject Forgot_text;

    public GameObject InputField_Phone_Auth;
    public GameObject InputField_Password_Auth;

    public GameObject InputField_Phone_Reg;


    private Button sign_btn;
    private Button reg_btn;

    private bool isAuthorized;
    
    // Start is called before the first frame update
    void Start()
    {
        sign_btn = Sign_btn.GetComponent<Button>();
        sign_btn.onClick.AddListener(Sign);

        reg_btn = Reg_btn.GetComponent<Button>();
        reg_btn.onClick.AddListener(SignUp);
    }

    private void Update()
    {
        if (GlobalVariables.tokenResponse != null && !isAuthorized) 
        {
            isAuthorized = true;
            Profile.SetActive(true);
            Auth_reg_window.SetActive(false);
        }
    }

    #region Регистрация
    void SignUp()
    {
        string phone = InputField_Phone_Reg.GetComponent<TMP_InputField>().text;

        StartCoroutine(SignUp(phone));
    }

    public IEnumerator SignUp(string phone)
    {

        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();

        formData.Add(new MultipartFormDataSection("phone", phone));

        UnityWebRequest www = UnityWebRequest.Post(signupByPhone, formData);
        // www.SetRequestHeader("Content-Type", "application/json");
        www.SetRequestHeader("X-Requested-With", "XMLHttpRequest");

        yield return www.SendWebRequest();


        Debug.Log(www.downloadHandler.text);

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            GlobalVariables.tokenResponse = JsonUtility.FromJson<TokenClass.TokenResponse>(www.downloadHandler.text);

            if (GlobalVariables.tokenResponse == null)
            {
                Debug.Log("Обновить не удалось!");
            }
            else
            {

                if (GlobalVariables.tokenResponse.access_token != null)
                {
                    Debug.Log("Регистрация прошла успешно!");
                    UserConfig.WriteUser();
                    Profile.SetActive(true);
                    Auth_reg_window.SetActive(false);
                }
                else
                {
                    Debug.Log("Ошибка регистрации");
                }
            }
        }
    }

    #endregion

    #region Вход
    void Sign()
    {
        string phone = InputField_Phone_Auth.GetComponent<TMP_InputField>().text;
        string password = InputField_Password_Auth.GetComponent<TMP_InputField>().text;

        StartCoroutine(SignIn(phone, password));

    }
    public IEnumerator SignIn(string phone, string password)
    {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("phone", phone));
        formData.Add(new MultipartFormDataSection("password", password));

        UnityWebRequest www = UnityWebRequest.Post(tokenByPhone, formData);
        // www.SetRequestHeader("Content-Type", "application/json");
        www.SetRequestHeader("X-Requested-With", "XMLHttpRequest");

        yield return www.SendWebRequest();


        Debug.Log(www.downloadHandler.text);

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            GlobalVariables.tokenResponse = JsonUtility.FromJson<TokenClass.TokenResponse>(www.downloadHandler.text);

            if (GlobalVariables.tokenResponse == null)
            {
                Debug.Log("Вход не удался!");
            }
            else
            {
                Debug.Log("Вход удался!");
                UserConfig.WriteUser();
                Profile.SetActive(true);
                Auth_reg_window.SetActive(false);
            }
        }
    }
    #endregion

}
