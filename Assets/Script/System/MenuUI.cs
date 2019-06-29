using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FighterAcademy
{
    public class MenuUI : MonoBehaviour
    {
        #region Public Field

        public InputField ipCharName;
        public Text charNameTxt;
        public Text scoreAfterMatch;
        public Button showLeaderBoards;

        #endregion

        private void Start()
        {
            scoreAfterMatch.text = "Score : " + PlayerPrefs.GetInt("Score");
        }


        #region Public Method
        public void setActive(bool active)
        {
            ipCharName.gameObject.SetActive(active);
            charNameTxt.gameObject.SetActive(active);
        }
        public void LobbyActive(bool active)
        {
            scoreAfterMatch.gameObject.SetActive(active);
            showLeaderBoards.gameObject.SetActive(active);
        }
        public void OnShowLeaderboards()
        {
            PlayGamesController.ShowLeaderBoard();
        }
        #endregion
    }
}

