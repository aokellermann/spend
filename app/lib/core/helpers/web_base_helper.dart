import 'package:flutter/cupertino.dart';
import 'package:spend/core/resources/constants.dart';
import 'package:spend/core/utils/translations.dart';

class WebBaseHelper {
  String getWebUrl({required BuildContext context, required String path}) {
    return '${Constants.kWebBaseUrl}$path?viewtype=app&lng=${Translations.of(context)!.currentLanguage}';
  }
}
