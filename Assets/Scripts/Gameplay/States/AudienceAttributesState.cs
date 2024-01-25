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

        protected override void InitializeState()
        {
            _attributes = DatabaseManager.Instance.Attributes;
            _audienceList.AddRange(_audienceParent.GetComponentsInChildren<AudienceMember>());
        }

        protected override void EnableState()
        {
            _timer = 0.0f;

            for (int i = 0; i < _audienceList.Count; i++)
            {
                AttributeData likedAttribute = null;
                AttributeData hatedAttribute = null;

                List<AttributeData> tempList = new List<AttributeData>(_attributes);

                int randomID = Random.Range(0, tempList.Count);
                likedAttribute = tempList[randomID];
                tempList.RemoveAt(randomID);

                randomID = Random.Range(0, tempList.Count);
                hatedAttribute = tempList[randomID];

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