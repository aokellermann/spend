import 'package:flutter/material.dart';
import 'package:spend/core/resources/color_palette.dart';
import 'package:spend/core/resources/paddings.dart';
import 'package:spend/core/utils/translations.dart';
import 'package:spend/presentation/components/app_bar/header.dart';
import 'package:spend/presentation/information/privacy_policy/privacy_policy_text.dart';

class PrivacyPolicy extends StatelessWidget {
  const PrivacyPolicy({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: ColorPalette.white,
      appBar: Header(
        title: Translations.of(context)!.text('appbar.title.privacy_policy'),
        showBackButton: true,
      ),
      body: const SafeArea(
        child: SingleChildScrollView(
          child: Padding(
            padding: EdgeInsets.fromLTRB(Paddings.paddingS, Paddings.paddingM,
                Paddings.paddingS, Paddings.paddingS),
            child: PrivacyPolicyText(),
          ),
        ),
      ),
    );
  }
}
