import 'package:flutter/material.dart';
import 'package:spend/core/mixins/ampel_system_mixin.dart';
import 'package:spend/core/resources/paddings.dart';

class AmpelSystemPlaceholder extends StatelessWidget {
  const AmpelSystemPlaceholder({super.key});

  @override
  Widget build(BuildContext context) {
    return const SizedBox(
      height: AmpelSystemMixin.kAmpelSystemHeight +
          AmpelSystemMixin.kAmpelSystemTabsHeight +
          Paddings.paddingXS +
          Paddings.paddingM,
      width: double.infinity,
    );
  }
}
