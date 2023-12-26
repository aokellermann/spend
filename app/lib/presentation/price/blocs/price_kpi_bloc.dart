import 'package:equatable/equatable.dart';
import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:get_it/get_it.dart';
import 'package:spend/data/repositories/api_repository.dart';
import 'package:spend/data/repositories/database_repository.dart';
import 'package:spend/domain/entities/key_performance_index.dart';

part 'price_kpi_event.dart';
part 'price_kpi_state.dart';

class PriceKpiBloc extends Bloc<PriceKpiEvent, PriceKpiState> {
  PriceKpiBloc() : super(PriceKpiInitial()) {
    on<LoadPriceKpi>((event, emit) async {
      emit(PriceKpiLoading());
      try {
        final List<KeyPerformanceIndex> result =
            await GetIt.I.get<ApiRepository>().getPriceKPIs();
        emit(PriceKpiData(result));
      } on Exception catch (e) {
        emit(PriceKpiError(e));
      }
    });
    on<UpdatePriceKpi>((event, emit) async {
      try {
        final List<KeyPerformanceIndex> result =
            GetIt.I.get<DatabaseRepository>().getPriceKPIs();
        emit(PriceKpiData(result));
      } on Exception catch (e) {
        emit(PriceKpiError(e));
      }
    });
  }
}
