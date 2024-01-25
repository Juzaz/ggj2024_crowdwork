using UnityEngine;

namespace GlobalGameJam.Gameplay
{
    public class AngleAndForceControls : MonoBehaviour
    {
        public float Angle => _angleValue;
        public float Force => _forceValue;

        [SerializeField] private SpriteRenderer _angleControls = null;
        [SerializeField] private SpriteRenderer _forceControls = null;

        private float _angleValue = 0.0f;
        private float _forceValue = 0.0f;

        bool _settingAngle = false;
        bool _settingForce = false;
        int _incrementValue = 1;

        private void OnEnable()
        {
            _angleControls.gameObject.SetActive(true);
            _forceControls.gameObject.SetActive(false);

            _angleValue = 0.0f;
            _forceValue = 0.0f;

            _incrementValue = 1;
            _settingAngle = true;
            _settingForce = false;
        }

        private void OnDisable()
        {
            _angleControls.gameObject.SetActive(false);
            _forceControls.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (_settingAngle)
            {
                updateValue(ref _angleValue);
                _angleControls.transform.localEulerAngles = new Vector3(0.0f, 0.0f, _angleValue * 90.0f);
            }
            if (_settingForce)
            {
                updateValue(ref _forceValue);
                _forceControls.transform.localScale = Vector3.one * _forceValue;
            }

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                _settingAngle = false;
                _settingForce = true;

                _forceControls.gameObject.SetActive(true);
            }
            else if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                _settingAngle = false;
                _settingForce = false;

                gameObject.SetActive(false);
            }
        }

        private void updateValue(ref float value)
        {
            value += _incrementValue * Time.deltaTime;

            if (value >= 1.0f)
            {
                value = 1.0f;
                _incrementValue = -1;
            }
            else if (value <= 0.0f)
            {
                value = 0.0f;
                _incrementValue = 1;
            }
        }
    }
}