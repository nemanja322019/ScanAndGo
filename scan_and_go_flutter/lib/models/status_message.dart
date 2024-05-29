class StatusMessageDto {
  final String message;
  final int statusCode;

  StatusMessageDto({
    required this.message,
    required this.statusCode,
  });

  factory StatusMessageDto.fromJson(Map<String, dynamic> json) {
    return StatusMessageDto(
      message: json['Message'],
      statusCode: json['StatusCode'],
    );
  }
}