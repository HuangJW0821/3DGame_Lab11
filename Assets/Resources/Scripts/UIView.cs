using UnityEngine;

public class UIView : MonoBehaviour
{
    private int score = 0;
    private int arrowNum = 0;

    void Update(){}

    // 用 OnGUI 显示 UI
    private void OnGUI(){

        GUIStyle style = new GUIStyle();
        style.fontSize = 24;
        style.normal.textColor = Color.red;

        GUI.Label(new Rect(Screen.width - 200, 20, 200, 30), "Score: " + score.ToString(), style);
        GUI.Label(new Rect(Screen.width - 200, 60, 200, 30), "Arrows Left: " + arrowNum.ToString(), style);
    }

    // 更新积分
    public void UpdateScoreText(int points){
        score = points;
    }

    public void UpdateArrowNum(int num){
        arrowNum = num;
    }
}

