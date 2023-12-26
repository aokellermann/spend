import 'package:flutter/material.dart';
import 'package:flutter_spinkit/flutter_spinkit.dart';
import 'package:spend/core/resources/color_palette.dart';
import 'package:spend/core/resources/gaps.dart';

class WebViewLoading extends StatelessWidget {
  final double loading;

  const WebViewLoading({super.key, required this.loading});

  @override
  Widget build(BuildContext context) {
    return Center(
        child: Column(
      mainAxisSize: MainAxisSize.min,
      mainAxisAlignment: MainAxisAlignment.center,
      children: [
        const SpinKitDoubleBounce(
          color: ColorPalette.primaryColor,
          size: 45,
        ),
        Gaps.vSpacingXS,
        Text(
          '${(loading * 10).toInt()}%',
          style: const TextStyle(
              fontSize: 14.3, color: ColorPalette.textColorLight),
        )
      ],
    ));
  }
}
