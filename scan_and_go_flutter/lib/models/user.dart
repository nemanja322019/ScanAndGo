
import 'dart:convert';

class LoginUserDto {
  final String email;
  final String password;

  const LoginUserDto({
    required this.email,
    required this.password,
  });

  Map<String, dynamic> toJson() {
    return {
      'Email': email,
      'Password': password,
    };
  }
}

class RegisterUserDTO {
  final String name;
  final String email;
  final String password;

  const RegisterUserDTO({
    required this.name,
    required this.email,
    required this.password,
  });

  Map<String, dynamic> toJson() {
    return {
      'Name': name,
      'Email': email,
      'Password': password,
    };
  }
}

class BuyerVerifyDto {
  final String email;
  final String verificationcode;

  const BuyerVerifyDto({
    required this.email,
    required this.verificationcode,
  });

  Map<String, dynamic> toJson() {
    return {
      'Email': email,
      'VerificationCode': verificationcode,
    };
  }
}

class EmailDto {
  final String email;

  const EmailDto({
    required this.email,
  });

  Map<String, dynamic> toJson() {
    return {
      'Email': email,
    };
  }
}

class CompleteProfileDto {
  final String email;
  final String address;

  const CompleteProfileDto({
    required this.email,
    required this.address,
  });

  Map<String, dynamic> toJson() {
    return {
      'Email': email,
      'Address': address,
    };
  }
}

class ResetPasswordDto {
  final String email;
  final String emailToken;
  final String oldPassword;
  final String newPassword;

  const ResetPasswordDto({
    required this.email,
    required this.emailToken,
    required this.oldPassword,
    required this.newPassword,
  });

  Map<String, dynamic> toJson() {
    return {
      'Email': email,
      'EmailToken': emailToken,
      'OldPassword': oldPassword,
      'NewPassword': newPassword,
    };
  }
}

class UserDto {
  final int id;
  final String name;
  final String email;

  UserDto({
    required this.id,
    required this.name,
    required this.email,
  });

   Map<String, dynamic> toJson() {
    return {
      'Id': id,
      'Name': name,
      'Email': email,
    };
  }

  factory UserDto.fromJson(Map<String, dynamic> json) {
    return UserDto(
      id: json['Id'],
      name: json['Name'],
      email: json['Email'],
    );
  }
}

class ResponseDto {
  final String token;
  final UserDto userDto;
  final String result;

  ResponseDto({
    required this.token,
    required this.userDto,
    required this.result
  });

  factory ResponseDto.fromJson(Map<String, dynamic> json) {
    return ResponseDto(
      token: json['Token'],
      userDto: UserDto.fromJson(json['UserDto']),
      result: json['Result'],
    );
  }
}

ResponseDto parseResponse(String responseBody) {
  final parsed = jsonDecode(responseBody);
  return ResponseDto.fromJson(parsed);
}
