import 'package:flutter/material.dart';
//import 'package:scan_and_go_flutter/services/shared_preferences_service.dart';
import 'package:scan_and_go_flutter/widgets/login.dart';

class LoginScreen extends StatefulWidget{
  const LoginScreen({
    super.key,
  });

  @override
  State<LoginScreen> createState() => _LoginScreenState();
}

class _LoginScreenState extends State<LoginScreen> {

  @override
  Widget build(BuildContext context) {
    return const Scaffold(
      body: Login(),
    );
  }  
}