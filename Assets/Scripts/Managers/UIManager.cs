using UnityEngine;
using UnityEngine.UI;
using Ziggurat.Units;
using System;

namespace Ziggurat.Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        private Slider _cameraSpeedSlider;
        [SerializeField]
        private Slider _mouseSensivitySlider;

        [SerializeField]
        private Slider HealthSlider, DamageRatioSlider, MovementSpeedSlider, FastAttackSlider, SlowAttackSlider, DoubleDamageSlider, MissSlider;
        [SerializeField]
        private Text HealthText, DamageRatioText, MovementSpeedText, FastAttackText, SlowAttackText, DoubleDamageText, MissText;

        [SerializeField]
        Animator panelAnim;

        private ZigguratScript CurrentZiggurat;

        public void Init(CameraController camera)
        {
            _cameraSpeedSlider.value = camera.Speed;
            _mouseSensivitySlider.value = camera.Sensivity;
        }

        
        public void UpdateParam(string str)
        {
            var unit = (GameManager.SelectedUnit as ZigguratScript);
            if (unit == null) return;
            StatsData data = unit.GetStats();

            switch (str)
            {
                case "Health":
                    data.BaseParams.MaxHealth = (ushort)HealthSlider.value;
                    HealthText.text = HealthSlider.value.ToString();
                    break;
                case "Speed":
                    data.MobilityParams.MoveSpeed = MovementSpeedSlider.value;
                    MovementSpeedText.text = MovementSpeedSlider.value.ToString() + " m/s";
                    break;
                case "FastAttack":
                    data.BattleParams.FastAttackDamage = (ushort)FastAttackSlider.value;
                    FastAttackText.text = FastAttackSlider.value.ToString();
                    break;
                case "SlowAttack":
                    data.BattleParams.StrongAttackDamage = (ushort)SlowAttackSlider.value;
                    SlowAttackText.text = SlowAttackSlider.value.ToString();
                    break;
                case "MissChance":
                    data.ProbabilityParams.MissChance = MissSlider.value / 100;
                    MissText.text = MissSlider.value.ToString() + " %";
                    break;
                case "DoubleDamageChance":
                    data.ProbabilityParams.CriticalChance = DoubleDamageSlider.value / 100;
                    DoubleDamageText.text = DoubleDamageSlider.value.ToString() + " %";
                    break;
                case "DamageRatio":
                    data.ProbabilityParams.StrongAttackChance = DamageRatioSlider.value / 100;
                    DamageRatioText.text = DamageRatioSlider.value.ToString() + ":" + (100 - DamageRatioSlider.value).ToString() + " %";
                    break;
                default: throw new Exception();
            }

            unit.SetStats(data);
        }

        public void ShowUnitPanel()
        {
            panelAnim.SetBool("IsEnabled", !(panelAnim.GetBool("IsEnabled")));
        }

        public void ChangingUnitParam(BaseUnit unit)
        {
            if (unit is BaseMelee)
            {
                HealthSlider.interactable = false;
                MovementSpeedSlider.interactable = false;
                FastAttackSlider.interactable = false;
                SlowAttackSlider.interactable = false;
                MissSlider.interactable = false;
                DoubleDamageSlider.interactable = false;
                DamageRatioSlider.interactable = false;

                StatsData data = unit.GetStats();
                HealthSlider.value = data.BaseParams.MaxHealth;
                MovementSpeedSlider.value = data.MobilityParams.MoveSpeed;
                FastAttackSlider.value = data.BattleParams.FastAttackDamage;
                SlowAttackSlider.value = data.BattleParams.StrongAttackDamage;
                MissSlider.value = data.ProbabilityParams.MissChance * 100;
                DoubleDamageSlider.value = data.ProbabilityParams.CriticalChance * 100;
                DamageRatioSlider.value = data.ProbabilityParams.StrongAttackChance * 100;
            }

            if (unit is ZigguratScript)
            {
                HealthSlider.interactable = true;
                MovementSpeedSlider.interactable = true;
                FastAttackSlider.interactable = true;
                SlowAttackSlider.interactable = true;
                MissSlider.interactable = true;
                DoubleDamageSlider.interactable = true;
                DamageRatioSlider.interactable = true;

                StatsData data = unit.GetStats();
                HealthSlider.value = data.BaseParams.MaxHealth;
                MovementSpeedSlider.value = data.MobilityParams.MoveSpeed;
                FastAttackSlider.value = data.BattleParams.FastAttackDamage;
                SlowAttackSlider.value = data.BattleParams.StrongAttackDamage;
                MissSlider.value = data.ProbabilityParams.MissChance * 100;
                DoubleDamageSlider.value = data.ProbabilityParams.CriticalChance * 100;
                DamageRatioSlider.value = data.ProbabilityParams.StrongAttackChance * 100;
            }
        }
    }
}