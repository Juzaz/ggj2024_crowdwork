using UnityEngine;

namespace GlobalGameJam.Data
{
    [CreateAssetMenu(fileName = "idea_", menuName = "Datafiles/New Idea")]
    public class IdeaData : ScriptableObject, IInitializableData
    {
        [Range(0, 50)] public int IdeaSpriteIndex;
        public Sprite Texture;
        public AttributeData[] Attributes;

        // Runtime
        public string IdeaTextSprite { get; private set; }

        public void InitializeData()
        {
            IdeaTextSprite = $"<sprite={IdeaSpriteIndex}>";
        }
    }
}