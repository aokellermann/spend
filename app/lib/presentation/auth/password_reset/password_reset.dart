import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:spend/core/resources/color_palette.dart';
import 'package:spend/core/resources/gaps.dart';
import 'package:spend/core/resources/paddings.dart';
import 'package:spend/core/utils/auth/auth_constants.dart';
import 'package:spend/core/utils/translations.dart';
import 'package:spend/domain/services/password_service.dart';
import 'package:spend/presentation/auth/password_reset/footers/password_reset_footer.dart';
import 'package:spend/presentation/components/app_bar/header.dart';
import 'package:spend/presentation/components/auth/auth_scaffold.dart';
import 'package:spend/presentation/components/auth/control_elements/email_text_field.dart';

class PasswordReset extends StatelessWidget {
  const PasswordReset({super.key});

  @override
  Widget build(BuildContext context) {
    return AuthScaffold(
      header: Header(
          title: Translations.of(context)!.text('password-reset.appbar.title'),
          showSwissLogo: true),
      footer: const PasswordResetFooter(),
      body: [
        Padding(
          padding: const EdgeInsets.only(top: Paddings.paddingS),
          child: RichText(
            text: TextSpan(
                style: const TextStyle(
                  fontSize: AuthConst.kAuthInstructionsFontSize,
                  fontWeight: FontWeight.normal,
                  color: ColorPalette.textColor,
                ),
                children: [
                  TextSpan(
                    text: Translations.of(context)!
                        .text('password-reset.email.instructions.email-input'),
                  ),
                  TextSpan(
                    text: Translations.of(context)!
                        .text('button.title.request-code'),
                    style: const TextStyle(fontWeight: FontWeight.w600),
                  ),
                  TextSpan(
                      text: Translations.of(context)!.text(
                          'password-reset.email.instructions.code-input')),
                ]),
          ),
        ),
        Gaps.vSpacingM,
        Selector<PasswordService, String?>(
            selector: (_, service) => service.emailErrorText,
            shouldRebuild: (prev, next) => true,
            builder: (_, error, __) {
              return EmailTextField(
                label: Translations.of(context)!.text('textfield.label.email'),
                controller: context.read<PasswordService>().emailController,
                errorText: error,
                focusNode: context.read<PasswordService>().emailFocusNode,
                inputAction: TextInputAction.done,
                hintText:
                    Translations.of(context)!.text('textfield.hint.email'),
              );
            }),
        Gaps.vSpacingS,
      ],
    );
  }
}
