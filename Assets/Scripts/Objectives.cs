using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objectives : MonoBehaviour
{
    public GameObject Panel;
    public GameObject notesPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.O) && Panel != null)
        {
            bool isActive = Panel.activeSelf;
            Panel.SetActive(!isActive);
        }
        if (Input.GetKeyDown(KeyCode.N) && notesPanel != null)
        {
            bool isActive = notesPanel.activeSelf;
            notesPanel.SetActive(!isActive);
            
        }
    }
}
