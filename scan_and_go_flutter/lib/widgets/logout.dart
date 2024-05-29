import 'package:flutter/material.dart';
import 'package:scan_and_go_flutter/services/auth_service.dart';

class Logout extends StatelessWidget {
  Logout({
    super.key,
    required this.onLogout,
  });

  final AuthService authService = AuthService();
  final VoidCallback onLogout;

  @override
  Widget build(BuildContext context) {
    return ListTile(
      title: const Text('Logout'),
      onTap: () async {
        await authService.logout();
        onLogout();
      },
    );
  }
}