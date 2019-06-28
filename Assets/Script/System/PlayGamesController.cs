using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.UI;


namespace FighterAcademy
{
    public class PlayGamesController : MonoBehaviour
    {
        public static PlayGamesController Instance;
        // Start is called before the first frame update
        void Start()
        {
            if(Instance == null)
                Instance = this;

            UserAuthentication();
        }

        void UserAuthentication()
        {
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
            PlayGamesPlatform.InitializeInstance(config);
            PlayGamesPlatform.Activate();
            Social.localUser.Authenticate((bool success) =>
            {
                if(success == true)
                {
                    Debug.Log("logged in googleplay games services : " + Social.localUser);
                    
                    NetConnectManager.Instance.ConnectedToMaster(Social.localUser.userName);
                    
                }
                else
                {
                    Debug.LogError("Unable to sign in to Play Games");
                }

            });
        }

        public void PostLeaderBoards(long newScore)
        {
            Social.ReportScore(newScore, GPGSIds.leaderboard_rank, (bool success) =>
              {
                  if(success == true)
                  {
                      Debug.Log("Post leader boards");
                  }
                  else
                  {
                      Debug.LogError("Unable to post new score");
                  }
              });
        }

        public static void ShowLeaderBoard()
        {
            PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSIds.leaderboard_rank);
        }
        // Update is called once per frame
        void Update()
        {

        }
    }
}

