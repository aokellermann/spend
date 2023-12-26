import 'package:equatable/equatable.dart';
import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:get_it/get_it.dart';
import 'package:spend/data/repositories/api_repository.dart';
import 'package:spend/data/repositories/database_repository.dart';
import 'package:spend/domain/entities/key_performance_index.dart';

part 'gas_kpi_event.dart';
part 'gas_kpi_state.dart';

class GasKpiBloc extends Bloc<GasKpiEvent, GasKpiState> {
  GasKpiBloc() : super(GasKpiInitial()) {
    on<LoadGasKpi>((event, emit) async {
      emit(GasKpiLoading());
      try {
        final List<KeyPerformanceIndex> result =
            await GetIt.I.get<ApiRepository>().getGasKPIs();
        emit(GasKpiData(result));
      } on Exception catch (e) {
        emit(GasKpiError(e));
      }
    });
    on<UpdateGasKpi>((event, emit) async {
      try {
        final List<KeyPerformanceIndex> result =
            GetIt.I.get<DatabaseRepository>().getGasKPIs();
        emit(GasKpiData(result));
      } on Exception catch (e) {
        emit(GasKpiError(e));
      }
    });
  }
}
