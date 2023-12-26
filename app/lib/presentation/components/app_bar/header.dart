import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:get_it/get_it.dart';
import 'package:plaid_flutter/plaid_flutter.dart';
import 'package:provider/provider.dart';
import 'package:spend/core/mixins/header_mixin.dart';
import 'package:spend/core/resources/color_palette.dart';
import 'package:spend/core/resources/gaps.dart';
import 'package:spend/core/resources/paddings.dart';
import 'package:spend/core/types/navigation_route.dart';
import 'package:spend/core/utils/navigation/navigation_utils.dart';
import 'package:spend/core/utils/translations.dart';
import 'package:spend/data/repositories/graph_repository.dart';
import 'package:spend/domain/services/kpi_manager.dart';
import 'package:spend/presentation/components/app_bar/header_leading.dart';
import 'package:spend/presentation/components/app_bar/header_title.dart';
import 'package:spend/presentation/components/app_bar/star_button.dart';

class Header extends StatelessWidget
    with HeaderMixin
    implements PreferredSizeWidget {
  final String title;
  final bool showSwissLogo;
  final bool showInformation;
  final bool showBackButton;
  final bool showEditButton;
  final bool showStarButton;
  final bool showAddButton;

  const Header({
    super.key,
    required this.title,
    this.showSwissLogo = false,
    this.showInformation = false,
    this.showBackButton = false,
    this.showEditButton = false,
    this.showStarButton = false,
    this.showAddButton = false,
  });

  @override
  Widget build(BuildContext context) {
    return Selector<KPIManager, bool>(
        selector: (_, manager) => manager.editorMode,
        builder: (context, isEditing, _) {
          return AppBar(
            centerTitle: getCenterTitle(showSwissLogo, isEditing),
            backgroundColor: ColorPalette.white,
            elevation: 0,
            primary: true,
            titleSpacing: 0,
            systemOverlayStyle: const SystemUiOverlayStyle(
              statusBarColor: ColorPalette.white,
              statusBarIconBrightness: Brightness.dark,
              statusBarBrightness: Brightness.light,
            ),
            bottom: PreferredSize(
              preferredSize: const Size.fromHeight(1.0),
              child: Container(
                color: ColorPalette.dividerColor,
                height: 1.0,
              ),
            ),
            leading: HeaderLeading(
                showSwissLogo: showSwissLogo,
                showBackButton: showBackButton,
                isEditing: isEditing),
            leadingWidth: getLeadingWidth(
                showSwissLogo, showBackButton, isEditing, context),
            title: HeaderTitle(
                title: isEditing
                    ? Translations.of(context)!.text(
                        'appbar.title.editor-mode.${context.read<KPIManager>().currentRoute.routingTitle}')
                    : title,
                isEditing: isEditing),
            actions: [
              if (isEditing) ...[
                GestureDetector(
                  behavior: HitTestBehavior.translucent,
                  child: Padding(
                    padding: const EdgeInsets.only(right: Paddings.paddingS),
                    child: Row(
                      mainAxisSize: MainAxisSize.min,
                      mainAxisAlignment: MainAxisAlignment.end,
                      crossAxisAlignment: CrossAxisAlignment.center,
                      children: [
                        SvgPicture.asset('assets/icons/app_bar/check.svg',
                            width: 16,
                            colorFilter: const ColorFilter.mode(
                                ColorPalette.primaryColor, BlendMode.srcIn)),
                        Gaps.hSpacingXS,
                        Text(
                          Translations.of(context)!.text('appbar.save'),
                          style: const TextStyle(
                              fontSize: 16,
                              fontWeight: FontWeight.w500,
                              color: ColorPalette.primaryColor),
                        ),
                      ],
                    ),
                  ),
                  onTap: () => context.read<KPIManager>().saveChanges(context),
                )
              ] else ...[
                if (showAddButton)
                  IconButton(
                    onPressed: () async => PlaidLink.open(
                        configuration: LinkTokenConfiguration(
                            token: await GetIt.I.get<GraphRepository>().createLinkToken())),
                    padding: const EdgeInsets.only(right: Paddings.paddingXS),
                    icon: SvgPicture.asset('assets/icons/app_bar/plus.svg',
                        width: 21,
                        colorFilter: const ColorFilter.mode(
                            ColorPalette.primaryColor, BlendMode.srcIn)),
                  ),
                if (showEditButton)
                  IconButton(
                    onPressed: () =>
                        context.read<KPIManager>().toggleEditorMode(),
                    padding: const EdgeInsets.only(right: Paddings.paddingXS),
                    icon: SvgPicture.asset('assets/icons/app_bar/pen.svg',
                        width: 21,
                        colorFilter: const ColorFilter.mode(
                            ColorPalette.primaryColor, BlendMode.srcIn)),
                  ),
                if (showInformation)
                  IconButton(
                    onPressed: () => Navigation.goToInformation(),
                    padding: const EdgeInsets.only(right: Paddings.paddingXS),
                    icon: SvgPicture.asset(
                      'assets/icons/app_bar/circle-info.svg',
                      width: 22.5,
                      height: 22.5,
                      colorFilter: const ColorFilter.mode(
                          ColorPalette.primaryColor, BlendMode.srcIn),
                    ),
                  ),
                if (showStarButton)
                  const Padding(
                    padding: EdgeInsets.only(right: Paddings.paddingS),
                    child: StarButton(),
                  ),
              ]
            ],
          );
        });
  }

  @override
  Size get preferredSize => const Size.fromHeight(kToolbarHeight);
}
