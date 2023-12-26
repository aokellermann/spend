import 'package:flutter/material.dart';
import 'package:spend/core/resources/color_palette.dart';
import 'package:spend/core/resources/paddings.dart';
import 'package:spend/core/utils/dismiss_keyboard.dart';
import 'package:spend/presentation/components/app_bar/header.dart';

/// This widget applies the auth scaffold design
///
/// It is used only for authentication screens
/// to keep the scaffolds consistent
class AuthScaffold extends StatelessWidget {
  final List<Widget> body;
  final Header? header;
  final Widget footer;

  const AuthScaffold(
      {super.key, required this.body, this.header, required this.footer});

  @override
  Widget build(BuildContext context) {
    return WillPopScope(
      onWillPop: () async => true,
      child: DismissKeyboard(
        child: Scaffold(
          backgroundColor: ColorPalette.white,
          resizeToAvoidBottomInset: true,
          appBar: header,
          body: SafeArea(
            child: SingleChildScrollView(
              child: Padding(
                padding:
                    const EdgeInsets.symmetric(horizontal: Paddings.paddingS),
                child: Column(
                  mainAxisAlignment: MainAxisAlignment.start,
                  crossAxisAlignment: CrossAxisAlignment.center,
                  children: body,
                ),
              ),
            ),
          ),
          bottomNavigationBar: footer,
        ),
      ),
    );
  }
}
