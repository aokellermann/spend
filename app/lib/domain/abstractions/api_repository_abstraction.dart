import 'package:spend/core/types/kpi_category.dart';
import 'package:spend/domain/entities/ampel.dart';
import 'package:spend/domain/entities/key_performance_index.dart';

abstract class ApiRepositoryAbstraction {
  // Translations
  Future<Map<String, dynamic>> getApiTranslations();

  // Ampel
  Future<List<Ampel>> getAmpel();

  // KPIs
  Future<List<KeyPerformanceIndex>> getEnergyKPIs();

  Future<List<KeyPerformanceIndex>> getGasKPIs();

  Future<List<KeyPerformanceIndex>> getPriceKPIs();

  Future<List<KeyPerformanceIndex>> getWeatherKPIs();

  Future<List<KeyPerformanceIndex>> getOverviewKPIs();

  Future<List<KeyPerformanceIndex>> getKPIsByCategory(KPICategory category);
}
