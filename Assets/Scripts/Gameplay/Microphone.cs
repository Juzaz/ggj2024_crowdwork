using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalGameJam.Gameplay
{
    public class Microphone : MonoBehaviour
    {
        [SerializeField, Range(0.0f, 1.0f)] private float _launchAngle = 1.0f;
        [SerializeField, Range(1.0f, 5.0f)] private float _launchForce = 3.5f;
        [SerializeField] private float _mavity = 9.81f;
        [SerializeField, Range(0.01f, 1.0f)] private float _timeScale = 1.0f;

        private Vector3 _startingPosition;
        private Vector3 _force;

        private void Awake()
        {
            _startingPosition = transform.position;
            gameObject.SetActive(false);
        }

        private void OnEnable()
        {
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
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("OnTriggerEnter2D: " + collision.name);
        }
    }
}