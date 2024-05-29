import 'dart:convert';

import 'package:flutter/material.dart';
import 'package:scan_and_go_flutter/app_config.dart';
import 'package:scan_and_go_flutter/models/status_message.dart';
import 'package:scan_and_go_flutter/models/user.dart';
import 'package:scan_and_go_flutter/screens/home_screen.dart';
import 'package:scan_and_go_flutter/services/auth_service.dart';
import 'package:scan_and_go_flutter/services/http_service.dart';
import 'package:scan_and_go_flutter/services/shared_preferences_service.dart';
import 'package:scan_and_go_flutter/widgets/error_snackbar.dart';

class EditProfile extends StatefulWidget {
  const EditProfile({
    super.key,
  });

  @override
  State<EditProfile> createState() => _EditProfileState();
}

class _EditProfileState extends State<EditProfile> {
  final _formKey = GlobalKey<FormState>();
  final TextEditingController _addressController = TextEditingController();
  final _authService = AuthService();
  late UserDto? userDto;
  late String? token;
  bool isProfileCompleted = true;

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
      isProfileCompleted = await checkIfIsProfileCompleted();
      setState(() {});
    }
  }

  Future<void> _completeProfileButton() async {
    if (_formKey.currentState!.validate()) {
      CompleteProfileDto completeProfileDto = CompleteProfileDto(
        email: userDto!.email,
        address: _addressController.text,
      );

      final response = await HttpService().httpPost(
          AppConfig.userCompleteProfileEndpoint, completeProfileDto,
          token: token);
      if (response.statusCode == 200) {
        ErrorSnackbar.show("Profile updated.");
        setProfileCompletedToTrue();
        _navigateToHomeScreen();
      } else {
        StatusMessageDto statusMessageDto =
            StatusMessageDto.fromJson(jsonDecode(response.body));
        ErrorSnackbar.show(statusMessageDto.message);
      }
    }
  }

  void _navigateToHomeScreen() {
    Navigator.of(context).pushReplacement(
        MaterialPageRoute(builder: (ctx) => const HomeScreen()));
  }

  Future<void> _resetPasswordButton() async {
    final response = await HttpService().httpPost(
        "${AppConfig.userSendResetEmailEndpoint}/${userDto!.email}",
        userDto!.email,
        token: token);
    if (response.statusCode == 200) {
      ErrorSnackbar.show("Check your email.");
    } else {
      StatusMessageDto statusMessageDto =
          StatusMessageDto.fromJson(jsonDecode(response.body));
      ErrorSnackbar.show(statusMessageDto.message);
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Your profile'),
      ),
      body: SingleChildScrollView(
        child: Padding(
          padding: const EdgeInsets.all(16.0),
          child: Column(
            children: [
              !isProfileCompleted ? Form(
                key: _formKey,
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.stretch,
                  children: <Widget>[
                    TextFormField(
                      controller: _addressController,
                      decoration: const InputDecoration(labelText: 'Address'),
                      validator: (value) {
                        if (value == null ||
                            value.isEmpty ||
                            value.trim().length <= 1 ||
                            value.trim().length > 50) {
                          return 'Please enter your address';
                        }
                        return null;
                      },
                    ),
                    const SizedBox(height: 30),
                    ElevatedButton(
                      onPressed: _completeProfileButton,
                      child: const Text('Complete Profile'),
                    ),
                  ],
                ),
              )
              : Container(),
              const SizedBox(
                height: 30,
              ),
              ElevatedButton(
                onPressed: _resetPasswordButton,
                child: const Text("Reset Password"),
              ),
            ],
          ),
        ),
      ),
    );
  }

  @override
  void dispose() {
    _addressController.dispose();
    super.dispose();
  }
}
