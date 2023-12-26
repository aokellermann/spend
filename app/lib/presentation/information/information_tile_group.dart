import 'package:flutter/material.dart';
import 'package:spend/core/resources/color_palette.dart';
import 'package:spend/core/resources/paddings.dart';

/// This class groups information tiles
///
/// It adds spacing and borders to the tile groups
class InformationTileGroup extends StatelessWidget {
  final List<Widget> children;

  const InformationTileGroup({super.key, required this.children});

  @override
  Widget build(BuildContext context) {
    return Container(
      margin: const EdgeInsets.only(top: Paddings.paddingM),
      decoration: const BoxDecoration(
          border: Border.symmetric(
              horizontal:
                  BorderSide(width: 1, color: ColorPalette.dividerColor)),
          color: Colors.transparent),
      child: Column(
        mainAxisSize: MainAxisSize.min,
        mainAxisAlignment: MainAxisAlignment.start,
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [...children],
      ),
    );
  }
}
