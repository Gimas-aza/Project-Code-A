using UnityEngine;

namespace Assets.Units.Player
{
    [RequireComponent(typeof(LineRenderer))]
    public class LaserPointer : MonoBehaviour
    {
        [SerializeField] private Transform _laserOrigin;
        [SerializeField] private float _gunRange = 50f;
    
        private LineRenderer _laserLine;
    
        private void OnValidate()
        {
            _laserOrigin ??= GetComponent<Transform>();
        }

        private void Awake()
        {
            _laserLine = GetComponent<LineRenderer>();
            SetActivateLaser(false);
        }
    
        private void Update()
        {
            _laserLine.SetPosition(0, _laserOrigin.position);

            RaycastHit hit;
            if(Physics.Raycast(_laserOrigin.position, _laserOrigin.forward, out hit, _gunRange))
                _laserLine.SetPosition(1, hit.point);
            else
                _laserLine.SetPosition(1, _laserOrigin.position + (_laserOrigin.forward * _gunRange));
        }

        public void SetActivateLaser(bool value)
        {
            _laserLine.enabled = value;
        }
    }
}