import 'dart:convert';

import 'package:flutter/material.dart';
import 'package:scan_and_go_flutter/app_config.dart';
import 'package:scan_and_go_flutter/models/status_message.dart';
import 'package:scan_and_go_flutter/models/user.dart';
import 'package:scan_and_go_flutter/screens/login_screen.dart';
import 'package:scan_and_go_flutter/services/http_service.dart';
import 'package:scan_and_go_flutter/services/shared_preferences_service.dart';
import 'package:scan_and_go_flutter/widgets/error_snackbar.dart';

class ProfileCompletion extends StatefulWidget{
  const ProfileCompletion({
    super.key,
    required this.email,
  });

  final String email;

  @override
  State<ProfileCompletion> createState() => _ProfileCompletionState();
}

class _ProfileCompletionState extends State<ProfileCompletion> {
  final _formKey = GlobalKey<FormState>();
  final TextEditingController _addressController = TextEditingController();


  Future<void> _completeProfileButton() async {
    if (_formKey.currentState!.validate()) {
      CompleteProfileDto completeProfileDto = CompleteProfileDto(
        email: widget.email,
        address: _addressController.text,
      );

      final response = await HttpService().httpPost(AppConfig.userCompleteProfileEndpoint, completeProfileDto,);
      if (response.statusCode == 200) {
        ErrorSnackbar.show("Profile updated.");
        _navigateToLoginScreen();
        setProfileCompletedToTrue();
      }
      else{
        StatusMessageDto statusMessageDto = StatusMessageDto.fromJson(jsonDecode(response.body));
        ErrorSnackbar.show(statusMessageDto.message);
      }
    }
  }

  void _navigateToLoginScreen(){
    Navigator.of(context).pushReplacement(MaterialPageRoute(builder: (ctx) => const LoginScreen()));
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        leading: Container(), 
        title: const Text('Complete your profile'),
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
                  const SizedBox(height: 20),
                  ElevatedButton(
                    onPressed: _completeProfileButton,
                    child: const Text('Complete Profile'),
                  ),
                ],
              ),
            ),
            const SizedBox(height: 30,),
            ElevatedButton(onPressed: _navigateToLoginScreen, child: const Text("Skip for now")),
          ],
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