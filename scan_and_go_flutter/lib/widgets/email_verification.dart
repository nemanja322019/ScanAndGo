
import 'dart:convert';

import 'package:flutter/material.dart';
import 'package:scan_and_go_flutter/app_config.dart';
import 'package:scan_and_go_flutter/models/status_message.dart';
import 'package:scan_and_go_flutter/models/user.dart';
import 'package:scan_and_go_flutter/services/http_service.dart';
import 'package:scan_and_go_flutter/widgets/error_snackbar.dart';
import 'package:scan_and_go_flutter/widgets/profile_completion.dart';

class EmailVerification extends StatefulWidget{
  const EmailVerification({
    super.key,
    required this.email,
  });

  final String email;

  @override
  State<EmailVerification> createState() => _EmailVerificationState();
}

class _EmailVerificationState extends State<EmailVerification> {
  final _formKey = GlobalKey<FormState>();
  final TextEditingController _verificationcode = TextEditingController();

  Future<void> _verifyEmailButton() async {
    if (_formKey.currentState!.validate()) {
        BuyerVerifyDto buyerVerifyDto = BuyerVerifyDto(
        email: widget.email,
        verificationcode: _verificationcode.text,
      );

      final response = await HttpService().httpPost(AppConfig.userVerificationEndpoint, buyerVerifyDto);
      
      if (response.statusCode == 200) {
        _navigateToProfileCompletion();
      }
      else{
        StatusMessageDto statusMessageDto = StatusMessageDto.fromJson(jsonDecode(response.body));
        ErrorSnackbar.show(statusMessageDto.message);
      }
    }
  }

  void _navigateToProfileCompletion(){
    Navigator.of(context).pushReplacement(MaterialPageRoute(builder: (ctx) => ProfileCompletion(email: widget.email,)));
  }

  Future<void> _resendEmailButton() async {
    EmailDto emailDto = EmailDto(
      email: widget.email,
    );

    final response = await HttpService().httpPost(AppConfig.userResendVerificationEmailEndpoint, emailDto);
    if (response.statusCode == 200) {
        StatusMessageDto statusMessageDto = StatusMessageDto.fromJson(jsonDecode(response.body));
        ErrorSnackbar.show(statusMessageDto.message);
      }
      else{
        StatusMessageDto statusMessageDto = StatusMessageDto.fromJson(jsonDecode(response.body));
        ErrorSnackbar.show(statusMessageDto.message);
      }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Verify your email'),
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
                    controller: _verificationcode,
                    decoration: const InputDecoration(labelText: 'Enter the code you recieved'),
                    validator: (value) {
                      if (value == null ||
                          value.isEmpty ||
                          value.trim().length <= 1 ||
                          value.trim().length > 50) {
                        return 'Please enter verification code';
                      }
                      return null;
                    },
                  ),
                  const SizedBox(height: 20),
                  ElevatedButton(
                    onPressed: _verifyEmailButton,
                    child: const Text('Verify'),
                  ),
                ],
              ),
            ),
            const SizedBox(height: 30,),
            ElevatedButton(onPressed: _resendEmailButton, child: const Text("Resend email")),
          ],
        ),
      ),
    );
  }

  @override
  void dispose() {
    _verificationcode.dispose();
    super.dispose();
  }
}