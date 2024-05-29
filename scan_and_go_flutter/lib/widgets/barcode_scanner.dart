import 'dart:convert';

import 'package:flutter/material.dart';
import 'package:flutter_barcode_scanner/flutter_barcode_scanner.dart';
import 'package:scan_and_go_flutter/app_config.dart';
import 'package:scan_and_go_flutter/models/product.dart';
import 'package:scan_and_go_flutter/models/shopping_cart.dart';
import 'package:scan_and_go_flutter/models/status_message.dart';
import 'package:scan_and_go_flutter/models/store.dart';
import 'package:scan_and_go_flutter/models/user.dart';
import 'package:scan_and_go_flutter/screens/shopping_cart_screen.dart';
import 'package:scan_and_go_flutter/services/auth_service.dart';
import 'package:scan_and_go_flutter/services/http_service.dart';
import 'package:scan_and_go_flutter/widgets/error_snackbar.dart';

class BarcodeScanner extends StatefulWidget {
  const BarcodeScanner({
    super.key, 
    required this.store,
  });

  final Store store;

  @override
  State<BarcodeScanner> createState() => _BarcodeScannerState();
}


class _BarcodeScannerState extends State<BarcodeScanner> {
  late String? token;
  late UserDto? userDto;
  final _authService = AuthService();
  Product? lastScannedProduct;

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

  Future<void> _scanBarcodeNormal() async {
    String barcodeScanRes =
        await FlutterBarcodeScanner.scanBarcode('#ff6666', 'Cancel', true, ScanMode.BARCODE);
    if (barcodeScanRes != "-1") {
      _addToCart(barcodeScanRes);
    }
  }

  Future<void> _addToCart(String barcodeScanRes) async {
    AddToCartDto addToCartDto = AddToCartDto(
      buyerId: userDto!.id,
      barcode: barcodeScanRes,
    );

    final response = await HttpService().httpPost(AppConfig.shoppingCartAddToCartEndpoint, addToCartDto, token: token);

    if (response.statusCode == 200) {
      setState(() {
        lastScannedProduct = Product.fromJson(jsonDecode(response.body));
      });
      ErrorSnackbar.show("Added to cart.");
    } else {
      StatusMessageDto statusMessageDto = StatusMessageDto.fromJson(jsonDecode(response.body));
      ErrorSnackbar.show(statusMessageDto.message);
    }
  }

  void _navigateToShoppingCartScreen() {
    Navigator.of(context).push(MaterialPageRoute(builder: (ctx) => ShoppingCartScreen(store: widget.store,)));
  }

@override
Widget build(BuildContext context) {
  return Scaffold(
    appBar: AppBar(
      title: Text(widget.store.name),
      bottom: PreferredSize(
        preferredSize: Size.zero,
        child: Container(
          alignment: Alignment.centerLeft,
          padding: const EdgeInsets.only(left: 75.0),
          child: Text(
            "Address: ${widget.store.address}",
          ),
        ),
      ),
    ),
    body: Center(
      child: Column(
        mainAxisAlignment: MainAxisAlignment.center,
        children: [
          const Text(
            "Press on camera icon below to scan barcode",
            style: TextStyle(fontSize: 16),
          ),
          IconButton(
            onPressed: () => _scanBarcodeNormal(),
            icon: const Icon(
              Icons.camera_alt_outlined,
              color: Color(0xFF684FA1),
              size: 150,
            ),
            tooltip: 'Scan a product',
          ),
          if (lastScannedProduct != null)
            Column(
              children: [
                const SizedBox(height: 20), 
                const Text(
                  "Last Scanned Product:",
                  style: TextStyle(fontSize: 20, fontWeight: FontWeight.bold, color: Colors.black),
                ),
                const SizedBox(height: 10), 
                Text(
                  "Name: ${lastScannedProduct!.name}",
                  style: const TextStyle(fontSize: 16, color: Colors.black),
                ),
                Text(
                  "Price: \$${lastScannedProduct!.price.toStringAsFixed(2)}",
                  style: const TextStyle(fontSize: 16, color: Colors.black),
                ),
                Text(
                  "Weight: ${lastScannedProduct!.weight} g", 
                  style: const TextStyle(fontSize: 16, color: Colors.black),
                ),
              ],
            ),
          if(lastScannedProduct == null)
            const SizedBox(height: 128), 
        ],
      ),
    ),
    floatingActionButton: FloatingActionButton(
      onPressed: () {
        _navigateToShoppingCartScreen();
      },
      child: const Icon(Icons.shopping_cart),
    ),
  );
}

}
