using UnityEngine;
using UnityEngine.UI;
using Ziggurat.Units;

namespace Ziggurat.Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        private Slider _cameraSpeedSlider;
        [SerializeField]
        private Slider _mouseSensivitySlider;

        [SerializeField]
        private Slider HealthSlider, DamageRatioSlider;
        [SerializeField]
        private Text HealthText, DamageRatioText;

        [SerializeField]
        Animator panelAnim;

        public void Init(CameraController camera)
        {
            _cameraSpeedSlider.value = camera.Speed;
            _mouseSensivitySlider.value = camera.Sensivity;
        }

        public void HealthTextChanging() => HealthText.text = HealthSlider.value.ToString();
        public void DamageRatioTextChanging() => DamageRatioText.text = DamageRatioSlider.value.ToString() + ":" + (100 - DamageRatioSlider.value).ToString() + " %";

        public void ShowUnitPanel()
        {
            panelAnim.SetBool("IsEnabled", !(panelAnim.GetBool("IsEnabled")));
        }

        public void ChangingUnitParam(BaseUnit unit)
        {
            if (unit is ZigguratScript)
            {
                HealthSlider.value = unit.Health;
            }
        }
    }
}