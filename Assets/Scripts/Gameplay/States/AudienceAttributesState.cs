using System.Collections.Generic;
using UnityEngine;

using GlobalGameJam.Data;

namespace GlobalGameJam.Gameplay.States
{
    public class AudienceAttributesState : BaseState
    {
        public override GameplayStateEnum State => GameplayStateEnum.AudienceAttributes;

        [Header("Duration")]
        [SerializeField, Range(0.0f, 15.0f)] private float _sequenceLenght = 1.0f;
        private float _timer = 0.0f;

        [Header("Scene references")]
        [SerializeField] private GameObject _audienceParent = null;

        private List<AudienceMember> _audienceList = new List<AudienceMember>();
        private List<AttributeData> _attributes = new List<AttributeData>();

        private int[] roundPosiveAttributes = new int[]
            {
                2,
                3,
                3,
                3,
                3
            };

        private int[] roundNegativeAttributes = new int[]
            {
                0,
                1,
                1,
                2,
                2
            };

        private int[] roundNegativePeople = new int[]
            {
                0,
                1,
                1,
                3,
                4
            };

        protected override void InitializeState()
        {
            _attributes = DatabaseManager.Instance.Attributes;
            _audienceList.AddRange(_audienceParent.GetComponentsInChildren<AudienceMember>());
        }

        protected override void EnableState()
        {
            _timer = 0.0f;

            int currentRount = _gameplayManager.CurrentRound - 1;
            int positiveAttributeCount = roundPosiveAttributes[currentRount];
            int negativeAttributeCount = roundNegativeAttributes[currentRount];
            int negativePeople = roundNegativePeople[currentRount];

            List<AttributeData> tempList = new List<AttributeData>(_attributes);
            List<AttributeData> _possiblePosiveAttributes = new List<AttributeData>();
            List<AttributeData> _possibleNegativeAttributes = new List<AttributeData>();

            for (int i = 0; i < positiveAttributeCount; i++)
            {
                AttributeData attribute = tempList[Random.Range(0, tempList.Count)];
                tempList.Remove(attribute);

                _possiblePosiveAttributes.Add(attribute);
            }

            for (int i = 0; i < negativeAttributeCount; i++)
            {
                AttributeData attribute = tempList[Random.Range(0, tempList.Count)];
                tempList.Remove(attribute);

                _possibleNegativeAttributes.Add(attribute);
            }
 
            for (int i = 0; i < _audienceList.Count; i++)
            {
                AttributeData likedAttribute = _possiblePosiveAttributes[Random.Range(0, _possiblePosiveAttributes.Count)];
                AttributeData hatedAttribute = (Random.Range(0, 101) > 80 && negativePeople > 0) ? _possibleNegativeAttributes[Random.Range(0, _possibleNegativeAttributes.Count)] : null;

                if (hatedAttribute != null) negativePeople--;
                _audienceList[i].ShowAttributes(likedAttribute, hatedAttribute);
            }
        }

        private void Update()
        {
            _timer += Time.deltaTime;

            if (_timer >= _sequenceLenght)
            {
                EndState();
            }
        }
    }
}