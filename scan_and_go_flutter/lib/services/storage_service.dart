import 'dart:convert';

import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'package:scan_and_go_flutter/models/user.dart';

class StorageService {
  final FlutterSecureStorage _storage = const FlutterSecureStorage();

  Future<UserDto?> getUser() async {
    String? userDtoJson = await _storage.read(key: 'userDto');
    if (userDtoJson != null) {
      return UserDto.fromJson(jsonDecode(userDtoJson));
    } else {
      return null;
    }
  }
  Future<void> saveUser(UserDto userDto) async {
    String userDtoJson = jsonEncode(userDto);
    await _storage.write(key: 'userDto', value: userDtoJson);
  }
  Future<void> deleteUser() async {
    await _storage.delete(key: 'userDto');
  }


  Future<String?> getToken() async {
    return await _storage.read(key: 'token');
  }
  Future<void> saveToken(String token) async {
    await _storage.write(key: 'token', value: token);
  }
  Future<void> deleteToken() async {
    await _storage.delete(key: 'token');
  }
}