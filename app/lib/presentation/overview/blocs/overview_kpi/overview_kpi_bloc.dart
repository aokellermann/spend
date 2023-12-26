import 'package:equatable/equatable.dart';
import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:get_it/get_it.dart';
import 'package:spend/data/repositories/api_repository.dart';
import 'package:spend/data/repositories/database_repository.dart';
import 'package:spend/domain/entities/key_performance_index.dart';

part 'overview_kpi_event.dart';
part 'overview_kpi_state.dart';

class OverviewKpiBloc extends Bloc<OverviewKpiEvent, OverviewKpiState> {
  OverviewKpiBloc() : super(OverviewKpiInitial()) {
    on<LoadOverviewKpi>((event, emit) async {
      emit(OverviewKpiLoading());
      try {
        final List<KeyPerformanceIndex> result =
            await GetIt.I.get<ApiRepository>().getOverviewKPIs();
        emit(OverviewKpiData(result));
      } on Exception catch (e) {
        emit(OverviewKpiError(e));
      }
    });
    on<UpdateOverviewKpi>((event, emit) async {
      try {
        final List<KeyPerformanceIndex> result =
            GetIt.I.get<DatabaseRepository>().getOverviewKPIs();
        emit(OverviewKpiData(result));
      } on Exception catch (e) {
        emit(OverviewKpiError(e));
      }
    });
  }
}
