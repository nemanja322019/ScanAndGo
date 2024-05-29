import 'dart:convert';

import 'package:flutter/material.dart';
import 'package:scan_and_go_flutter/app_config.dart';
import 'package:scan_and_go_flutter/models/status_message.dart';
import 'package:scan_and_go_flutter/screens/forgot_password_screen.dart';
import 'package:scan_and_go_flutter/screens/home_screen.dart';
import 'package:scan_and_go_flutter/screens/register_screen.dart';
import 'package:scan_and_go_flutter/screens/tutorial_screen.dart';
import 'package:scan_and_go_flutter/services/http_service.dart';
import 'package:scan_and_go_flutter/models/user.dart';
import 'package:scan_and_go_flutter/services/auth_service.dart';
import 'package:scan_and_go_flutter/widgets/error_snackbar.dart';

class Login extends StatefulWidget {
  const Login({
    super.key,
  });

  @override
  State<Login> createState() => _LoginState();
}

class _LoginState extends State<Login> {
  final _formKey = GlobalKey<FormState>();
  final TextEditingController _emailController = TextEditingController();
  final TextEditingController _passwordController = TextEditingController();
  RegExp emailRegex = RegExp(r'^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$');
  final authService = AuthService();

  final GlobalKey _loginButtonKey = GlobalKey();


   @override
  void initState() {
    super.initState();
    _logout();
  }

  Future<void> _logout() async {
    await authService.logout();
  }

  Future<void> _loginButton() async {
    if (_formKey.currentState!.validate()) {
      LoginUserDto loginDto = LoginUserDto(
        email: _emailController.text,
        password: _passwordController.text,
      );

      final response = await HttpService().httpPost(AppConfig.userLoginEndpoint, loginDto);

      if (response.statusCode == 200) {
        ResponseDto responseDto = parseResponse(response.body);
        await authService.login(responseDto.token, responseDto.userDto);

        _onLoginSuccess();
      } else {
        StatusMessageDto statusMessageDto = StatusMessageDto.fromJson(jsonDecode(response.body));
        ErrorSnackbar.show(statusMessageDto.message);
      }
    }
  }

  void _onLoginSuccess(){
    Navigator.of(context).pushReplacement(MaterialPageRoute(builder: (ctx) => const HomeScreen()));
  }

  void _navigateToForgotPasswordScreen(){
    Navigator.of(context).push(MaterialPageRoute(builder: (ctx) => const ForgotPasswordScreen()));
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Login Form'),
        actions: [
          ElevatedButton(
            onPressed: () {
              Navigator.of(context).pushReplacement(MaterialPageRoute(builder: (ctx) => const RegisterScreen()));
            },
            child: const Text('Register'),
          ),
        ],
      ),
      body: Padding(
        padding: const EdgeInsets.all(16.0),
        child: Column(
          children: [
            Form(
              key: _formKey,
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.stretch,
                children: <Widget>[
                  TextFormField(
                    controller: _emailController,
                    decoration: const InputDecoration(labelText: 'Email'),
                    keyboardType: TextInputType.emailAddress,
                    validator: (value) {
                      if (value == null || value.isEmpty) {
                        return 'Please enter your email';
                      }
                      if (!emailRegex.hasMatch(value)) {
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
                      if (value == null || value.isEmpty) {
                        return 'Your password is empty';
                      }
                      return null;
                    },
                  ),
                  const SizedBox(height: 20),
                  ElevatedButton(
                    key: _loginButtonKey,
                    onPressed: _loginButton,
                    child: const Text('Log in'),
                  ),
                ],
              ),
            ),
            const SizedBox(height: 50,),
            Row(
  children: [
    Expanded(
      flex: 1,
      child: ElevatedButton(
        onPressed: _navigateToForgotPasswordScreen,
        child: const Text("Forgot password"),
      ),
    ),
    const SizedBox(width: 8),
    Expanded(
      flex: 1,
      child: ElevatedButton(
        onPressed: () {
          Navigator.of(context).pushReplacement(MaterialPageRoute(builder: (ctx) => const TutorialScreen()));
        },
        child: const Text("Show tutorial"),
      ),
    ),
  ],
),

          ],
        ),
      ),
    );
  }

  @override
  void dispose() {
    _emailController.dispose();
    _passwordController.dispose();
    super.dispose();
  }
}