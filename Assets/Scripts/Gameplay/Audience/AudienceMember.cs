using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GlobalGameJam.Data;

namespace GlobalGameJam.Gameplay.Audience
{
    public class AudienceMember : MonoBehaviour, IInitializable
    {
        [Header("Attributes")]
        [SerializeField] private GameObject _attributeParent = null;
        [SerializeField] private SpriteRenderer _attributeSprite = null;

        public IEnumerator Initialize()
        {
            _attributeParent.SetActive(false);

            yield break;
        }

        public void ShowAttribute(AttributeData attribute)
        {
            _attributeSprite.sprite = attribute.Attribute;
            _attributeParent.SetActive(true);
        }

        public void HideAttribute()
        {
            _attributeParent.SetActive(false);
        }

        public void Laugh()
        {

        }

        public void Boo()
        {

        }
    }
}