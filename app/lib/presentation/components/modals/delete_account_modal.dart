import 'package:flutter/material.dart';
import 'package:get_it/get_it.dart';
import 'package:spend/core/resources/paddings.dart';
import 'package:spend/core/utils/navigation/navigation_utils.dart';
import 'package:spend/data/repositories/auth_repository.dart';

///
/// UNUSED
///
class DeleteAccountModal extends StatelessWidget {
  const DeleteAccountModal({super.key});

  @override
  Widget build(BuildContext context) {
    return Container(
      padding: const EdgeInsets.fromLTRB(Paddings.paddingS, Paddings.paddingS,
          Paddings.paddingS, Paddings.paddingXS),
      decoration: const BoxDecoration(
        color: Colors.transparent,
        borderRadius: BorderRadius.only(
            topLeft: Radius.circular(12), topRight: Radius.circular(12)),
      ),
      child: Column(
        children: [
          const Text('Delete account'),
          const Text('Are you sure?'),
          TextButton(
            child: const Text('Delete account'),
            onPressed: () async {
              await GetIt.I.get<AuthRepository>().deleteUser();
              //Navigator.pop(context);
              Navigation.goToOverview();
            },
          ),
          TextButton(
            child: const Text('Cancel'),
            onPressed: () => Navigator.pop(context),
          ),
        ],
      ),
    );
  }
}
