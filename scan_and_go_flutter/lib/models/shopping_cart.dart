
import 'package:scan_and_go_flutter/models/product.dart';

class ShoppingCartDto{
  const ShoppingCartDto({
    required this.id,
    required this.items,
  });

  final int id;
  final List<ShoppingCartItem> items;

  factory ShoppingCartDto.fromJson(Map<String, dynamic> json) {
    var itemsList = json['Items'] as List;
    List<ShoppingCartItem> parsedItems = itemsList.map((itemJson) => ShoppingCartItem.fromJson(itemJson)).toList();

    return ShoppingCartDto(
      id: json['Id'],
      items: parsedItems,
    );
  }
}

class ShoppingCartItem{
  const ShoppingCartItem({
    required this.id,
    required this.product,
    required this.quantity,
  });

  final int id;
  final Product product;
  final int quantity;

  factory ShoppingCartItem.fromJson(Map<String, dynamic> json) {
    return ShoppingCartItem(
      id: json['Id'],
      product: Product.fromJson(json['Product']),
      quantity: json['Quantity'],
    );
  }
}

class AddToCartDto{
  const AddToCartDto({
    required this.buyerId,
    required this.barcode,
  });

  final int buyerId;
  final String barcode;

  Map<String, dynamic> toJson() {
    return {
      'BuyerId': buyerId,
      'Barcode': barcode,
    };
  }
}

class BuyerIdStoreId{
  const BuyerIdStoreId({
    required this.buyerId,
    required this.storeId,
  });

  final int buyerId;
  final int storeId;

  Map<String, dynamic> toJson() {
    return {
      'BuyerId': buyerId,
      'StoreId': storeId,
    };
  }
}

class ManageItemDto{
  const ManageItemDto({
    required this.buyerId,
    required this.productId,
  });

  final int buyerId;
  final int productId;

  Map<String, dynamic> toJson() {
    return {
      'BuyerId': buyerId,
      'ProductId': productId,
    };
  }
}