using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Prueba_Llamadas : MonoBehaviour
{
    // Start is called before the first frame update
    private ActivitiesRoot activityRootPrueba;

    
    private Activity[] activity = new Activity[5];

    private void Start()
    {
        ActivitiesRoot();
    }


    public void activitiesAdd()
    {
        for(int i = 0; i < 5; i++)
        {
            activity[i] = new Activity();
            activity[i].id = 1234567;
            activity[i].title = "Deportes";
            activity[i].description = "mgojnrgono{snanangirapndijngp";
            activity[i].location = "Aquí";
            activity[i].category = "Biotecnología";
            activity[i].dateTime = new System.DateTime(2024, 3, 9);

        }
        
    }

    public ActivitiesRoot ActivitiesRoot()
    {
        activitiesAdd();
        activityRootPrueba = new ActivitiesRoot();
        activityRootPrueba.activities = activity.ToList();
        /*foreach (Activity _activity in activity)
        {
            activityRootPrueba.activities.Add(_activity);
        }*/
        return activityRootPrueba;
    }
}
