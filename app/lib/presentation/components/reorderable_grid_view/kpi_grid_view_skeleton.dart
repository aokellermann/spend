import 'package:flutter/material.dart';
import 'package:reorderables/reorderables.dart';
import 'package:spend/core/mixins/kpi_card_mixin.dart';
import 'package:spend/core/resources/color_palette.dart';
import 'package:spend/core/resources/constants.dart';
import 'package:spend/core/resources/paddings.dart';
import 'package:spend/core/types/navigation_route.dart';
import 'package:spend/presentation/components/kpi_card/kpi_card.dart';
import 'package:spend/presentation/components/shimmer.dart';

/// This is the skeleton widget for the KPI Grid View
///
/// It is shown while the blocbuilder state is Loading
/// It creates a shimmer effect for each KPI tile
class KPIGridViewSkeleton extends StatelessWidget with KPICardMixin {
  final NavigationRoute route;

  const KPIGridViewSkeleton({super.key, required this.route});

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.all(Paddings.paddingS),
      child: AbsorbPointer(
        absorbing: true,
        child: ReorderableWrap(
          spacing: Constants.kGridViewSpacing,
          runSpacing: Constants.kGridViewRunSpacing,
          onReorder: (_, __) {},
          enableReorder: false,
          scrollPhysics: const NeverScrollableScrollPhysics(),
          children: initialKPIs(route)
              .map(
                (k) => Shimmer(
                  baseColor: ColorPalette.shimmerBaseColor,
                  highlightColor: ColorPalette.shimmerHighlightColor,
                  child: KpiCard(
                    kpi: k,
                    currentRoute: route,
                    editorMode: false,
                  ),
                ),
              )
              .toList(),
        ),
      ),
    );
  }
}
