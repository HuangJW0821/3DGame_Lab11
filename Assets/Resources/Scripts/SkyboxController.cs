using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxController : MonoBehaviour{
    public Material skybox1;
    public Material skybox2;
    private int skyboxIndex = 0;
    public int changeTime = 15;
    
    void Start(){
        InvokeRepeating("ChangeSkybox", 0, changeTime);
    }

    public void ChangeSkybox(){
        if(skyboxIndex == 0){
            RenderSettings.skybox = skybox1;
            skyboxIndex = 1;
        }else{
            RenderSettings.skybox = skybox2;
            skyboxIndex = 0;
        }
    }
}
