import 'dart:convert';

import 'package:flutter/material.dart';
import 'package:scan_and_go_flutter/app_config.dart';
import 'package:scan_and_go_flutter/models/order.dart';
import 'package:scan_and_go_flutter/models/shopping_cart.dart';
import 'package:scan_and_go_flutter/models/status_message.dart';
import 'package:scan_and_go_flutter/models/user.dart';
import 'package:scan_and_go_flutter/screens/payment_screen.dart';
import 'package:scan_and_go_flutter/services/auth_service.dart';
import 'package:scan_and_go_flutter/services/http_service.dart';
import 'package:scan_and_go_flutter/widgets/error_snackbar.dart';


class OrderScreen extends StatefulWidget {
  const OrderScreen({
    super.key,
    required this.shoppingCartDto,
    required this.storeId,
  });

  final ShoppingCartDto shoppingCartDto;
  final int storeId;

  @override
  State<OrderScreen> createState() => _OrderScreenState();
}

class _OrderScreenState extends State<OrderScreen> {
  late String? token;
  late UserDto? userDto;
  final _authService = AuthService();


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
    }
  }
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Order Summary'),
      ),
      body: Column(
        children: [
          Expanded(
            child: ListView.builder(
              itemCount: widget.shoppingCartDto.items.length,
              itemBuilder: (context, index) {
                final item = widget.shoppingCartDto.items[index];
                return ListTile(
                  title: Row(
                    mainAxisAlignment: MainAxisAlignment.spaceBetween,
                    children: [
                      Text(item.product.name, style: const TextStyle(color: Color(0xFF684FA1), fontWeight: FontWeight.bold),),
                      Text('x${widget.shoppingCartDto.items[index].quantity}'),
                    ],
                  ),
                  subtitle: Text('€${item.product.price.toStringAsFixed(2)}'),
                );
              },
            ),
          ),
          Padding(
            padding: const EdgeInsets.all(16.0),
            child: Row(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              children: [
                const Text('Total:', style: TextStyle(
                  fontSize: 20,
                  fontWeight: FontWeight.bold,
                ),),
                Text(
                  '€${_calculateTotal(widget.shoppingCartDto).toStringAsFixed(2)}',
                  style: const TextStyle(fontSize: 20, fontWeight: FontWeight.bold),
                ),
              ],
            ),
          ),
          ElevatedButton(
            onPressed: () => _showPaymentConfirmationDialog(context),
            child: const Text('Pay Now'),
          ),
          const SizedBox(height: 50,),
        ],
      ),
    );
  }

  double _calculateTotal(ShoppingCartDto shoppingCart) {
    double total = 0;
    for (var item in shoppingCart.items) {
      total += item.product.price * item.quantity;
    }
    return total;
  }

  Future<void> _pay(ShoppingCartDto shoppingCartDto) async{
    List<PayOrderItemDto> items = shoppingCartDto.items.map((item) =>
      PayOrderItemDto(
        productId: item.product.id,
        count: item.quantity,
      ),
    ).toList();
    PayOrderDto payOrderDto = PayOrderDto(
      userId: userDto!.id,
      storeId: widget.storeId,
      items: items,
    );

    final response = await HttpService().httpPost(AppConfig.orderPayOrderEndpoint, payOrderDto, token: token);
    if (response.statusCode == 200) {
      final responseBody = jsonDecode(response.body);
      final String paymentUrl = responseBody['url'];

      _navigateToPaymentScreen(paymentUrl);

    } else {
      StatusMessageDto statusMessageDto =
          StatusMessageDto.fromJson(jsonDecode(response.body));
      ErrorSnackbar.show(statusMessageDto.message);
    }
  }

  void _navigateToPaymentScreen(String initialUrl){
    Navigator.of(context).push(MaterialPageRoute(builder: (ctx) => PaymentScreen(initialUrl: initialUrl)));
  }

  Future<void> _showPaymentConfirmationDialog(BuildContext context) async {
    return showDialog<void>(
      context: context,
      builder: (BuildContext context) {
        return AlertDialog(
          title: const Text('Confirm Payment'),
          content: const SingleChildScrollView(
            child: ListBody(
              children: <Widget>[
                Text('Are you sure you want to proceed with the payment?'),
              ],
            ),
          ),
          actions: <Widget>[
            TextButton(
              onPressed: () {
                Navigator.of(context).pop();
                _pay(widget.shoppingCartDto);
              },
              child: const Text('Confirm'),
            ),
            TextButton(
              onPressed: () {
                Navigator.of(context).pop();
              },
              child: const Text('Cancel'),
            ),
          ],
        );
      },
    );
  }
}