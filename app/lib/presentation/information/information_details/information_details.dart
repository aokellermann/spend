import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:spend/core/resources/color_palette.dart';
import 'package:spend/core/utils/information_details_parameter.dart';
import 'package:spend/domain/services/webview_service.dart';
import 'package:spend/presentation/components/app_bar/header.dart';
import 'package:spend/presentation/components/web_view/bfe_webview.dart';

/// This Widget is used for all information subpages (like Barrierefreiheit, ...)
class InformationDetails extends StatelessWidget {
  final InformationDetailsParameter detailsParameter;

  const InformationDetails({super.key, required this.detailsParameter});

  @override
  Widget build(BuildContext context) {
    return ChangeNotifierProvider(
      create: (_) => WebviewService(context: context),
      child: Scaffold(
        backgroundColor: ColorPalette.websiteBgColor,
        appBar: Header(title: detailsParameter.title, showBackButton: true),
        body: SafeArea(
          child: BfeWebview(url: detailsParameter.url),
        ),
      ),
    );
  }
}
