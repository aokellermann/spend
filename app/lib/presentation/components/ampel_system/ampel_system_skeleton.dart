import 'package:flutter/material.dart';
import 'package:spend/core/resources/color_palette.dart';
import 'package:spend/core/types/ampel_level.dart';
import 'package:spend/core/types/ampel_type.dart';
import 'package:spend/domain/entities/ampel.dart';
import 'package:spend/presentation/components/ampel_system/ampel_system.dart';
import 'package:spend/presentation/components/shimmer.dart';

class AmpelSystemSkeleton extends StatelessWidget {
  const AmpelSystemSkeleton({super.key});

  @override
  Widget build(BuildContext context) {
    return Shimmer(
      baseColor: ColorPalette.shimmerBaseColor,
      highlightColor: ColorPalette.shimmerHighlightColor,
      child: const IgnorePointer(
          child: AmpelSystem(
              ampel: Ampel(
                  type: AmpelType.energy,
                  level: AmpelLevel.level1,
                  validFrom: ''))),
    );
  }
}
