using JetBrains.Annotations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
using static UnityEngine.ParticleSystem;

#region Entities

[System.Serializable]
public class Instanceable { }

[System.Serializable]
public class User
{
    public int id { get; set; }
    public string name { get; set; }
    public string email { get; set; }
    public string password { get; set; }
    public string pass { get; set; }
    public List<Activity> activities { get; set; }
    public List<Conference> conferences { get; set; }

}
public class MapRoot
{
    public string image { get; set; }
}

[System.Serializable]
public class Media
{
    public int id { get; set; }
    public string type { get; set; }
    public string url { get; set; }
    public string complete_url { get; set; }
}

[Serializable]
public class Activity
{
    public int id { get; set; }
    public string title { get; set; }
    public string description { get; set; }
    public string link { get; set; }
    public DateTime dateTime { get; set; }
    public bool has_capacity_limit { get; set; }
    public int? capacity_limit { get; set; }
    public string category { get; set; }
    public string capacity_status { get; set; } //Puede regresar "AGOTADO" o "DISPONIBLE"
    public string ticket_status { get; set; } //Puede regresar "REDIMIDO" o "NO REDIMIDO"
    public Media media { get; set; }
    public string location { get; set; }
    public string formattedDate { get; set; }
}

[Serializable]
public class Conference
{
    public int id { get; set; }
    public string title { get; set; }
    public string description { get; set; }
    public string link { get; set; }
    public DateTime dateTime { get; set; }
    public bool has_capacity_limit { get; set; }
    public int? capacity_limit { get; set; }
    public string category { get; set; }
    public string speaker { get; set; }
    public string capacity_status { get; set; } //Puede regresar "AGOTADO" o "DISPONIBLE"
    public string ticket_status { get; set; } //Puede regresar "REDIMIDO" o "NO REDIMIDO"
    public Media media { get; set; }
    public string location { get; set; }
    public string formattedDate { get; set; }
}

#endregion

#region Root Classes

[System.Serializable]
public class UserRoot { public User user { get; set; } }

[System.Serializable]
public class PostLogInRoot
{
    public string email { get; set; }
    public string password { get; set; }
}

[System.Serializable]
public class LogInRoot {
    public User user { get; set; }
    public string access_token { get; set; }
    public string token_type { get; set; }
}

[System.Serializable]
public class PostCreateUserRoot
{
    public string user_name { get; set; }
    public string email { get; set; }
    public string password { get; set; }
}

[System.Serializable]
public class PostRegisterToActivity
{
    public string user_id { get; set; }
    public string activity_id { get; set; }
}

[System.Serializable]
public class PostRegisterToConference
{
    public string user_id { get; set; }
    public string conference_id { get; set; }
}

[System.Serializable]
public class ErrorMessageRoot { public string message { get; set; } }

[Serializable]
public class ActivitiesRoot{ public List<Activity> activities { get; set; } }

[Serializable]
public class ActivityRoot { public Activity activity { get; set; } }

[Serializable]
public class GetSeveralActivities {
    public List<Activity> activities { get; set; }
    public int total { get; set; }
    public string? next { get; set; }
    public string? previous { get; set; }
}

[Serializable]
public class GetActivityStatusRoot { public string status { get; set; } }

[Serializable]
public class ConferencesRoot { public List<Conference> conferences { get; set; } }

[Serializable]
public class ConferenceRoot { public List<Conference> conference { get; set; } }

[Serializable]
public class GetSeveralConference
{
    public List<Conference> conferences { get; set; }
    public int total { get; set; }
    public string? next { get; set; }
    public string? previous { get; set; }
}

[Serializable]
public class GetConferenceStatusRoot { public string status { get; set; } }
#endregion

#region Test Entities
[System.Serializable]
public class UserExample : Instanceable
{
    public string email;
    public string password;
}


[System.Serializable]
public class InstanceableExample : Instanceable
{
    public int id;
    public string name;
    public string description;
}

#endregion

#region Test Root Classes

[System.Serializable]
public class InstanceableExampleRoot
{
    public List<InstanceableExample> examples { get; set; }
}

[System.Serializable]
public class LogInExample
{
    public UserExample user { get; set; }
}


#endregion
