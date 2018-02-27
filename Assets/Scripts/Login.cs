using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour {

    // Public - These variables cannot begin with capital letters.
    public GameObject username;
    public GameObject password;

    // Private - These are the capital lettered variables.
    private string Username;
    private string Password;

    private string[] Lines;
    private string DecryptedPassword;

	public void LoginButton()
    {
        bool UN = false;
        bool PW = false;

        // Username validation.
        if(Username != "")  // If username field is not empty.
        {
            if(System.IO.File.Exists(@"C:/Users/Lee/Documents/UnityTestFolder/" + Username + ".txt")) // Checks if the user exists on file.
            {
                UN = true;
                Lines = System.IO.File.ReadAllLines(@"C:/Users/Lee/Documents/UnityTestFolder/" + Username + ".txt"); // Reads all of the lines in the file as strings i.e. line 0 is username, line 1 is email and line 2 is password.
            } else
            {
                Debug.LogWarning("Username invalid!");
            }
        } else
        {
            Debug.LogWarning("Username field is empty!");
        }

        // Password validation.
        if(Password != "")  // If password field is not empty.
        {
            if (System.IO.File.Exists(@"C:/Users/Lee/Documents/UnityTestFolder/" + Username + ".txt"))
            {
                // Encryption of password - Details on this found on Register.cs.
                int i = 1;
                foreach (char c in Lines[2])
                {
                    // char Decrypted = (char)(c / i);
                    char Decrypted = c;
                    DecryptedPassword += Decrypted.ToString();
                    i++;
                }
                if(Password == DecryptedPassword)
                {
                    PW = true;
                } else
                {
                    Debug.LogWarning("Password not equal to decrypted password!");
                }
            } else
            {
                Debug.LogWarning("Password invalid!");
            }
        } else
        {
            Debug.LogWarning("Password field is empty!");
        }

        if(UN == true && PW == true)
        {
            username.GetComponent<InputField>().text = "";
            password.GetComponent<InputField>().text = "";
            print("Login Successful!");
            // Functionality goes here - E.G. load the next level.
            //Application.LoadLevel("Start Menu");
            SceneManager.LoadScene("MainMenu");
        }
    }

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Tab))  // This will allow the user to tab through the login/register forms
        {
            if (username.GetComponent<InputField>().isFocused)
            {
                password.GetComponent<InputField>().Select();  // Selects the next input field (email) in the form when the tab button is pressed.
            }
        }
        if (Input.GetKeyDown(KeyCode.Return))   // If the user hits the enter key/ the return key.
        {
            if (Password != "" && Password != "")  // Here we use "" instead of 'null' as we cannot use null in this case.
            {
                LoginButton();
            }
        }
        Username = username.GetComponent<InputField>().text;
        Password = password.GetComponent<InputField>().text;
    }
}
