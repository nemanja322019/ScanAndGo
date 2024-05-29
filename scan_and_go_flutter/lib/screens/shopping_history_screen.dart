import 'dart:convert'; 

import 'package:flutter/material.dart';
import 'package:scan_and_go_flutter/app_config.dart';
import 'package:scan_and_go_flutter/models/order.dart';
import 'package:scan_and_go_flutter/models/status_message.dart';
import 'package:scan_and_go_flutter/screens/order_history_screen.dart';
import 'package:scan_and_go_flutter/services/auth_service.dart';
import 'package:scan_and_go_flutter/services/http_service.dart';
import 'package:scan_and_go_flutter/widgets/error_snackbar.dart';

class ShoppingHistoryScreen extends StatefulWidget {
  const ShoppingHistoryScreen({
    super.key,
  });

  @override
  State<ShoppingHistoryScreen> createState() => _ShoppingHistoryScreenState();
}

class _ShoppingHistoryScreenState extends State<ShoppingHistoryScreen> {
  List<OrderDto> orders = [];
  late String? token;
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
    }
    await _getOrders();
  }

  Future<void> _getOrders() async {
    final response = await HttpService()
        .httpGet(AppConfig.orderGetAllBuyerOrdersEndpoint, token: token);
    if (response.statusCode == 200) {
      setState(() {
        final List<dynamic> jsonOrders = jsonDecode(response.body);
        orders = jsonOrders
            .map((jsonOrder) => OrderDto.fromJson(jsonOrder))
            .toList();
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

  String formatDateWithoutMilliseconds(DateTime dateTime) {
    String formattedDate =
        '${dateTime.year}-${_twoDigits(dateTime.month)}-${_twoDigits(dateTime.day)} ${_twoDigits(dateTime.hour)}:${_twoDigits(dateTime.minute)}';
    return formattedDate;
  }

  String _twoDigits(int n) {
    if (n >= 10) return "$n";
    return "0$n";
  }



  void _navigateToOrderHistoryScreen(OrderDto order){
    Navigator.of(context).push(MaterialPageRoute(builder: (ctx) => OrderHistoryScreen(orderId: order.id)));
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text("Shopping history"),
      ),
      body: orders.isEmpty
          ? const Center(
              child: Text("No orders found"),
            )
          : ListView.builder(
              itemCount: orders.length,
              itemBuilder: (context, index) {
                final order = orders[index];
                return ListTile(
                  title: Text(order.storeName, style: const TextStyle(color: Color(0xFF684FA1), fontWeight: FontWeight.bold,),),
                  trailing: Text(order.paymentDate != null
                      ? formatDateWithoutMilliseconds(order.paymentDate!)
                      : "Unpaid"),
                  onTap: () => _navigateToOrderHistoryScreen(orders[index]),
                );
              },
            ),
    );
  }
}
