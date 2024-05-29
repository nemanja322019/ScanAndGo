import 'package:flutter/material.dart';
import 'package:scan_and_go_flutter/screens/register_screen.dart';

class TutorialScreen extends StatefulWidget {
  const TutorialScreen({
    super.key,
  });

  @override
  State<TutorialScreen> createState() => _TutorialScreenState();
}

class _TutorialScreenState extends State<TutorialScreen> {
  bool tutorialStarted = false;

  final List<String> images = [
    'assets/image1.png',
    'assets/image2.png',
    'assets/image3.png',
    'assets/image4.png',
    'assets/image5.png',
  ];

  final List<String> imageTexts = [
    'Pick a store',
    'Scan a barcode with your camera',
    'Manage your groceries',
    'Pay online',
    'Show QR code on your way out',
  ];

  int currentIndex = 0;
  final PageController _pageController = PageController();

  @override
  void dispose() {
    _pageController.dispose();
    super.dispose();
  }

  void nextPage() {
    if (currentIndex < images.length - 1) {
      _pageController.nextPage(
        duration: const Duration(milliseconds: 300),
        curve: Curves.easeInOut,
      );
    } else {
      Navigator.pushReplacement(
        context,
        MaterialPageRoute(builder: (context) => const RegisterScreen()),
      );
    }
  }

  void previousPage() {
    if (currentIndex > 0) {
      _pageController.previousPage(
        duration: const Duration(milliseconds: 300),
        curve: Curves.easeInOut,
      );
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Tutorial'),
      ),
      body: !tutorialStarted
          ? Column(
              mainAxisAlignment: MainAxisAlignment.center,
              children: [
                const Padding(
                  padding: EdgeInsets.all(20.0),
                  child: Text(
                    "Welcome to Scan and Go, we will now show you around.",
                    style: TextStyle(
                      fontSize: 26,
                    ),
                  ),
                ),
                IconButton(
                  onPressed: () {
                    setState(() {
                      tutorialStarted = true;
                    });
                  },
                  icon: const Icon(
                    Icons.arrow_circle_right_outlined,
                    size: 50,
                    color: Color(0xFF684FA1),
                  ),
                )
              ],
            )
          : Column(
              children: [
                Expanded(
                  child: PageView.builder(
                    itemCount: images.length,
                    controller: _pageController,
                    onPageChanged: (index) {
                      setState(() {
                        currentIndex = index;
                      });
                    },
                    itemBuilder: (context, index) {
                      return Column(
                        children: [
                          Text(
                            imageTexts[index],
                            style: const TextStyle(fontSize: 20, color: Colors.red),
                          ),
                          Expanded(
                            child: Image.asset(
                              images[index],
                              fit: BoxFit.cover,
                            ),
                          ),
                        ],
                      );
                    },
                  ),
                ),
                Row(
                  mainAxisAlignment: MainAxisAlignment.spaceEvenly,
                  children: [
                    IconButton(
                      icon: const Icon(Icons.arrow_back),
                      onPressed: previousPage,
                    ),
                    Text(
                      currentIndex < images.length - 1
                          ? '${currentIndex + 1}/${images.length}'
                          : 'Finish',
                    ),
                    IconButton(
                      icon: const Icon(Icons.arrow_forward),
                      onPressed: nextPage,
                    ),
                  ],
                ),
              ],
            ),
    );
  }
}