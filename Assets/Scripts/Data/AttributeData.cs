using UnityEngine;

namespace GlobalGameJam.Data
{
    [CreateAssetMenu(fileName = "attribute_", menuName = "Datafiles/New Attribute")]
    public class AttributeData : ScriptableObject, IInitializableData
    {
        public Color Color;

        public void InitializeData()
        {
        }
    }
}