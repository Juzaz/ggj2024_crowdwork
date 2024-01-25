using System.Collections;
using UnityEngine;

using GlobalGameJam.Data;

namespace GlobalGameJam.Gameplay
{
    public class AudienceMember : MonoBehaviour, IInitializable
    {
        public AttributeData LikedAttribute => _likedAttribute;
        public AttributeData HatedAttribute => _hatedAttribute;

        [Header("References")]
        [SerializeField] private Animator _animator = null;
        [SerializeField] private SpriteRenderer _likedSpriteRenderer = null;
        [SerializeField] private SpriteRenderer _hatedSpriteRenderer = null;

        private AttributeData _likedAttribute = null;
        private AttributeData _hatedAttribute = null;

        public IEnumerator Initialize()
        {
            HideAttributes();

            yield break;
        }

        public void ShowAttributes(AttributeData likedAttribute, AttributeData hatedAttribute)
        {
            ResetState();

            _likedAttribute = likedAttribute;
            _hatedAttribute = hatedAttribute;

            _likedSpriteRenderer.GetComponentInParent<SpriteRenderer>().color = _likedAttribute.Color;
            _hatedSpriteRenderer.GetComponentInParent<SpriteRenderer>().color = _hatedAttribute.Color;

            _likedSpriteRenderer.gameObject.SetActive(true);
            _hatedSpriteRenderer.gameObject.SetActive(true);
        }

        public void HideAttributes()
        {
            _likedSpriteRenderer.gameObject.SetActive(false);
            _hatedSpriteRenderer.gameObject.SetActive(false);
        }

        public void Laugh()
        {
            _animator.SetTrigger("Good");
            HideAttributes();
        }

        public void Boo()
        {
            _animator.SetTrigger("Bad");
            HideAttributes();
        }

        public void ResetState()
        {
            _animator.SetTrigger("Reset");
            HideAttributes();
        }
    }
}