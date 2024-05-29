import 'dart:convert';

import 'package:flutter/material.dart';
import 'package:scan_and_go_flutter/models/status_message.dart';
import 'package:scan_and_go_flutter/models/user.dart';
import 'package:scan_and_go_flutter/app_config.dart';
import 'package:scan_and_go_flutter/screens/login_screen.dart';
import 'package:scan_and_go_flutter/services/http_service.dart';
import 'package:scan_and_go_flutter/widgets/email_verification.dart';
import 'package:scan_and_go_flutter/widgets/error_snackbar.dart';

class Registration extends StatefulWidget{
  const Registration({
    super.key,
  });

  @override
  State<Registration> createState() => _RegistrationState();
}

class _RegistrationState extends State<Registration> {
  final _formKey = GlobalKey<FormState>();
  final TextEditingController _nameController = TextEditingController();
  final TextEditingController _emailController = TextEditingController();
  final TextEditingController _passwordController = TextEditingController();
  final TextEditingController _passwordConfirmController = TextEditingController();
  RegExp emailRegex = RegExp(r'^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$');

  Future<void> _registerButton() async {
    if (_formKey.currentState!.validate()) {
        RegisterUserDTO registerUserDTO = RegisterUserDTO(
        name: _nameController.text,
        email: _emailController.text,
        password: _passwordController.text,
      );

      final response = await HttpService().httpPost(AppConfig.userRegistrationEndpoint, registerUserDTO);

      if (response.statusCode == 200) {
        _navigateToEmailVerification();
      }
      else{
        StatusMessageDto statusMessageDto = StatusMessageDto.fromJson(jsonDecode(response.body));
        ErrorSnackbar.show(statusMessageDto.message);
      }

    }
  }

  void _navigateToEmailVerification(){
    Navigator.of(context).pushReplacement(MaterialPageRoute(builder: (ctx) => EmailVerification(email: _emailController.text,)));
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Registration Form'),
        actions: [
          ElevatedButton(
            onPressed: () {
              Navigator.of(context).pushReplacement(MaterialPageRoute(builder: (ctx) => const LoginScreen()));
            },
            child: const Text('Log In'),
          ),
        ],
      ),
      body: Padding(
        padding: const EdgeInsets.all(16.0),
        child: Form(
          key: _formKey,
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.stretch,
            children: <Widget>[
              TextFormField(
                controller: _nameController,
                decoration: const InputDecoration(labelText: 'Name'),
                validator: (value) {
                  if (value == null ||
                      value.isEmpty || 
                      value.trim().length <= 1 ||
                      value.trim().length > 50) {
                    return 'Please enter your name';
                  }
                  return null;
                },
              ),
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
              TextFormField(
                controller: _passwordController,
                decoration: const InputDecoration(labelText: 'Password'),
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
                controller: _passwordConfirmController,
                decoration: const InputDecoration(labelText: 'Confirm password'),
                obscureText: true,
                validator: (value) {
                  if (value != _passwordController.text) {
                    return 'Passwords do not match';
                  }
                  return null;
                },
              ),
              const SizedBox(height: 20),
              ElevatedButton(
                onPressed: _registerButton,
                child: const Text('Register'),
              ),
            ],
          ),
        ),
      ),
    );
  }

  @override
  void dispose() {
    _nameController.dispose();
    _emailController.dispose();
    _passwordController.dispose();
    _passwordConfirmController.dispose();
    super.dispose();
  }
}