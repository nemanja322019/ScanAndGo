
class Store{
  const Store({
    required this.id,
    required this.name,
    required this.address,
    this.distance,
  });

  final int id;
  final String name;
  final String address;
  final double? distance;

  factory Store.fromJson(Map<String, dynamic> json) {
    return Store(
      id: json['Id'],
      name: json['Name'],
      address: json['Address'],
      distance: json['Distance']
    );
  }
}