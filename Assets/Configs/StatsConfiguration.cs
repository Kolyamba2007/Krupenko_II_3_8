using OneLine;
using UnityEngine;
using Ziggurat.Units;

namespace Ziggurat.Configuration
{
	[CreateAssetMenu(fileName = "NewStatsConfiguration", menuName = "Configurations/StatsConfiguration")]
	public class StatsConfiguration : ScriptableObject
	{
		[SerializeField, Tooltip("Тип юнита"), OneLine, Highlight(0.05f, 0.72f, 0.62f)]
		private UnitType _unitType;
		[SerializeField, OneLineWithHeader, HideLabel, Highlight(0f, 0f, 1f), Separator("[ Базовые параметры персонажа ]")]
		private BaseParamsData _baseParams = BaseParamsData.Empty;
		[SerializeField, OneLineWithHeader, HideLabel, Highlight(0f, 1f, 0f), Separator("[ Параметры мобильности юнитов ]")]
		private MobilityParamsData _mobilityParams = MobilityParamsData.Empty;
		[SerializeField, OneLineWithHeader, HideLabel, Highlight(1f, 0f, 0f), Separator("[ Боевые параметры юнитов ]")]
		private BattleParamsData _battleParams = BattleParamsData.Empty;
		[SerializeField, OneLineWithHeader, HideLabel, Highlight(0f, 1f, 1f), Separator("[ Вероятности событий у юнитов ]")]
		private ProbabilityParamsData _probabilityParams = ProbabilityParamsData.Empty;

		/// <summary>
		/// Возвращает тип юнита
		/// </summary>
		public ref UnitType UnitType => ref _unitType;
		/// <summary>
		/// Возвращает свойства юнита
		/// </summary>
		public StatsData AllProperties => new StatsData { BaseParams = _baseParams, BattleParams = _battleParams, MobilityParams = _mobilityParams, ProbabilityParams = _probabilityParams };
		/// <summary>
		/// Возвращает базовые параметры юнита
		/// </summary>
		public BaseParamsData BaseParams => _baseParams;
		/// <summary>
		/// Возвращает параметры мобильности юнита
		/// </summary>
		public MobilityParamsData MobilityParams => _mobilityParams;
		/// <summary>
		/// Возвращает боевые параметры юнита
		/// </summary>
		public BattleParamsData BattleParams => _battleParams;
		/// <summary>
		/// Возвращает параметры вероятностей юнита
		/// </summary>
		public ProbabilityParamsData ProbabilityParams => _probabilityParams;
	}
}