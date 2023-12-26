import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:spend/core/resources/gaps.dart';
import 'package:spend/core/utils/translations.dart';
import 'package:spend/domain/services/password_service.dart';
import 'package:spend/presentation/components/auth/auth_footer.dart';
import 'package:spend/presentation/components/auth/control_elements/primary_button.dart';
import 'package:spend/presentation/components/auth/control_elements/secondary_button.dart';

class PasswordResetFooter extends StatelessWidget {
  const PasswordResetFooter({super.key});

  @override
  Widget build(BuildContext context) {
    return AuthFooter(
      children: [
        ValueListenableBuilder(
            valueListenable: context.read<PasswordService>().isLoading,
            builder: (context, loading, _) {
              return PrimaryButton(
                title:
                    Translations.of(context)!.text('button.title.request-code'),
                callback: () async => await context
                    .read<PasswordService>()
                    .resetPassword(context),
                isLoading: loading,
              );
            }),
        Gaps.vSpacingXS,
        SecondaryButton(
            title: Translations.of(context)!.text('button.title.cancel'),
            callback: () =>
                context.read<PasswordService>().cancelPasswordReset()),
      ],
    );
  }
}
