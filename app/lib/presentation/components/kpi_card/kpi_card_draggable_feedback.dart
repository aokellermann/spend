import 'package:flutter/material.dart';
import 'package:spend/core/mixins/kpi_card_mixin.dart';

class KpiCardDraggableFeedback extends StatelessWidget {
  final Widget child;

  const KpiCardDraggableFeedback({super.key, required this.child});

  @override
  Widget build(BuildContext context) {
    return Container(
      decoration: BoxDecoration(
          borderRadius:
              BorderRadius.circular(KPICardMixin.kKpiCardBorderRadius),
          boxShadow: [
            BoxShadow(
                color: Colors.grey.withOpacity(.4),
                blurRadius: 10,
                spreadRadius: 3),
          ]),
      child: child,
    );
  }
}
