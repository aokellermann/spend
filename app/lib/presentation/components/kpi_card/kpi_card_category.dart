import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:spend/core/resources/color_palette.dart';
import 'package:spend/core/resources/gaps.dart';
import 'package:spend/core/types/kpi_category.dart';
import 'package:spend/core/utils/kpi_utils.dart';
import 'package:spend/domain/entities/key_performance_index.dart';

class KpiCardCategory extends StatelessWidget {
  final KeyPerformanceIndex kpi;

  const KpiCardCategory({super.key, required this.kpi});

  @override
  Widget build(BuildContext context) {
    return Row(
      mainAxisAlignment: MainAxisAlignment.start,
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        SvgPicture.asset(kpi.category.iconAssetPath,
            width: kpi.category == KPICategory.weather ? 14 : 12.5,
            colorFilter: const ColorFilter.mode(
                ColorPalette.textColor, BlendMode.srcIn)),
        Gaps.hSpacingXXS,
        Flexible(
          fit: FlexFit.loose,
          child: Text(
            KPIUtils.getCategoryTitle(kpi.category, context),
            style: const TextStyle(
                fontWeight: FontWeight.bold,
                fontSize: 11.55,
                color: ColorPalette.textColor),
          ),
        ),
      ],
    );
  }
}
