
import 'package:flutter/material.dart';
import 'package:scan_and_go_flutter/screens/home_screen.dart';

class QrcodeScreen extends StatelessWidget {
  const QrcodeScreen({
    super.key,
    required this.qrCodeUrl,
  });

  final String qrCodeUrl;

  @override
  Widget build(BuildContext context) {
    
    return Scaffold(
      appBar: AppBar(
        title: const Text("Generating your QR code.."),
        automaticallyImplyLeading: false,
        leading: null,
      ),
      body: Center(
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            Expanded(
              child: Column(
                mainAxisAlignment: MainAxisAlignment.center,
                children: [
                  Image.network(
                    qrCodeUrl,
                    width: 200,
                    height: 200,
                  ),
                  const SizedBox(height: 20),
                ],
              ),
            ),
            ElevatedButton(
              onPressed: () {
                Navigator.of(context).pushReplacement(MaterialPageRoute(builder: (ctx) => const HomeScreen()));
              },
              child: const Text('Return to Main Page'),
            ),
            const SizedBox(height: 20),
          ],
        ),
      ),
    );
  }
}