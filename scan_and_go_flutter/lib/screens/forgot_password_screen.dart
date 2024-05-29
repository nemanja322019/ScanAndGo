import 'package:flutter/material.dart';
import 'package:scan_and_go_flutter/widgets/enter_email.dart';

class ForgotPasswordScreen extends StatefulWidget{
  const ForgotPasswordScreen({
    super.key
  });

  @override
  State<ForgotPasswordScreen> createState() => _ForgotPasswordScreenState();
}

class _ForgotPasswordScreenState extends State<ForgotPasswordScreen> {
  @override
  Widget build(BuildContext context) {
    return const EnterEmail();
  }
}