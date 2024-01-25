using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalGameJam.Gameplay
{
    public class Microphone : MonoBehaviour
    {
        private float _launchAngle = 1.0f;
        private float _launchForce = 3.5f;
        
        [Header("Physics settings")]
        [SerializeField, Range(0.01f, 1.0f)] private float _timeScale = 1.0f;
        [SerializeField] private float _mavity = 9.81f;

        private Vector3 _startingPosition;
        private Vector3 _force;

        private void OnEnable()
        {
            if (_startingPosition != Vector3.zero)
                _startingPosition = transform.position;

            _launchAngle = Random.Range(0.0f, 1.0f);
            _launchForce = Random.Range(1.0f, 5.0f);

            _force = new Vector3(_launchAngle, 1.0f, 0.0f) * _launchForce;
        }

        private void OnDisable()
        {
            transform.position = _startingPosition;
        }

        private void FixedUpdate()
        {
            transform.position += _force * Time.fixedDeltaTime * _timeScale;
            _force.y -= _mavity * Time.fixedDeltaTime * _timeScale;

            if (transform.position.y <= 0.90f || transform.position.x >= 2.2f)
            {
                gameObject.SetActive(false);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("OnTriggerEnter2D: " + collision.name);
            IdeaBubble collectedIdea = collision.GetComponent<IdeaBubble>();

            if (collectedIdea != null)
            {
                collectedIdea.gameObject.SetActive(false);
            }
        }
    }
}