import 'dart:convert';

import 'package:flutter/material.dart';
import 'package:scan_and_go_flutter/app_config.dart';
import 'package:scan_and_go_flutter/models/status_message.dart';
import 'package:scan_and_go_flutter/models/user.dart';
import 'package:scan_and_go_flutter/services/auth_service.dart';
import 'package:scan_and_go_flutter/services/http_service.dart';
import 'package:scan_and_go_flutter/widgets/error_snackbar.dart';

class PasswordReset extends StatefulWidget{
  const PasswordReset({
    super.key,
    required this.email,
    required this.emailToken,
  });

  final String email;
  final String emailToken;

  @override
  State<PasswordReset> createState() => _PasswordResetState();
}

class _PasswordResetState extends State<PasswordReset> {
  final _formKey = GlobalKey<FormState>();
  final TextEditingController _oldPasswordController = TextEditingController();
  final TextEditingController _newPasswordController = TextEditingController();
  late String? token;
  final _authService = AuthService();

  @override
  void initState() {
    super.initState();
    _getData();
  }

  Future<void> _getData() async {
    token = await _authService.getToken();
    if(token == null){
      await _authService.logout();
    }
  }

  Future<void> _resetPasswordButton() async {
    if (_formKey.currentState!.validate()) {
      ResetPasswordDto resetPasswordDto = ResetPasswordDto(
        email: widget.email,
        emailToken: widget.emailToken,
        oldPassword: _oldPasswordController.text,
        newPassword: _newPasswordController.text,
      );

      final response = await HttpService().httpPost(AppConfig.userResetPasswordEndpoint, resetPasswordDto, token: token);

      if (response.statusCode == 200) {
       _handleSuccess();
      } else {
       _handleError(response);
      }
    }
  }

  void _handleSuccess() {
    Navigator.of(context).pop();
    ErrorSnackbar.show('Password changed!');
  }

  void _handleError(response) {
    StatusMessageDto statusMessageDto = StatusMessageDto.fromJson(jsonDecode(response.body));
    ErrorSnackbar.show(statusMessageDto.message);
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text("Reset your password"),
      ),
      body: Padding(
        padding: const EdgeInsets.all(16.0),
        child: Form(
          key: _formKey,
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.stretch,
            children: <Widget>[
              TextFormField(
                controller: _oldPasswordController,
                decoration: const InputDecoration(labelText: 'Your old password'),
                obscureText: true,
                validator: (value) {
                  if (value == null ||
                      value.isEmpty ||
                      value.trim().length < 8 ||
                      value.trim().length > 50) {
                    return 'Password must be 8 characters or longer';
                  }
                  return null;
                },
              ),
              TextFormField(
                controller: _newPasswordController,
                decoration: const InputDecoration(labelText: 'Your new password'),
                obscureText: true,
                validator: (value) {
                  if (value == null ||
                      value.isEmpty ||
                      value.trim().length < 8 ||
                      value.trim().length > 50) {
                    return 'Password must be 8 characters or longer';
                  }
                  return null;
                },
              ),
              const SizedBox(height: 20),
              ElevatedButton(
                onPressed: _resetPasswordButton,
                child: const Text('Set new password'),
              ),
            ],
          ),
        ),
      ),
    );
  }

  @override
  void dispose() {
    _newPasswordController.dispose();
    _oldPasswordController.dispose();
    super.dispose();
  }
}