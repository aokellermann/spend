import 'package:get_it/get_it.dart';
import 'package:graphql_flutter/graphql_flutter.dart';

import '../../data/repositories/auth_repository.dart';

class GraphBaseHelper {
  late GraphQLClient _client;
  GraphQLClient get client => _client;

  GraphBaseHelper() {
    final HttpLink httpLink = HttpLink(
      'http://localhost:5000/graphql',
    );

    final AuthLink authLink = AuthLink(
        getToken: () async => GetIt.I.get<AuthRepository>().getAccessToken());

    final Link link = authLink.concat(httpLink);

    _client = GraphQLClient(
      link: link,
      // The default store is the InMemoryStore, which does NOT persist to disk
      cache: GraphQLCache(),
    );
  }
}
