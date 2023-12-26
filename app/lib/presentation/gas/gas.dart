// ignore_for_file: unused_import

import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:provider/provider.dart';
import 'package:spend/core/extensions/list_extension.dart';
import 'package:spend/core/resources/color_palette.dart';
import 'package:spend/core/types/navigation_route.dart';
import 'package:spend/core/utils/navigation/navigation_utils.dart';
import 'package:spend/core/utils/translations.dart';
import 'package:spend/domain/services/kpi_manager.dart';
import 'package:spend/presentation/components/ampel_system/ampel_system.dart';
import 'package:spend/presentation/components/ampel_system/ampel_system_placeholder.dart';
import 'package:spend/presentation/components/ampel_system/ampel_system_skeleton.dart';
import 'package:spend/presentation/components/app_bar/header.dart';
import 'package:spend/presentation/components/reorderable_grid_view/kpi_grid_view.dart';
import 'package:spend/presentation/components/reorderable_grid_view/kpi_grid_view_placeholder.dart';
import 'package:spend/presentation/components/reorderable_grid_view/kpi_grid_view_skeleton.dart';
import 'package:spend/presentation/components/reorderable_grid_view/reorderable_grid_view.dart';
import 'package:spend/presentation/gas/blocs/gas_kpi_bloc.dart';
import 'package:spend/presentation/overview/blocs/ampel/ampel_bloc.dart';

class Gas extends StatefulWidget {
  const Gas({super.key});

  @override
  State<Gas> createState() => _GasState();
}

class _GasState extends State<Gas> {
  late final KPIManager manager;

  @override
  void initState() {
    super.initState();
    context.read<AmpelBloc>().add(LoadAmpel());
    context.read<GasKpiBloc>().add(LoadGasKpi());
    manager = context.read<KPIManager>()..setUnavailable();
  }

  @override
  Widget build(BuildContext context) {
    return WillPopScope(
      onWillPop: () async {
        if (manager.editorMode) {
          manager.discardChanges(context);
        } else {
          Navigation.goToOverview();
        }
        return false;
      },
      child: Scaffold(
        backgroundColor: ColorPalette.websiteBgColor,
        appBar: Header(
          title: Translations.of(context)!.text('dashboard.gas.context-name'),
          showEditButton: true,
        ),
        body: SafeArea(
          child: ListView(
            children: [
              Selector<KPIManager, bool>(
                  selector: (_, manager) => manager.editorMode,
                  builder: (context, edit, _) {
                    return BlocBuilder<AmpelBloc, AmpelState>(
                      builder: (context, data) {
                        if (data is AmpelInitial) {
                          return const AmpelSystemPlaceholder();
                        } else if (data is AmpelLoading) {
                          return const AmpelSystemSkeleton();
                        } else if (data is AmpelData) {
                          if (data.result.isNotEmpty && !edit) {
                            return AmpelSystem(ampel: data.result.second!);
                          } else {
                            return const SizedBox();
                          }
                        } else if (data is AmpelError) {
                          return const SizedBox();
                        }
                        return const AmpelSystemPlaceholder();
                      },
                    );
                  }),
              BlocBuilder<GasKpiBloc, GasKpiState>(
                builder: (context, data) {
                  if (data is GasKpiInitial) {
                    return const KPIGridViewPlaceholder(
                        route: NavigationRoute.gas);
                  } else if (data is GasKpiLoading) {
                    return const KPIGridViewSkeleton(
                        route: NavigationRoute.gas);
                  } else if (data is GasKpiData) {
                    if (data.result.isNotEmpty) {
                      return KPIGridView(
                          kpis: data.result, route: NavigationRoute.gas);
                    } else {
                      return const SizedBox();
                    }
                  } else if (data is GasKpiError) {
                    return const SizedBox();
                  }
                  return const SizedBox();
                },
              ),
            ],
          ),
        ),
      ),
    );
  }
}
