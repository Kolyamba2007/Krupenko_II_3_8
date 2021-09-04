using UnityEngine;
using UnityEngine.UI;

namespace Ziggurat.Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        private Slider _cameraSpeedSlider;
        [SerializeField]
        private Slider _mouseSensivitySlider;

        public void Init(CameraController camera)
        {
            _cameraSpeedSlider.value = camera.Speed;
            _mouseSensivitySlider.value = camera.Sensivity;
        }
    }
}