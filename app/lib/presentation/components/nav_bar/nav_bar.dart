import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';
import 'package:provider/provider.dart';
import 'package:spend/core/mixins/nav_bar_mixin.dart';
import 'package:spend/core/resources/color_palette.dart';
import 'package:spend/core/resources/constants.dart';
import 'package:spend/domain/services/kpi_manager.dart';
import 'package:spend/presentation/components/nav_bar/nav_bar_items.dart';
import 'package:spend/presentation/components/nav_bar/nav_bar_slider.dart';

class NavBar extends StatelessWidget {
  final StatefulNavigationShell navigationShell;

  const NavBar({Key? key, required this.navigationShell}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Selector<KPIManager, bool>(
        selector: (_, manager) => manager.editorMode,
        builder: (context, isEditing, _) {
          return AnimatedContainer(
            duration: const Duration(
                milliseconds: NavBarMixin.heightAnimationDuration),
            curve: Curves.easeIn,
            height: isEditing
                ? 0
                : Constants.kNavBarHeight +
                    MediaQuery.of(context).viewPadding.bottom,
            color: ColorPalette.white,
            child: Stack(
              children: [
                NavBarItems(index: navigationShell.currentIndex),
                const Divider(
                    thickness: 1, color: ColorPalette.dividerColor, height: 1),
                NavBarSlider(index: navigationShell.currentIndex),
              ],
            ),
          );
        });
  }
}
