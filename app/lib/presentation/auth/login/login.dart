import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:provider/provider.dart';
import 'package:spend/core/resources/color_palette.dart';
import 'package:spend/core/resources/gaps.dart';
import 'package:spend/core/resources/paddings.dart';
import 'package:spend/core/utils/auth/auth_constants.dart';
import 'package:spend/core/utils/navigation/navigation_utils.dart';
import 'package:spend/core/utils/translations.dart';
import 'package:spend/domain/services/login_service.dart';
import 'package:spend/presentation/auth/login/login_footer.dart';
import 'package:spend/presentation/components/auth/auth_scaffold.dart';
import 'package:spend/presentation/components/auth/control_elements/email_text_field.dart';
import 'package:spend/presentation/components/auth/control_elements/password_text_field.dart';
import 'package:spend/presentation/components/auth/control_elements/secondary_button.dart';

class Login extends StatelessWidget {
  const Login({super.key});

  @override
  Widget build(BuildContext context) {
    return AuthScaffold(
      header: null,
      footer: const LoginFooter(),
      body: [
        Gaps.vSpacingL,
        SvgPicture.asset(
          'assets/icons/app_bar/dollar.svg',
          alignment: Alignment.center,
          width: AuthConst.kLoginSwissFlagSize,
        ),
        Gaps.vSpacingXS,
        const Text(
          'Spend',
          maxLines: 2,
          style: TextStyle(
            fontSize: AuthConst.kLoginAppTitleFontSize,
            fontWeight: FontWeight.bold,
            color: ColorPalette.textColor,
          ),
        ),
        Padding(
          padding: const EdgeInsets.fromLTRB(
              0, Paddings.paddingM, 0, Paddings.paddingL),
          child: Text(
            Translations.of(context)!.text('login.instructions'),
            style: const TextStyle(
              fontSize: AuthConst.kAuthInstructionsFontSize,
              fontWeight: FontWeight.normal,
              color: ColorPalette.textColor,
            ),
            textAlign: TextAlign.center,
          ),
        ),
        Selector<LoginService, String?>(
          selector: (_, service) => service.emailErrorText,
          shouldRebuild: (prev, current) => true,
          builder: (_, error, __) {
            return EmailTextField(
              label: Translations.of(context)!.text('textfield.label.email'),
              controller: context.read<LoginService>().emailController,
              errorText: error,
              focusNode: context.read<LoginService>().emailFocusNode,
            );
          },
        ),
        Selector<LoginService, String?>(
          selector: (_, service) => service.passwordErrorText,
          shouldRebuild: (prev, current) => true,
          builder: (_, error, __) {
            return PasswordTextField(
              label: Translations.of(context)!.text('textfield.label.password'),
              controller: context.read<LoginService>().passwordController,
              errorText: error,
              inputAction: TextInputAction.done,
              focusNode: context.read<LoginService>().passwordFocusNode,
            );
          },
        ),
        Padding(
          padding: const EdgeInsets.symmetric(horizontal: Paddings.paddingXL),
          child: SecondaryButton(
            title:
                Translations.of(context)!.text('button.title.forgot-password'),
            callback: () => Navigation.goToPwReset(),
            isSmall: true,
          ),
        ),
        Gaps.vSpacingS,
      ],
    );
  }
}
