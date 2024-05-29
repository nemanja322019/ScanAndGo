import 'package:shared_preferences/shared_preferences.dart';

Future<bool> checkIfIsFirstTime() async {
  SharedPreferences prefs = await SharedPreferences.getInstance();
  bool? isFirstTime = prefs.getBool('isFirstTime');
  if (isFirstTime == null || isFirstTime) {
    return true;
  }
  return false;
}
Future<void> setFirstTimeInSharedPreferencesToFalse() async {
  SharedPreferences prefs = await SharedPreferences.getInstance();
  await prefs.setBool('isFirstTime', false);
}

Future<bool> checkIfIsLoggedIn() async {
  SharedPreferences prefs = await SharedPreferences.getInstance();
  bool? isLoggedIn = prefs.getBool('isLoggedIn');
  if (isLoggedIn == null || !isLoggedIn) {
    return false;
  }
  return true;
}
Future<void> setLoggedInSharedPreferencesToTrue() async {
  SharedPreferences prefs = await SharedPreferences.getInstance();
  await prefs.setBool('isLoggedIn', true);
}
Future<void> setLoggedInSharedPreferencesToFalse() async {
  SharedPreferences prefs = await SharedPreferences.getInstance();
  await prefs.setBool('isLoggedIn', false);
}

Future<bool> checkIfIsProfileCompleted() async {
  SharedPreferences prefs = await SharedPreferences.getInstance();
  bool? isProfileCompleted = prefs.getBool('isProfileCompleted');
  if (isProfileCompleted == null || isProfileCompleted) {
    return true;
  }
  return false;
}
Future<void> setProfileCompletedToFalse() async {
  SharedPreferences prefs = await SharedPreferences.getInstance();
  await prefs.setBool('isProfileCompleted', false);
}
Future<void> setProfileCompletedToTrue() async {
  SharedPreferences prefs = await SharedPreferences.getInstance();
  await prefs.setBool('isProfileCompleted', true);
}


Future<bool> checkIfIsFirstReset() async {
  SharedPreferences prefs = await SharedPreferences.getInstance();
  bool? isFirstReset = prefs.getBool('isFirstReset');
  if (isFirstReset == null || isFirstReset) {
    return true;
  }
  return false;
}
Future<void> setFirstResetToFalse() async {
  SharedPreferences prefs = await SharedPreferences.getInstance();
  await prefs.setBool('isFirstReset', false);
}
Future<void> setFirstResetToTrue() async {
  SharedPreferences prefs = await SharedPreferences.getInstance();
  await prefs.setBool('isFirstReset', true);
}