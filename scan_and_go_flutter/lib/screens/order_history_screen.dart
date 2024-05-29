import 'dart:convert';

import 'package:flutter/material.dart';
import 'package:scan_and_go_flutter/app_config.dart';
import 'package:scan_and_go_flutter/models/order.dart';
import 'package:scan_and_go_flutter/models/status_message.dart';
import 'package:scan_and_go_flutter/services/auth_service.dart';
import 'package:scan_and_go_flutter/services/http_service.dart';
import 'package:scan_and_go_flutter/widgets/error_snackbar.dart';

class OrderHistoryScreen extends StatefulWidget {
  const OrderHistoryScreen({
    super.key,
    required this.orderId,
  });

  final int orderId;

  @override
  State<OrderHistoryScreen> createState() => _OrderHistoryScreenState();
}

class _OrderHistoryScreenState extends State<OrderHistoryScreen> {
  var order = const OrderDto(id: -1, storeName: '', items: [], paymentDate: null, qrCodeUrl: null);
  late OrderDto order1;
  late String? token;
  final _authService = AuthService();
  double totalSum = 0;
  
  @override
  void initState() {
    super.initState();
    _getData();
  }

  Future<void> _getData() async {
    token = await _authService.getToken();
    if (token == null) {
      await _authService.logout();
    }
    await _getOrder();
  }

  Future<void> _getOrder() async {
    final response = await HttpService()
        .httpGet('${AppConfig.orderGetOrderByIdEndpoint}${widget.orderId}', token: token);
    if (response.statusCode == 200) {
      setState(() {
        order = OrderDto.fromJson(jsonDecode(response.body));
        totalSum = order.items.fold<double>(
        0, (previousValue, item) => previousValue + item.productPrice * item.productCount);
      });
    } else {
      if (response.body.isEmpty) {
        ErrorSnackbar.show("Your shopping history is empty!");
      } else {
        StatusMessageDto statusMessageDto =
            StatusMessageDto.fromJson(jsonDecode(response.body));
        ErrorSnackbar.show(statusMessageDto.message);
      }
    }
  }

 @override
Widget build(BuildContext context) {
  return Scaffold(
    appBar: AppBar(
      title: order.id == -1 ? const Text('Loading...')
      : Text(order.storeName),
    ),
    body: order.id == -1
        ? const Center(child: CircularProgressIndicator())
        : Column(
          children: [
            Expanded(
              child: ListView.builder(
                itemCount: order.items.length,
                itemBuilder: (context, index) {
                  final item = order.items[index];
                  return ListTile(
                    title: Text(item.productName),
                    subtitle: Text('Price: \$${item.productPrice.toStringAsFixed(2)}, Count: ${item.productCount}'),
                  );
                },
              ),
            ),
              order.qrCodeUrl != null ? Image.network(
              order.qrCodeUrl!,
              width: 200,
              height: 200,
            ) : Container(),
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
                  '€${totalSum.toStringAsFixed(2)}',
                  style: const TextStyle(fontSize: 20, fontWeight: FontWeight.bold),
                ),
              ],
            ),
          ),
          ],
        ),
  );
}
}