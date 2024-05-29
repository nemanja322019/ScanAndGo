import 'package:flutter/material.dart';
import 'package:scan_and_go_flutter/widgets/registration.dart';

class RegisterScreen extends StatefulWidget {
  const RegisterScreen({
    super.key,
  });

  @override
  State<RegisterScreen> createState() => _RegisterScreenState();
}

class _RegisterScreenState extends State<RegisterScreen> {

  @override
  Widget build(BuildContext context) {
    Widget content = const Registration();

    return Scaffold(
      body: content,
    );
  }
}
