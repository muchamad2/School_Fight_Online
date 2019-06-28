using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FighterAcademy
{
    public class PlayerUI : MonoBehaviour
    {
        #region Private Field
        [SerializeField]
        private Text playerNameText;

        [SerializeField]
        private Slider playerHealthSlider;

        private PlayerOnNetwork _target;

        float characterControlHeight = 0f;
        Transform targetTransform;
        Renderer targetRenderer;
        Vector3 targetPosition;
        #endregion

        #region Public Field
        [SerializeField]
        private Vector3 screenOffset = new Vector3(0f, 30f, 0f);
        #endregion

        #region Monobehaviour Callbacks
        private void Awake()
        {
            this.transform.SetParent(GameObject.Find("UI Canvas").GetComponent<Transform>(), false);
        }
        private void Update()
        {
            if(playerHealthSlider != null)
            {
                playerHealthSlider.value = _target.healthCondition.health;
            }
            if(_target == null)
            {
                Destroy(this.gameObject);
                return;
            }
        }
        private void LateUpdate()
        {
            if(targetRenderer != null)
            {
                this.gameObject.SetActive(targetRenderer.isVisible);
            }
            if(targetTransform != null)
            {
                targetPosition = targetTransform.position;
                targetPosition.y += characterControlHeight;
                this.transform.position = Camera.main.WorldToScreenPoint(targetPosition) + screenOffset;

            }
        }
        #endregion

        #region public Method
        public void SetTarget(PlayerOnNetwork target)
        {
            if(target == null)
            {
                return;
            }
            _target = target;
            if(playerNameText != null)
            {
                playerNameText.text = _target.photonView.Owner.NickName;
            }

            targetTransform = this._target.GetComponent<Transform>();
            targetRenderer = this._target.GetComponent<Renderer>();
            CharacterController charControl = target.GetComponent<CharacterController>();

            if(charControl != null)
            {
                characterControlHeight = charControl.height;
            }
        }
        #endregion
    }
}

