import 'package:flutter/material.dart';
import 'package:spend/core/mixins/kpi_card_mixin.dart';
import 'package:spend/core/resources/color_palette.dart';
import 'package:spend/core/resources/gaps.dart';
import 'package:spend/core/utils/kpi_utils.dart';
import 'package:spend/domain/entities/key_performance_index.dart';
import 'package:spend/presentation/components/animations/animated_number_increase.dart';

class KpiCardValue extends StatelessWidget {
  final KeyPerformanceIndex kpi;

  const KpiCardValue({super.key, required this.kpi});

  @override
  Widget build(BuildContext context) {
    final String unit = KPIUtils().getValueUnitByJsonKey(kpi.name);
    // short units -> Row
    if (unit.length <= 3) {
      return Row(
        mainAxisSize: MainAxisSize.min,
        mainAxisAlignment: MainAxisAlignment.end,
        crossAxisAlignment: CrossAxisAlignment.end,
        children: [
          AnimatedNumberIncrease(
            start: double.parse(kpi.value),
            end: double.parse(kpi.value),
            duration: const Duration(seconds: 2),
            isDisabled: kpi.isDisabled,
          ),
          Gaps.hSpacing3XS,
          Text(KPIUtils().getValueUnitByJsonKey(kpi.name),
              maxLines: 2,
              overflow: TextOverflow.fade,
              style: KPICardMixin.kUnitTextStyle.copyWith(
                  color: ColorPalette.textColor
                      .withOpacity(kpi.isDisabled ? 0.7 : 1))),
        ],
      );
    }
    // Long units -> Column
    else {
      return Column(
        mainAxisAlignment: MainAxisAlignment.center,
        crossAxisAlignment: CrossAxisAlignment.end,
        mainAxisSize: MainAxisSize.min,
        children: [
          AnimatedNumberIncrease(
            start: double.parse(kpi.value),
            end: double.parse(kpi.value),
            isDisabled: kpi.isDisabled,
          ),
          Gaps.vSpacingXXS,
          Text(KPIUtils().getValueUnitByJsonKey(kpi.name),
              maxLines: 2,
              overflow: TextOverflow.fade,
              style: KPICardMixin.kUnitTextStyle.copyWith(
                  color: ColorPalette.textColor
                      .withOpacity(kpi.isDisabled ? 0.7 : 1))),
        ],
      );
    }
  }
}
