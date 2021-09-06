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
        private Slider HealthSlider, DamageRatioSlider, MovementSpeedSlider;
        [SerializeField]
        private Text HealthText, DamageRatioText, MovementSpeedText;

        [SerializeField]
        Animator panelAnim;

        private ZigguratScript CurrentZiggurat;

        public void Init(CameraController camera)
        {
            _cameraSpeedSlider.value = camera.Speed;
            _mouseSensivitySlider.value = camera.Sensivity;
        }

        public void HealthTextChanging()
        {
            var unit = (GameManager.SelectedUnit as ZigguratScript);
            StatsData data = unit.GetStats();
            data.BaseParams.MaxHealth = (ushort)HealthSlider.value;

            HealthText.text = HealthSlider.value.ToString();
            unit.SetStats(data);
        }
        public void DamageRatioTextChanging()
        {
            var unit = (GameManager.SelectedUnit as ZigguratScript);
            StatsData data = unit.GetStats();
            data.BaseParams.MaxHealth = (ushort)DamageRatioSlider.value;

            DamageRatioText.text = DamageRatioSlider.value.ToString() + ":" + (100 - DamageRatioSlider.value).ToString() + " %";
            unit.SetStats(data);
        }
        public void SpeedTextChanging()
        {
            var unit = (GameManager.SelectedUnit as ZigguratScript);
            StatsData data = unit.GetStats();
            data.MobilityParams.MoveSpeed = (ushort)MovementSpeedSlider.value;

            MovementSpeedText.text = MovementSpeedSlider.value.ToString() + " m/s";
            unit.SetStats(data);
        }

        public void ShowUnitPanel()
        {
            panelAnim.SetBool("IsEnabled", !(panelAnim.GetBool("IsEnabled")));
        }

        public void ChangingUnitParam(BaseUnit unit)
        {
            //if (unit is BaseMelee)
            //{

            //}

            //if (unit is BaseManufacture)
            //{

            //}

            if (unit is ZigguratScript)
            {
                StatsData data = unit.GetStats();
                HealthSlider.value = data.BaseParams.MaxHealth;
                MovementSpeedSlider.value = data.MobilityParams.MoveSpeed;
            }
        }
    }
}