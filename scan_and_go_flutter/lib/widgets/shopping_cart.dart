import 'dart:convert';

import 'package:flutter/material.dart';
import 'package:scan_and_go_flutter/app_config.dart';
import 'package:scan_and_go_flutter/models/shopping_cart.dart';
import 'package:scan_and_go_flutter/models/status_message.dart';
import 'package:scan_and_go_flutter/models/store.dart';
import 'package:scan_and_go_flutter/models/user.dart';
import 'package:scan_and_go_flutter/screens/order_screen.dart';
import 'package:scan_and_go_flutter/services/auth_service.dart';
import 'package:scan_and_go_flutter/services/http_service.dart';
import 'package:scan_and_go_flutter/widgets/error_snackbar.dart';

class ShoppingCart extends StatefulWidget {
  const ShoppingCart({
    super.key,
    required this.store,
  });

  final Store store;

  @override
  State<ShoppingCart> createState() => _ShoppingCartState();
}

class _ShoppingCartState extends State<ShoppingCart> {
  late String? token;
  late UserDto? userDto;
  final _authService = AuthService();
  var shoppingCartDto = const ShoppingCartDto(id: 0, items: []);

  @override
  void initState() {
    super.initState();
    _getData();
  }

  Future<void> _getData() async {
    token = await _authService.getToken();
    if (token == null) {
      await _authService.logout();
    } else {
      userDto = await _authService.getUser();
      if (userDto == null) {
        await _authService.logout();
      }
      await _getShoppingCart();
    }
  }

  Future<void> _getShoppingCart() async {
    BuyerIdStoreId buyerIdStoreId = BuyerIdStoreId(
      buyerId: userDto!.id,
      storeId: widget.store.id,
    );

    final response = await HttpService().httpPost(
        AppConfig.shoppingCartGetShoppingCartEndpoint, buyerIdStoreId,
        token: token);
    if (response.statusCode == 200) {
      if (!mounted) {return;}
      setState(() {
        shoppingCartDto = ShoppingCartDto.fromJson(jsonDecode(response.body));
      });
    } else {
      if(response.body.isEmpty){
        ErrorSnackbar.show("Your shopping cart is empty!");
        _navigateBack();
      }
      else{
        StatusMessageDto statusMessageDto =
          StatusMessageDto.fromJson(jsonDecode(response.body));
        ErrorSnackbar.show(statusMessageDto.message);
      }
    }
  }

  void _navigateBack(){
    Navigator.of(context).pop();
  }

  Future<void> _increaseQuantity(productId) async{
    ManageItemDto manageItemDto = ManageItemDto(buyerId: userDto!.id, productId: productId);

    final response = await HttpService().httpPut(AppConfig.shoppingCartIncreaseQuantityEndpoint, manageItemDto, token: token);
    if (response.statusCode == 200) {
      _getShoppingCart();
    } else {
      StatusMessageDto statusMessageDto =
          StatusMessageDto.fromJson(jsonDecode(response.body));
      ErrorSnackbar.show(statusMessageDto.message);
    }
  }

  Future<void> _decreaseQuantity(productId) async{
    ManageItemDto manageItemDto = ManageItemDto(buyerId: userDto!.id, productId: productId);

    final response = await HttpService().httpPut(AppConfig.shoppingCartDecreaseQuantityEndpoint, manageItemDto, token: token);
    if (response.statusCode == 200) {
      _getShoppingCart();
    } else {
      StatusMessageDto statusMessageDto =
          StatusMessageDto.fromJson(jsonDecode(response.body));
      ErrorSnackbar.show(statusMessageDto.message);
    }
  }

  Future<void> _removeFromCart(productId) async{
    ManageItemDto manageItemDto = ManageItemDto(buyerId: userDto!.id, productId: productId);

    final response = await HttpService().httpDelete(AppConfig.shoppingCartRemoveFromCartEndpoint, params: manageItemDto.toJson(), token: token);
    if (response.statusCode == 200) {
      _getShoppingCart();
    } else {
      StatusMessageDto statusMessageDto =
          StatusMessageDto.fromJson(jsonDecode(response.body));
      ErrorSnackbar.show(statusMessageDto.message);
    }
  }

  void _navigateToOrder() {
    if(shoppingCartDto.items.isEmpty){
      ErrorSnackbar.show("Your shopping cart is empty!");
    } else {
      Navigator.of(context).push(MaterialPageRoute(builder: (ctx) => OrderScreen(shoppingCartDto: shoppingCartDto, storeId: widget.store.id)));
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text("Shopping Cart"),
      ),
      body: ListView.builder(
        itemCount: shoppingCartDto.items.length,
        itemBuilder: (BuildContext context, int index) {
          return ListTile(
            title: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                Row(
                  mainAxisAlignment: MainAxisAlignment.spaceBetween,
                  children: [
                    Text(
                      shoppingCartDto.items[index].product.name,
                      style: const TextStyle(
                        fontSize: 22,
                        fontWeight: FontWeight.bold,
                        color: Color(0xFF684FA1),
                      ),
                    ),
                    Row(
                      children: [
                        IconButton(
                          onPressed: () {
                            _increaseQuantity(shoppingCartDto.items[index].product.id);
                          },
                          icon: const Icon(Icons.add),
                        ),
                        Text(shoppingCartDto.items[index].quantity.toString()),
                        IconButton(
                          onPressed: () {
                            if (shoppingCartDto.items[index].quantity > 0) {
                              _decreaseQuantity(shoppingCartDto.items[index].product.id);
                            }
                            else{
                              _removeFromCart(shoppingCartDto.items[index].product.id);
                            }
                          },
                          icon: const Icon(Icons.remove),
                        ),
                        IconButton(
                          onPressed: () {
                            _removeFromCart(shoppingCartDto.items[index].product.id);
                          },
                          icon: const Icon(Icons.delete, color: Colors.red,),
                        ),
                      ],
                    ),
                  ],
                ),
              ],
            ),
          );
        },
      ),
      floatingActionButton: FloatingActionButton(
        onPressed: () {
          _navigateToOrder();
        },
        child: const Icon(Icons.payment),
      ),
    );
  }
}
