using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalGameJam.Gameplay
{
    public class ReturningMicrophone : MonoBehaviour
    {
        private Vector3 _startingPosition;
        private float _timer = 0.0f;

        [SerializeField] private Vector3 _endPosition;
        [SerializeField] private float _duration = 0.50f;
        [SerializeField] private float _speed = 0.50f;

        private void Awake()
        {
            _startingPosition = transform.position;
            gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            transform.position = _startingPosition;
            _timer = 0.0f;
        }

        private void Update()
        {
            _timer += Time.deltaTime;
            transform.position = Vector3.Slerp(_startingPosition, _endPosition, (_timer / _speed));

            if (_timer >= _duration)
            {
                gameObject.SetActive(false);
            }
        }
    }
}