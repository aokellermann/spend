import 'package:spend/core/utils/empty_objects.dart';
import 'package:spend/domain/entities/key_performance_index.dart';

mixin SkeletonMixin {
  List<KeyPerformanceIndex> emptyKPIs(int number) =>
      List.generate(number, (index) => EmptyObjects.kpi);
}
