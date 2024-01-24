using UnityEngine;

namespace GlobalGameJam.Data
{
    [CreateAssetMenu(fileName = "attribute_", menuName = "Datafiles/New Attribute")]
    public class AttributeData : ScriptableObject, IInitializableData
    {
        public Sprite Attribute;

        public void InitializeData()
        {
        }
    }
}