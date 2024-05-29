import 'dart:convert';

import 'package:flutter/material.dart';
import 'package:scan_and_go_flutter/app_config.dart';
import 'package:scan_and_go_flutter/models/status_message.dart';
import 'package:scan_and_go_flutter/models/user.dart';
import 'package:scan_and_go_flutter/services/http_service.dart';
import 'package:scan_and_go_flutter/widgets/error_snackbar.dart';

class EnterEmail extends StatefulWidget{
  const EnterEmail({
    super.key,
  });

  @override
  State<EnterEmail> createState() => _EnterEmailState();
}

class _EnterEmailState extends State<EnterEmail> {
  final _formKey = GlobalKey<FormState>();
  final TextEditingController _emailController = TextEditingController();
  RegExp emailRegex = RegExp(r'^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$');

  Future<void> _getResetTokenButton() async {
    if (_formKey.currentState!.validate()) {
      EmailDto emailDto = EmailDto(email: _emailController.text);

      final response = await HttpService().httpPost("${AppConfig.userForgotPasswordEndpoint}/${emailDto.email}", emailDto.email);
      if (response.statusCode == 200) {
        ErrorSnackbar.show("Check your email.");
        _navigateToLogin();
      }
      else{
        StatusMessageDto statusMessageDto = StatusMessageDto.fromJson(jsonDecode(response.body));
        ErrorSnackbar.show(statusMessageDto.message);
      }
    }
  }

  void _navigateToLogin(){
    Navigator.of(context).pop();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text("Enter your email address"),
      ),
      body: Padding(
        padding: const EdgeInsets.all(16.0),
        child: Form(
          key: _formKey,
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.stretch,
            children: <Widget>[
                  TextFormField(
                controller: _emailController,
                decoration: const InputDecoration(labelText: 'Email'),
                keyboardType: TextInputType.emailAddress,
                validator: (value) {
                  if (value == null ||
                      value.isEmpty) {
                    return 'Please enter your email';
                  }
                  if(!emailRegex.hasMatch(value)){
                    return 'Incorrect email format';
                  }
                  return null;
                },
              ),
              const SizedBox(height: 20),
              ElevatedButton(
                onPressed: _getResetTokenButton,
                child: const Text('Get reset email'),
              ),
            ],
          ),
        ),
      ),
    );
  }

  @override
  void dispose() {
    _emailController.dispose();
    super.dispose();
  }
}