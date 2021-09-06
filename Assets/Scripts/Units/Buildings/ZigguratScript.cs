using UnityEngine;
using Ziggurat.Configuration;

namespace Ziggurat.Units
{
    public class ZigguratScript : BaseManufacture
    {
        public override string Name => "Ziggurat";

        [Header("Ссылка на конфигурационный файл"), SerializeField]
        private BaseUnitStats _config;

        public override StatsData GetStats() => _config.AllProperties;

        public void SetStats(StatsData data) => _config.SetStats(data);
    }
}