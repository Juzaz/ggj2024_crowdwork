using System.Collections.Generic;
using UnityEngine;
using GlobalGameJam.Data;

namespace GlobalGameJam.Gameplay
{
    public class Microphone : MonoBehaviour
    {
        private float _launchAngle = 1.0f;
        private float _launchForce = 3.5f;
        
        [Header("Physics settings")]
        [SerializeField, Range(0.01f, 1.0f)] private float _timeScale = 1.0f;
        [SerializeField] private float _mavity = 9.81f;

        [Header("")]
        [SerializeField] List<AudioClip> _hittedIdeaAudioList = new List<AudioClip>();

        private Vector3 _startingPosition;
        private Vector3 _force;
        private bool _forceAndAngleSet;

        List<IdeaData> _collectedIdeas = new List<IdeaData>();
        public IdeaData[] CollectedIdeas => _collectedIdeas.ToArray();

        private void OnEnable()
        {
            if (_startingPosition == Vector3.zero)
                _startingPosition = transform.position;

            if (!_forceAndAngleSet)
            {
                _launchAngle = Random.Range(0.0f, 1.0f);
                _launchForce = Random.Range(1.0f, 5.0f);
            }

            _collectedIdeas.Clear();

            _forceAndAngleSet = false;
            _force = new Vector3(_launchAngle, 1.0f, 0.0f) * _launchForce;
        }

        private void OnDisable()
        {
            transform.position = _startingPosition;
        }

        public void SetAngleAndForce(float angle, float force)
        {
            _forceAndAngleSet = true;

            _launchAngle = 1.0f - angle;
            _launchForce = 1.0f + (force * 4.0f);
        }

        private void FixedUpdate()
        {
            transform.position += _force * Time.fixedDeltaTime * _timeScale;
            _force.y -= _mavity * Time.fixedDeltaTime * _timeScale;

            if (transform.position.y <= 0.70f || transform.position.x >= 2.4f)
            {
                gameObject.SetActive(false);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            IdeaBubble collectedIdea = collision.GetComponent<IdeaBubble>();
            if (collectedIdea != null)
            {
                _collectedIdeas.Add(collectedIdea.AttachedIdea);
                collectedIdea.gameObject.SetActive(false);

                Audio.AudioManager.Instance.PlayerSFX(_hittedIdeaAudioList[Random.Range(0, _hittedIdeaAudioList.Count)]);
            }
        }
    }
}