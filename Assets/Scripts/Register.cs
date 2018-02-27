using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;


public class Register : MonoBehaviour {

    // Public - These variables cannot begin with capital letters.
    public GameObject username;
    public GameObject email;
    public GameObject password;
    public GameObject confirmPassword;

    // Private - These are the capital lettered variables.
    private string Username;
    private string Email;
    private string Password;
    private string ConfirmPassword;

    
    private string registerForm;    // This form will hold all the private variables.
    private bool emailValidation = false;

    // The following characters are for validating email. Email can only start with these characters.
    private string[] Characters = new string[] {"a","b","c","d","e","f","g","h","i","j","k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z",
                                    "A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z",
                                    "1","2","3","4","5","6","7","8","9","0","_", "-"};

    // Public - as it needs to be seen by the button.
    public void RegisterButton()
    {
        //print("Registration Successful!");    // Test message which will print to the Unity console if successful.

        bool UN = false;
        bool EM = false;
        bool PW = false;
        bool CPW = false;

        // Username validation.
        if(Username != "")  // If the username field is not empty.
        {
            if (!System.IO.File.Exists(@"C:/Users/Lee/Documents/UnityTestFolder/" + Username + ".txt"))
            {
                UN = true;
            }
            else
            {
                Debug.LogWarning("Username already exists!");
            }
        } else
        {
            Debug.LogWarning("Username field is empty!");
        }
        
        // Email validation.
        if(Email != "") // If email field is not empty.
        {
            EmailValidation();
            if (emailValidation)    // If email valid is true.
            {
                if (Email.Contains("@"))
                {
                    if (Email.Contains("."))
                    {
                        EM = true;
                    } else
                    {
                        Debug.LogWarning("Email is incorrect!");
                    }
                } else
                {
                    Debug.LogWarning("Email is incorrect!");
                }
            } else
            {
                Debug.LogWarning("Email is incorrect!");
            }
        } else
        {
            Debug.LogWarning("Email field is empty!");
        }

        // Password validation. MUST ADD UPPERCASE AUTHENTICATION
        if(Password != "")
        {
            if(Password.Length > 5) // Password must be atleast 6 characters long - matching VS ASP.Net MVC Authentication ruleset.
            {
                if(Password.Contains("0") || Password.Contains("1") || Password.Contains("2") || Password.Contains("3") || Password.Contains("4") || Password.Contains("5") || Password.Contains("6") || Password.Contains("7") || Password.Contains("8") || Password.Contains("9"))
                {
                    PW = true;
                }
                else
                {
                    Debug.LogWarning("Password must contain atleast one digit character!");
                }
            } else
            {
                Debug.LogWarning("Password must be atleast 6 characters!");
            }
        } else 
        {
            Debug.LogWarning("Password field is empty!");
        }

        // Confirm Password validation.
        if(ConfirmPassword != "") // If confirm password field is not empty.
        {
            if(ConfirmPassword == Password) // If both passwords match.
            {
                CPW = true;
            } else
            {
                Debug.LogWarning("Passwords do not match!");
            }
        } else // If confirm password field is empty.
        {
            Debug.LogWarning("Confirm password field is empty!");
        }

        // Password encryption
        if(UN == true && EM == true && PW == true && CPW == true)
        {
            bool Clear = true;
            int i = 1;
            foreach(char c in Password) // For every character in the users password
            {
                if (Clear)
                {
                    Password = "";
                    Clear = false;
                }
                i++; // Inverse factorial encryption, i starts at 1 and goes up each time to how many characters are in the users password.
                char Encrypted = c;
                // char Encrypted = (char)(c * i);
                Password += Encrypted.ToString(); // Adds the encryption to the password.
            }
            // PUTS USERS DETAILS INTO A TEXT FILE TO THE FOLLOWING FOLDER (FOR TESTING PURPOSES FOR THIS PROJECT):
            registerForm = ("Username:\t" + Username + Environment.NewLine + "Email:\t\t" + Email + Environment.NewLine + "Password:\t" + Password);
            System.IO.File.WriteAllText(@"C:/Users/Lee/Documents/UnityTestFolder/" + Username + ".txt", registerForm);
            // Clearing forms.
            username.GetComponent<InputField>().text = "";
            email.GetComponent<InputField>().text = "";
            password.GetComponent<InputField>().text = "";
            confirmPassword.GetComponent<InputField>().text = "";
            print("Registration Complete!");
        }
    }

    void EmailValidation()
    {
        bool SW = false; // SW = Starts With.
        bool EW = false; // EW = Ends With.
        for (int i = 0; i < Characters.Length; i++)
        {
            if (Email.StartsWith(Characters[i]))
            {
                SW = true;
            }
        }
        for (int i = 0; i < Characters.Length; i++)
        {
            if (Email.EndsWith(Characters[i]))
            {
                EW = true;
            }
        }
        if (SW == true && EW == true)
        {
            emailValidation = true;
        }
        else
        {
            emailValidation = false;
        }
    }

    // Update is called once per frame.
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))  // This will allow the user to tab through the login/register forms
        {
            if (username.GetComponent<InputField>().isFocused)
            {
                email.GetComponent<InputField>().Select();  // Selects the next input field (email) in the form when the tab button is pressed.
            }
            if (email.GetComponent<InputField>().isFocused)
            {
                password.GetComponent<InputField>().Select();  // Selects the next input field (password) in the form when the tab button is pressed.
            }
            if (password.GetComponent<InputField>().isFocused)
            {
                confirmPassword.GetComponent<InputField>().Select();  // Selects the next input field (confirm password) in the form when the tab button is pressed.
            }
        }
        if (Input.GetKeyDown(KeyCode.Return))   // If the user hits the enter key/ the return key.
        {
            if(Password != "" && Email != "" && Password != "" && ConfirmPassword != "")  // Here we use "" instead of 'null' as we cannot use null in this case.
            {
                RegisterButton();
            }
        }
        Username = username.GetComponent<InputField>().text;
        Email = email.GetComponent<InputField>().text;
        Password = password.GetComponent<InputField>().text;
        ConfirmPassword = confirmPassword.GetComponent<InputField>().text;
    }
}

