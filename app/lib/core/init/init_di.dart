import 'package:get_it/get_it.dart';
import 'package:spend/core/helpers/api_base_helper.dart';
import 'package:spend/core/helpers/graph_base_helper.dart';
import 'package:spend/core/helpers/web_base_helper.dart';
import 'package:spend/core/utils/localization.dart';
import 'package:spend/data/repositories/api_repository.dart';
import 'package:spend/data/repositories/auth_repository.dart';
import 'package:spend/data/repositories/database_repository.dart';
import 'package:spend/data/repositories/graph_repository.dart';
import 'package:spend/domain/services/kpi_manager.dart';

// register all singletons for service locator
initDI() async {
  GetIt.I.registerSingleton(GraphRepository());
  GetIt.I.registerSingleton(GraphBaseHelper());
  GetIt.I.registerSingleton(ApiRepository());
  GetIt.I.registerSingleton(ApiBaseHelper());
  GetIt.I.registerSingleton(WebBaseHelper());
  GetIt.I.registerSingleton(AppLocalization());
  GetIt.I.registerSingleton(DatabaseRepository());
  GetIt.I.registerSingleton(AuthRepository());
  GetIt.I.registerLazySingleton(() => KPIManager()); // keep lazy
}
