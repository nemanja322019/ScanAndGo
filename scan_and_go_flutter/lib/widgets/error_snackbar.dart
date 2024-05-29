import 'package:flutter/material.dart';

class ErrorSnackbar {
  static final GlobalKey<ScaffoldMessengerState> scaffoldMessengerKey =
      GlobalKey<ScaffoldMessengerState>();

  static void show(String errorMessage) {
    scaffoldMessengerKey.currentState?.showSnackBar(
      SnackBar(
        content: Text(errorMessage, style: const TextStyle(fontSize: 18),),
        duration: const Duration(seconds: 3),
      ),
    );
  }
}