// Created on 16/12/2022
// Updated on 19/12/2022
using UnityEngine;

namespace FruitChop
{
    public class Blade : MonoBehaviour
    {

        public Vector3 force;
        public Vector3 direction { get; private set; }

        private Camera mainCamera;

        private Collider sliceCollider;
        [SerializeField] private TrailRenderer sliceTrail;

        [SerializeField] private float minSliceVelocity = 0.01f;
        private Vector3 startPos;
        private bool alreadyPlayed;

        [SerializeField] private Vector3 maxForce;

        private void Awake()
        {
            alreadyPlayed = false;
            mainCamera = Camera.main;
            sliceCollider = GetComponent<Collider>();
        }
        private void OnEnable()
        {
            StopSliceMobile();
        }

        private void OnDisable()
        {
            StopSliceMobile();
        }

        #region Touch Input
        /// <summary>
        /// It changes the blade position by taking a position parameter when the Input has begun
        /// </summary>
        /// <param name="pos">it is the current position of the touch to convert to according to world space</param>
        public void StartSliceMobile(Vector3 pos)
        {
            Vector3 position = mainCamera.ScreenToWorldPoint(pos);
            position.z = 0f;
            transform.position = position;

            startPos = position;

            sliceCollider.enabled = false;
            sliceTrail.enabled = true;
            sliceTrail.Clear();
        }

        /// <summary>
        /// It changes the blade position by taking contunuous position when the Input is being moved
        /// </summary>
        /// <param name="pos">it is the current position of the touch to convert to according to world space</param>
        public void ContinueSliceMobile(Vector3 pos)
        {
            Vector3 newPosition = mainCamera.ScreenToWorldPoint(pos);
            newPosition.z = 0f;

            direction = newPosition - transform.position;

            float velocity = direction.magnitude / Time.deltaTime;
            sliceCollider.enabled = velocity > minSliceVelocity;

            force = startPos - newPosition;
            force.z = force.magnitude;

            if (force.z > maxForce.z)
            {
                if (!alreadyPlayed)
                {
                    AudioManager.Instance.Play(AudioData.EAudio.BladeSwing, 1, false, AudioManager.AudioType.Sound);
                    alreadyPlayed = true;
                }
            }
            else
            {
                alreadyPlayed = false;
            }
            transform.position = newPosition;
        }

        /// <summary>
        /// It disables to collider and trail so no triggers are called
        /// </summary>
        public void StopSliceMobile()
        {
            sliceCollider.enabled = false;
            sliceTrail.enabled = false;
        }
        #endregion
    }
}