import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:spend/core/resources/gaps.dart';
import 'package:spend/core/utils/navigation/navigation_utils.dart';
import 'package:spend/core/utils/translations.dart';
import 'package:spend/domain/services/login_service.dart';
import 'package:spend/presentation/components/auth/auth_footer.dart';
import 'package:spend/presentation/components/auth/control_elements/primary_button.dart';
import 'package:spend/presentation/components/auth/control_elements/primary_button_outlined.dart';

class LoginFooter extends StatelessWidget {
  const LoginFooter({super.key});

  @override
  Widget build(BuildContext context) {
    return AuthFooter(
      children: [
        ValueListenableBuilder(
          valueListenable: context.read<LoginService>().isLoading,
          builder: (context, loading, _) {
            return PrimaryButton(
              title: Translations.of(context)!.text('button.title.login'),
              callback: () async =>
                  await context.read<LoginService>().signInUser(context),
              isLoading: loading,
            );
          },
        ),
        Gaps.vSpacingS,
        PrimaryButtonOutlined(
          title: Translations.of(context)!.text('button.title.registration'),
          callback: () => Navigation.goToRegistration(),
        ),
      ],
    );
  }
}
