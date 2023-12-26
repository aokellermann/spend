import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:provider/provider.dart';
import 'package:spend/core/mixins/header_mixin.dart';
import 'package:spend/core/resources/color_palette.dart';
import 'package:spend/core/resources/paddings.dart';
import 'package:spend/domain/services/kpi_manager.dart';
import 'package:spend/presentation/components/app_bar/back_button.dart';
import 'package:spend/presentation/components/app_bar/swiss_logo.dart';

class HeaderLeading extends StatelessWidget with HeaderMixin {
  final bool showSwissLogo;
  final bool showBackButton;
  final bool isEditing;

  const HeaderLeading(
      {super.key,
      required this.showSwissLogo,
      required this.showBackButton,
      required this.isEditing});

  @override
  Widget build(BuildContext context) {
    if (isEditing) {
      return Padding(
        padding: const EdgeInsets.only(left: Paddings.paddingS),
        child: IconButton(
          padding: const EdgeInsets.all(0),
          onPressed: () => context.read<KPIManager>().discardChanges(context),
          icon: SvgPicture.asset('assets/icons/app_bar/cancel.svg',
              width: HeaderMixin.kHeaderCancelButtonWidth,
              colorFilter: const ColorFilter.mode(
                  ColorPalette.primaryColor, BlendMode.srcIn)),
        ),
      );
    } else {
      if (showSwissLogo) {
        return const SpendLogo();
      } else if (showBackButton) {
        return const BackButtonBfe();
      } else {
        return const SizedBox();
      }
    }
  }
}
