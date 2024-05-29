import 'package:flutter/material.dart';
import 'package:scan_and_go_flutter/models/store.dart';
import 'package:scan_and_go_flutter/widgets/barcode_scanner.dart';


class StoreScreen extends StatelessWidget {
  const StoreScreen({
    super.key,
    required this.store,
  });

  final Store store;

  @override
  Widget build(BuildContext context) {
    return BarcodeScanner(store: store);
  }
}
