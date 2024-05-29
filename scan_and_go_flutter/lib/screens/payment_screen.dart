
import 'package:flutter/material.dart';
import 'package:scan_and_go_flutter/screens/qrcode_screen.dart';
import 'package:webview_flutter/webview_flutter.dart';


class PaymentScreen extends StatefulWidget{
  const PaymentScreen({
    super.key,
    required this.initialUrl,
  });

  final String initialUrl;

  @override
  State<PaymentScreen> createState() => _PaymentScreenState();
}

class _PaymentScreenState extends State<PaymentScreen> {
  late WebViewController controller;

  @override
  void initState() {
    super.initState();
    controller = WebViewController()
      ..setJavaScriptMode(JavaScriptMode.unrestricted)
      ..setBackgroundColor(const Color(0x00000000))
      ..setNavigationDelegate(
        NavigationDelegate(
          onProgress: (int progress) {},
          onPageStarted: (String url) {},
          onPageFinished: (String url) {
            setupJavascriptChannel(); // Set up JavaScript channel when the page finishes loading
            sendJavascriptMessage();
          },
          onWebResourceError: (WebResourceError error) {},
          onNavigationRequest: (NavigationRequest request) {
            // if (request.url.startsWith('https://www.youtube.com/')) {
            //   return NavigationDecision.prevent;
            // }
            return NavigationDecision.navigate;
          },
          // onHttpAuthRequest: (HttpAuthRequest request) {
            
          // },
        ),
      )
      ..loadRequest(Uri.parse(widget.initialUrl));
  }

  void setupJavascriptChannel(){
    controller.addJavaScriptChannel(
      'flutter_inject',
      onMessageReceived: (JavaScriptMessage message) {
        navigateToQrCodeScreen(message.message);
      },
    );
  }
  void sendJavascriptMessage() {
  controller.runJavaScript('''
    window.flutter_inject.postMessage(document.body.innerText);
  ''');
  }

  void navigateToQrCodeScreen(String qrCode){
    Navigator.of(context).pushAndRemoveUntil(MaterialPageRoute(builder: (ctx) => QrcodeScreen(qrCodeUrl: qrCode)), (route) => false);
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text('Enter your card')),
      body: Column(
        children: [
          Expanded(child: WebViewWidget(controller: controller)),
        ],
      ),
    );
  }
}