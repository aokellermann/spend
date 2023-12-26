// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'key_performance_index.dart';

// **************************************************************************
// TypeAdapterGenerator
// **************************************************************************

class KeyPerformanceIndexAdapter extends TypeAdapter<KeyPerformanceIndex> {
  @override
  final int typeId = 0;

  @override
  KeyPerformanceIndex read(BinaryReader reader) {
    final numOfFields = reader.readByte();
    final fields = <int, dynamic>{
      for (int i = 0; i < numOfFields; i++) reader.readByte(): reader.read(),
    };
    return KeyPerformanceIndex(
      name: fields[0] as String,
      value: fields[1] as String,
      trend: fields[2] as Trend?,
      trendRating: fields[3] as TrendRating?,
      date: fields[4] as String?,
      category: fields[5] as KPICategory,
      isExpanded: fields[6] as bool,
      route: fields[7] as NavigationRoute,
      isDisabled: fields[8] as bool,
      position: fields[9] as int,
    );
  }

  @override
  void write(BinaryWriter writer, KeyPerformanceIndex obj) {
    writer
      ..writeByte(10)
      ..writeByte(0)
      ..write(obj.name)
      ..writeByte(1)
      ..write(obj.value)
      ..writeByte(2)
      ..write(obj.trend)
      ..writeByte(3)
      ..write(obj.trendRating)
      ..writeByte(4)
      ..write(obj.date)
      ..writeByte(5)
      ..write(obj.category)
      ..writeByte(6)
      ..write(obj.isExpanded)
      ..writeByte(7)
      ..write(obj.route)
      ..writeByte(8)
      ..write(obj.isDisabled)
      ..writeByte(9)
      ..write(obj.position);
  }

  @override
  int get hashCode => typeId.hashCode;

  @override
  bool operator ==(Object other) =>
      identical(this, other) ||
      other is KeyPerformanceIndexAdapter &&
          runtimeType == other.runtimeType &&
          typeId == other.typeId;
}
