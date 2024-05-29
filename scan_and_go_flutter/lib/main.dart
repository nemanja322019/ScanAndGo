import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:scan_and_go_flutter/screens/login_screen.dart';
import 'package:scan_and_go_flutter/screens/tutorial_screen.dart';

import 'dart:io';

import 'package:scan_and_go_flutter/services/shared_preferences_service.dart';
import 'package:scan_and_go_flutter/widgets/error_snackbar.dart';



class MyHttpOverrides extends HttpOverrides {
  @override
  HttpClient createHttpClient(SecurityContext? context) {
    return super.createHttpClient(context)
      ..badCertificateCallback =
          (X509Certificate cert, String host, int port) => true;
  }
}

Future<void> main() async {
  WidgetsFlutterBinding.ensureInitialized();
  HttpOverrides.global = MyHttpOverrides();
  setFirstResetToTrue();

  runApp(MyApp(isFirstTime: await checkIfIsFirstTime()));
}

class MyApp extends StatelessWidget {
  const MyApp({super.key, required this.isFirstTime});

  final bool isFirstTime;

  
  @override
  Widget build(BuildContext context) {
    SystemChrome.setPreferredOrientations([
      DeviceOrientation.portraitUp,
    ]);
    if (isFirstTime) {
      setFirstTimeInSharedPreferencesToFalse();
      return MaterialApp(
        scaffoldMessengerKey: ErrorSnackbar.scaffoldMessengerKey,
        home: const TutorialScreen(),
      );
    } else {
      return MaterialApp(
        scaffoldMessengerKey: ErrorSnackbar.scaffoldMessengerKey,
        home: const LoginScreen(),
    );
    }
  }
}
