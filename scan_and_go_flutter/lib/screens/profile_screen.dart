import 'dart:async';

import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:scan_and_go_flutter/services/shared_preferences_service.dart';
import 'package:scan_and_go_flutter/widgets/edit_profile.dart';
import 'package:scan_and_go_flutter/widgets/error_snackbar.dart';
import 'package:scan_and_go_flutter/widgets/password_reset.dart';
import 'package:uni_links/uni_links.dart';

class ProfileScreen extends StatefulWidget{
  const ProfileScreen({
    super.key,
  });

  @override
  State<ProfileScreen> createState() => _ProfileScreenState();
}

class _ProfileScreenState extends State<ProfileScreen> {
  late StreamSubscription<Uri?> _uriLinkSubscription;

  @override
  void initState() {
    super.initState();
    initUniLinks();
  }

  @override
  void dispose() {
    _uriLinkSubscription.cancel();
    super.dispose();
  }
  
 Future<void> initUniLinks() async {
  try {
    Uri? initialUri = await getInitialUri();
    if (initialUri != null) {
      await handleDeepLink(initialUri);
    }
    _uriLinkSubscription = uriLinkStream.listen((Uri? uri) {
      if (uri != null) {
        handleDeepLink(uri);
      }
      }, onError: (err) {
        ErrorSnackbar.show('Oops, something went wrong');
      });
    } on PlatformException {
      // Handle exception
    }
  }

  Future<void> handleDeepLink(Uri uri) async {
    if(await checkIfIsFirstReset()){
      await setFirstResetToFalse();
      String email = uri.queryParameters['email'] ?? '';
      String emailToken = Uri.decodeComponent(uri.queryParameters['emailToken'] ?? '');

      await Future.delayed(const Duration(seconds: 1));

      _navigateToReset(email, emailToken);
    }
  }

  void _navigateToReset(String email, String emailToken){
    Navigator.of(context).push(MaterialPageRoute(builder: (ctx) => PasswordReset(email: email, emailToken: emailToken)));
  }

  @override
  Widget build(BuildContext context) {
    return const EditProfile();
  }
}