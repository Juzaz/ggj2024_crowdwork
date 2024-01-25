using System.Collections;
using UnityEngine;

using GlobalGameJam.Data;

namespace GlobalGameJam.Gameplay
{
    public class IdeaBubble : MonoBehaviour, IInitializable
    {
        [SerializeField] private SpriteRenderer _spriteRenderer = null;

        private IdeaData _attachedIdea = null;
        public IdeaData AttachedIdea => _attachedIdea;

        public IEnumerator Initialize()
        {
            gameObject.SetActive(false);
            yield break;
        }

        public void SetIdea(IdeaData ideaData)
        {
            _attachedIdea = ideaData;

            _spriteRenderer.sprite = ideaData.Texture;
            gameObject.SetActive(true);
        }
    }
}