import 'package:flutter/material.dart';
import 'package:scan_and_go_flutter/screens/login_screen.dart';
import 'package:scan_and_go_flutter/services/shared_preferences_service.dart';
import 'package:scan_and_go_flutter/widgets/available_stores.dart';

class HomeScreen extends StatefulWidget {
  const HomeScreen({
    super.key,
  });

  @override
  State<HomeScreen> createState() => _HomeScreenState();
}

class _HomeScreenState extends State<HomeScreen> {
  bool _isLoggedIn = false;

  @override
  void initState() {
    _checkisLoggedIn();
    super.initState();
  }

  Future<void> _checkisLoggedIn() async {
    _isLoggedIn = await checkIfIsLoggedIn();
    if(!_isLoggedIn){
      _navigateToLogin();
    }
    setState(() {});
  }
  
  void _navigateToLogin(){
    Navigator.of(context).pushReplacement(MaterialPageRoute(builder: (ctx) => const LoginScreen()));
  }

  @override
  Widget build(BuildContext context) {
    if (!_isLoggedIn) {
      return Container();
    }
    return const Scaffold(
      body: AvailableStores(),
    );
  }
}
