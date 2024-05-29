class OrderDto{
  const OrderDto({
    required this.id,
    required this.storeName,
    required this.items,
    required this.paymentDate,
    required this.qrCodeUrl,
  });

  final int id;
  final String storeName;
  final List<OrderItemDto> items;
  final DateTime? paymentDate;
  final String? qrCodeUrl;

  factory OrderDto.fromJson(Map<String, dynamic> json) {
    var itemsList = json['Items'] as List;
    List<OrderItemDto> parsedItems = itemsList.map((itemJson) => OrderItemDto.fromJson(itemJson)).toList();
    
    return OrderDto(
      id: json['Id'],
      storeName: json['StoreName'],
      items: parsedItems,
      paymentDate: json['PaymentDate'] != null ? DateTime.parse(json['PaymentDate']) : null,
      qrCodeUrl: json['QRCodeURL'],
    );
  }
}

class OrderItemDto{
  const OrderItemDto({
    required this.productId,
    required this.productName,
    required this.productPrice,
    required this.productWeight,
    required this.productCount,
  });

  final int productId;
  final String productName;
  final double productPrice;
  final double productWeight;
  final int productCount;

  factory OrderItemDto.fromJson(Map<String, dynamic> json) {
    return OrderItemDto(
      productId: json['Id'],
      productName: json['ProductName'],
      productPrice: json['ProductPrice'] is int ? (json['ProductPrice'] as int).toDouble() : json['ProductPrice'],
      productWeight: json['ProductWeight'] is int ? (json['ProductWeight'] as int).toDouble() : json['ProductWeight'],
      productCount: json['ProductCount'],
    );
  }
}

class PayOrderDto{
  const PayOrderDto({
    required this.userId,
    required this.storeId,
    required this.items,
  });

  final int userId;
  final int storeId;
  final List<PayOrderItemDto> items;

  Map<String, dynamic> toJson() {
    return {
      'UserId': userId,
      'StoreId': storeId,
      'Items': items.map((item) => item.toJson()).toList(),
    };
  }
}

class PayOrderItemDto{
  const PayOrderItemDto({
    required this.productId,
    required this.count,
  });

  final int productId;
  final int count;

  Map<String, dynamic> toJson() {
    return {
      'ProductId': productId,
      'Count': count,
    };
  }
}