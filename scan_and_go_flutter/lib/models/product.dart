class Product{
  const Product({
    required this.id,
    required this.name,
    required this.price,
    required this.weight,
  });

  final int id;
  final String name;
  final double price;
  final double weight;
  
  Map<String, dynamic> toJson() {
    return {
      'Id': id,
      'Name': name,
      'Price': price,
      'Weight': weight,
    };
  }

  factory Product.fromJson(Map<String, dynamic> json) {
    return Product(
      id: json['Id'],
      name: json['Name'],
      price: json['Price'] is int ? (json['Price'] as int).toDouble() : json['Price'],
      weight: json['Weight'] is int ? (json['Weight'] as int).toDouble() : json['Weight'],
    );
  }
}