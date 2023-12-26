import 'package:equatable/equatable.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:get_it/get_it.dart';
import 'package:spend/data/repositories/api_repository.dart';
import 'package:spend/domain/entities/ampel.dart';

part 'ampel_event.dart';
part 'ampel_state.dart';

class AmpelBloc extends Bloc<AmpelEvent, AmpelState> {
  AmpelBloc() : super(AmpelInitial()) {
    on<LoadAmpel>((event, emit) async {
      emit(AmpelLoading());
      try {
        final List<Ampel> result =
            await GetIt.I.get<ApiRepository>().getAmpel();
        emit(AmpelData(result));
      } on Exception catch (e) {
        emit(AmpelError(e));
      }
    });
  }
}
