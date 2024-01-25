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
            _likedAttribute = likedAttribute;
            _hatedAttribute = hatedAttribute;

            _likedSpriteRenderer.sprite = _likedAttribute.Attribute;
            _hatedSpriteRenderer.sprite = _hatedAttribute.Attribute;

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

        }

        public void Boo()
        {

        }
    }
}