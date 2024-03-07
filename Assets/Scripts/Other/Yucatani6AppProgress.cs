using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yucatani6AppProgress : AppProgress
{
    [SerializeField] public string email = "";
    [SerializeField] public string password = "";

    public Yucatani6AppProgress(string _email, string _password)
    {
        this.email = _email;
        this.password = _password;
    }
}
