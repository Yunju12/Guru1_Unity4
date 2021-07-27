using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlatGameManager : MonoBehaviour
{
    public int totalPoint;
    public int stagePoint;
    public int stageIndex;

    
   public void NextStage()
    {
        stageIndex++;
        totalPoint += stagePoint;
        stagePoint = 0;
    }

    
    void Update()
    {
        
    }
}
