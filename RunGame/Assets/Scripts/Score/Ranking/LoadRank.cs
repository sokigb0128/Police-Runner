using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class LoadRank : MonoBehaviour
{
    private int nowScore;
    private string nowName,key;
    private List<int> scoreList = new List<int>();
    private List<string> nameList = new List<string>();

    private DatabaseReference rankingDB;
    private bool loadComplete = false,sortComplete=false, firstTime = true;

    // Use this for initialization
    void Start()
    {
        nowScore = Score.GetScore();
        nowName = GameStart.name;

        //FireBaseのDatabaseにアクセス
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://rungameranking.firebaseio.com/");
        rankingDB = FirebaseDatabase.DefaultInstance.GetReference("ranks");
        
        ScoreRead();
    }

    private void Update()
    {
        if (loadComplete)
        {
            if (firstTime)
            {
                if (ScoreUpdate())
                {
                    //書き込みは一回のみ
                    if (gameObject.name == "RankingScores")
                    {
                        ScoreExport();
                    }
                    firstTime = false;
                }
                sortComplete = true;
            }
        }
    }

    void ScoreRead()
    {
        var value = rankingDB.OrderByChild("score").LimitToLast(10).GetValueAsync();
            value.ContinueWith(task => {
            if (task.IsFaulted)
            {
                // 取得エラー
                Debug.Log("[ERROR] get ranking sort");
            }
            else if (task.IsCompleted)
            {
                // 取得成功
                Debug.Log("[INFO] get ranking success.");
                DataSnapshot snapshot = task.Result;
                IEnumerator<DataSnapshot> result = snapshot.Children.GetEnumerator();
                
                while (result.MoveNext())
                {
                    DataSnapshot dataSnap = result.Current;
                    string name = (string)dataSnap.Child("name").Value;
                    // Firebaseの数値データはLong型となっているので、一度longで受け取った後にintにキャスト
                    int score = (int)(long)dataSnap.Child("score").Value;
                    scoreList.Add(score);
                    nameList.Add(name);
                }

                    if (scoreList.Count != 0)
                    {
                        loadComplete = true;
                    }

            }
        });
    }

    bool ScoreUpdate()
    {
        var reverseScoreList = new List<int>();
        var reverseNameList = new List<string>();
        for (int i = 1; i <= 10; i++)
        {
            reverseScoreList.Add(scoreList[10 - i]);
            reverseNameList.Add(nameList[10 - i]);
        }

        for (int i = 0; i < scoreList.Count; i++) {
            if (reverseScoreList[i] < nowScore) {
                reverseScoreList.Insert(i, nowScore);
                reverseNameList.Insert(i, nowName);
                scoreList = reverseScoreList;
                nameList = reverseNameList;
                key = (i+1).ToString();
                return true;
            }
        }

        scoreList = reverseScoreList;
        nameList = reverseNameList;
        return false;
    }

    void ScoreExport() {
        //まずbossNoのノードにレコードを登録(Push)して、生成されたキーを取得（これを１件のノード名に使う）
        //登録する１件データをDictionaryで定義(nameとtime)
        Dictionary<string, object> itemMap = new Dictionary<string, object>();
        itemMap.Add("name", nowName);
        itemMap.Add("score", nowScore);
        //１件データをさらにDictionaryに入れる。キーはノード(bossNo/さっき生成したキー)
        Dictionary<string, object> map = new Dictionary<string, object>();
        map.Add(key, itemMap);
        //データ更新！
        rankingDB.UpdateChildrenAsync(map);
    }

    public List<int> GetScoreList() { return scoreList; }
    public List<string> GetNameList() { return nameList; }
    public bool IsSortComplete() { return sortComplete; }

}
