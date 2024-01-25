using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GlobalGameJam.Data;

namespace GlobalGameJam.Gameplay
{
    public class IdeaBubble : MonoBehaviour, IInitializable
    {
        [SerializeField] private SpriteRenderer _spriteRenderer = null;

        public IEnumerator Initialize()
        {
            gameObject.SetActive(false);
            yield break;
        }

        public void SetIdea(IdeaData ideaData)
        {
            _spriteRenderer.sprite = ideaData.Texture;
            gameObject.SetActive(true);
        }
    }
}