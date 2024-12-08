using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public UIView uiView;
    private int score = 0;

    public static ScoreManager Instance;

    void Awake(){
        if (Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else{
            Destroy(gameObject);
        }

        uiView = FindObjectOfType<UIView>();
        if(uiView == null) Debug.LogError("UIView not found");
    }

    void Start(){
        if(uiView != null) uiView.UpdateScoreText(score);
    }

    // 增加积分
    public void AddScore(int points){
        score += points;
        // Debug.Log("Score:" + score + " += " + "points");
        if(uiView != null) uiView.UpdateScoreText(score);
    }
}
