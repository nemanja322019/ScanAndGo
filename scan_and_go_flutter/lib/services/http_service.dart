import 'package:http/http.dart' as http;
import 'dart:convert';

class HttpService {
  Future<http.Response> httpPost<T>(String url, T payload, {String? token}) async {
    try {
      final Map<String, String> headers = {'Content-Type': 'application/json'};
      if (token != null) {
        headers['Authorization'] = 'Bearer $token';
      }
      
      final response = await http.post(
        Uri.parse(url),
        body: jsonEncode(payload),
        headers: headers,
      );
      return response;
    } catch (e) {
      return http.Response('Exception during request: $e', 500);
    }
  }

  Future<http.Response> httpPut<T>(String url, T payload, {String? token}) async {
    try {
      final Map<String, String> headers = {'Content-Type': 'application/json'};
      if (token != null) {
        headers['Authorization'] = 'Bearer $token';
      }
      
      final response = await http.put(
        Uri.parse(url),
        body: jsonEncode(payload),
        headers: headers,
      );
      return response;
    } catch (e) {
      return http.Response('Exception during request: $e', 500);
    }
  }


  Future<http.Response> httpGet(String url, {String? token}) async {
    try {
      final Map<String, String> headers = {};
      if (token != null) {
        headers['Authorization'] = 'Bearer $token';
      }

      final response = await http.get(
        Uri.parse(url),
        headers: headers,
      );
      return response;
    } catch (e) {
      return http.Response('Exception during request: $e', 500);
    }
  }

  Future<http.Response> httpDelete<T>(String url, {Map<String, dynamic>? params, String? token}) async {
    try {
      final Map<String, String> headers = {};
      if (token != null) {
        headers['Authorization'] = 'Bearer $token';
      }

      if (params != null && params.isNotEmpty) {
        final queryString = Uri(queryParameters: params.map((key, value) => MapEntry(key, value.toString()))).query;
        url += (url.contains('?') ? '&' : '?') + queryString;
      }

      final response = await http.delete(
        Uri.parse(url),
        headers: headers,
      );
      return response;
    } catch (e) {
      return http.Response('Exception during request: $e', 500);
    }
  }


}