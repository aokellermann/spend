import 'package:flutter/material.dart';
import 'package:get_it/get_it.dart';
import 'package:graphql_flutter/graphql_flutter.dart';
import 'package:plaid_flutter/plaid_flutter.dart';
import 'package:spend/core/helpers/graph_base_helper.dart';

class GraphRepository {
  Future<void> initPlaid() async {
    PlaidLink.onSuccess.listen((event) async {
      await createItemLink(event.publicToken);
    });
  }

  Future<String> createLinkToken() async {
    var client = GetIt.I.get<GraphBaseHelper>().client;

    const String createLinkToken = r'''
mutation CreateLinkToken {
    action: createLinkToken {
        __typename
        createLinkTokenResponse {
            __typename
            linkToken
        }
    }
}
''';

    var res =
        await client.mutate(MutationOptions(document: gql(createLinkToken)));
    if (res.hasException) {
      debugPrint(res.exception.toString());
      return "";
    }

    debugPrint("created link token");

    return res.data?["action"]["createLinkTokenResponse"]["linkToken"] as String;
  }

  Future<void> createItemLink(String publicToken) async {
    var client = GetIt.I.get<GraphBaseHelper>().client;

    const String createItemLink = r'''
mutation CreateItemLink($publicToken: String!) {
    action: createItemLink(input: { request: { publicToken: $publicToken}}) {
        __typename
        item {
            __typename
            id
            itemId
            insertedAt
            updatedAt
        }
    }
}
''';

    var res = await client.mutate(MutationOptions(
        document: gql(createItemLink),
        variables: <String, dynamic>{'publicToken': publicToken}));
    if (res.hasException) {
      debugPrint(res.exception.toString());
      return;
    }

    var itemId = res.data?["action"]["item"]["itemId"];

    debugPrint("created item link $itemId");
  }
}
