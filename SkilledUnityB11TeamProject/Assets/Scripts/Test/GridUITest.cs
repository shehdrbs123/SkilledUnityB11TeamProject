using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridUITest : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            GameObject ui = GameManager.Instance._uiManager.GetUI("CraftPanelUI");
            GridPanelUI panelui = ui.GetComponent<GridPanelUI>();
            panelui.Init();
            ui.SetActive(!ui.activeSelf);
        }
        
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            if (!GameManager.Instance._buildManager.isBuildMode)
            {
                GameObject ui = GameManager.Instance._uiManager.GetUI("BuildPanelUI");
                GridPanelUI panelui = ui.GetComponent<GridPanelUI>();
                panelui.Init();
                ui.SetActive(!ui.activeSelf);                
            }
        }
        
        
    }
}
