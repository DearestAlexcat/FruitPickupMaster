using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using DG.Tweening;

namespace Client
{
    public abstract class Unit : MonoBehaviour
    {
        [SerializeField] protected Animator animator;

        [field: SerializeField] public Transform PopupPointer { get; private set; }

        [Header("TASK VIEW")]
        [SerializeField] Image levelTaskImage;
        [SerializeField] TMP_Text levelTaskText;

        [Header("OTHER LINKS")]
        public Basket basket;

        [Header("ROPE")]
        [SerializeField] GrapplingRope gr;

        [Header("RIGGING")]
        public PlayerRiggingManager riggingManager;

        [Header("PARTICLES")]
        public ParticleSystem pistolFireFX;
        public ParticleSystem addToCartFX;
        
        private void Start()
        {
            riggingManager.InitializeIK();
        }

        public abstract void PlayAnimation(ValueType flag);

        public GrapplingRope GetGrapplingRope => gr;

        public void ForwardRope(float deltaTime)
        {
            gr.ForwardRope(deltaTime);
        }

        public void BackwardRope(float deltaTime)
        {
            gr.BackwardRope(Time.deltaTime);        // Render of rope in reverse direction
            gr.PullGrapplingThing();                // Drag an object
        }

        public void ReplaceTaskOnEmoji(GameObject emojiPrefab)
        {
            DOTween.Sequence()
                   .Append(SetActiveTaskHolder(false))
                   .Append(SetActiveEmoji(emojiPrefab));
        }

        public Tween SetActiveEmoji(GameObject emojiPrefab)
        {
            GameObject emojiObj = Instantiate(emojiPrefab, Vector3.zero, Quaternion.identity, PopupPointer);
            
            emojiObj.transform.localPosition = emojiObj.transform.localScale = Vector3.zero;
           
            return emojiObj.transform.DOScale(0.7f, 0.5f)
                                     .SetEase(Ease.OutBack)
                                     .SetLink(emojiObj);
        }

        public Tween SetActiveTaskHolder(bool value)
        {
            if (value)
            {
                levelTaskImage.transform.parent.parent.localScale = Vector3.zero;

                return levelTaskImage.transform.parent.parent.DOScale(1f, 0.5f)
                                                             .SetEase(Ease.OutBack)
                                                             .SetLink(levelTaskImage.transform.parent.parent.gameObject);
            }
            else
            {
                return levelTaskImage.transform.parent.parent.DOScale(0f, 0.5f)
                                                             .SetEase(Ease.InBack)
                                                             .SetLink(levelTaskImage.transform.parent.parent.gameObject);
            }
        }

        public void SetTaskImage(Sprite sprite)
        {
            levelTaskImage.sprite = sprite;
        }

        public void SetTaskText(int collectionProgress)
        {
            levelTaskText.text = collectionProgress.ToString();
        }

        public void PlayPistolFireFx()
        {
            pistolFireFX?.Play(true);
        }

        public void PlayAddToCartFX()
        {
            addToCartFX?.Play(true);
        }
    }
}
