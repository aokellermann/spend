import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:spend/core/mixins/header_mixin.dart';
import 'package:spend/core/resources/paddings.dart';

class SpendLogo extends StatelessWidget {
  const SpendLogo({super.key});

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.only(left: Paddings.paddingS),
      child: SvgPicture.asset(
        'assets/icons/app_bar/dollar.svg',
        alignment: Alignment.center,
        width: HeaderMixin.kHeaderLogoWidth,
        height: HeaderMixin.kHeaderLogoHeight,
      ),
    );
  }
}
