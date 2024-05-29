import 'package:scan_and_go_flutter/models/user.dart';
import 'package:scan_and_go_flutter/services/shared_preferences_service.dart';
import 'package:scan_and_go_flutter/services/storage_service.dart';

class AuthService{
  final storageService = StorageService();

  Future<void> logout() async {
    await storageService.deleteToken();
    await storageService.deleteUser();

    await setLoggedInSharedPreferencesToFalse();
  }

  Future<void> login(String token, UserDto userDto) async {
    await storageService.saveToken(token);
    await storageService.saveUser(userDto);

    await setLoggedInSharedPreferencesToTrue();
  }

  Future<String?> getToken() async {
    return await storageService.getToken();
  }

  Future<UserDto?> getUser() async {
    return await storageService.getUser();
  }

}