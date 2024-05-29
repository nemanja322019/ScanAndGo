import 'package:flutter/material.dart';
import 'package:scan_and_go_flutter/models/store.dart';
import 'package:scan_and_go_flutter/widgets/shopping_cart.dart';

class ShoppingCartScreen extends StatefulWidget{
  const ShoppingCartScreen({
    super.key,
    required this.store,
  });

  final Store store;

  @override
  State<ShoppingCartScreen> createState() => _ShoppingCartState();
}

class _ShoppingCartState extends State<ShoppingCartScreen> {
  @override
  Widget build(BuildContext context) {
    return ShoppingCart(store: widget.store,);
  }
}