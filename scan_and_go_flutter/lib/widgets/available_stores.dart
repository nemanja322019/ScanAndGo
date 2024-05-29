import 'dart:convert';

import 'package:flutter/material.dart';
import 'package:location/location.dart';
import 'package:scan_and_go_flutter/app_config.dart';
import 'package:scan_and_go_flutter/models/store.dart';
import 'package:scan_and_go_flutter/screens/shopping_history_screen.dart';
import 'package:scan_and_go_flutter/screens/login_screen.dart';
import 'package:scan_and_go_flutter/screens/profile_screen.dart';
import 'package:scan_and_go_flutter/screens/store_screen.dart';
import 'package:scan_and_go_flutter/services/auth_service.dart';
import 'package:scan_and_go_flutter/services/http_service.dart';
import 'package:scan_and_go_flutter/services/location_service.dart';
import 'package:scan_and_go_flutter/widgets/error_snackbar.dart';
import 'package:scan_and_go_flutter/widgets/logout.dart';

class AvailableStores extends StatefulWidget {
  const AvailableStores({
    super.key,
  });

  @override
  State<AvailableStores> createState() => _AvailableStoresState();
}

class _AvailableStoresState extends State<AvailableStores> {
  List<Store> stores = [];
  late LocationData _locationData;
  late String? token;
  final _authService = AuthService();

  @override
  void initState() {
    super.initState();
    _getData();
    _getLocation();
  }

  Future<void> _getData() async {
    token = await _authService.getToken();
    if (token == null) {
      await _authService.logout();
    }
  }

  Future<void> _getLocation() async {
    LocationData? locationData = await getLocation();
    if (locationData != null) {
      setState(() {
        _locationData = locationData;
      });
      _getStoresByLocation();
    } else {
      _getAllStores();
    }
  }

  Future<void> _getStoresByLocation() async {
    try {
      LocationDto locationDto = LocationDto(
        latitude: _locationData.latitude!,
        longitude: _locationData.longitude!,
      );

      final response = await HttpService().httpPost(
          AppConfig.storeGetStoresByLocationEndpoint, locationDto.toJson(),
          token: token);
      final responseBody = response.body;

      if (response.statusCode == 200) {
        final List<dynamic> jsonResponse = json.decode(responseBody);
        setState(() {
          stores = jsonResponse
              .map<Store>((storeJson) => Store.fromJson(storeJson))
              .toList();
        });
        _selectStore(stores[0]);
      } else {
        ErrorSnackbar.show('Oops, something went wrong');
      }
    } catch (e) {
      _getAllStores();
    }
  }

  Future<void> _getAllStores() async {
    final response = await HttpService()
        .httpGet(AppConfig.storeGetAllStoresEndpoint, token: token);
    final responseBody = response.body;
    try {
      if (response.statusCode == 200) {
        final List<dynamic> jsonResponse = json.decode(responseBody);
        setState(() {
          stores = jsonResponse
              .map<Store>((storeJson) => Store.fromJson(storeJson))
              .toList();
        });
      } else {
        ErrorSnackbar.show('Oops, no stores to show');
      }
    } catch (e) {
      ErrorSnackbar.show('Oops, cant connect to server');
    }
  }

  void navigateToStoreScreen(store){
    Navigator.of(context).push(MaterialPageRoute(builder: (ctx) => StoreScreen(store: store,),),);
  }

  void _selectStore(Store store) {
    showDialog(
      context: context,
      builder: (BuildContext context) {
        return AlertDialog(
          title: const Text("Are you here?"),
          content: Text("Nearest store is: ${store.name}"),
          actions: <Widget>[
            TextButton(
              onPressed: () {
                Navigator.of(context).pop();
                navigateToStoreScreen(store);
              },
              child: const Text("Confirm"),
            ),
            TextButton(
              onPressed: () {
                Navigator.of(context).pop();
              },
              child: const Text("Cancel"),
            ),
          ],
        );
      },
    );
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Stores near you'),
        actions: [
          IconButton(
            onPressed: _getLocation,
            icon: const Icon(
              Icons.refresh,
              color: Color(0xFF684FA1),
            ),
          )
        ],
        leading: Builder(
          builder: (BuildContext context) {
            return IconButton(
              icon: const Icon(
                Icons.menu_outlined,
                color: Color(0xFF684FA1),
              ),
              onPressed: () {
                Scaffold.of(context).openDrawer();
              },
            );
          },
        ),
      ),
      drawer: Drawer(
        child: ListView(
          padding: EdgeInsets.zero,
          children: <Widget>[
            const DrawerHeader(
              decoration: BoxDecoration(
                color: Color(0xFF684FA1),
              ),
              child: Text(
                'Options',
                style: TextStyle(
                  color: Colors.white,
                  fontSize: 24,
                ),
              ),
            ),
            ListTile(
              title: const Text('Profile'),
              onTap: () {
                Navigator.of(context).push(
                    MaterialPageRoute(builder: (ctx) => const ProfileScreen()));
              },
            ),
            ListTile(
              title: const Text('Shopping History'),
              onTap: () {
                Navigator.of(context).push(MaterialPageRoute(
                    builder: (ctx) => const ShoppingHistoryScreen()));
              },
            ),
            Logout(
              onLogout: () {
                Navigator.of(context).pushReplacement(
                    MaterialPageRoute(builder: (ctx) => const LoginScreen()));
              },
            ),
          ],
        ),
      ),
      body: stores.isEmpty
          ? const Center(
              child: CircularProgressIndicator(),
            )
          : ListView.builder(
              itemCount: stores.length,
              itemBuilder: (BuildContext context, int index) {
                return Container(
                  decoration: BoxDecoration(
                    color: Colors.white,
                    borderRadius: BorderRadius.circular(10.0),
                    boxShadow: [
                      BoxShadow(
                        color: Colors.grey.withOpacity(0.5),
                        spreadRadius: 2,
                        blurRadius: 5,
                        offset: const Offset(0, 3),
                      ),
                    ],
                  ),
                  margin: const EdgeInsets.symmetric(vertical: 5.0, horizontal: 10.0),
                  child: Material(
                    borderRadius: BorderRadius.circular(
                        10.0),
                    clipBehavior: Clip
                        .antiAlias,
                    child: InkWell(
                      onTap: () {
                        navigateToStoreScreen(stores[index]);
                        //_selectStore(stores[index]);
                      },
                      child: Padding(
                        padding:
                            const EdgeInsets.all(26.0),
                        child: Column(
                          crossAxisAlignment: CrossAxisAlignment.start,
                          children: [
                            Row(
                              mainAxisAlignment: MainAxisAlignment.spaceBetween,
                              children: [
                                Text(
                                  stores[index].name,
                                  style: const TextStyle(
                                    fontSize: 22,
                                    fontWeight: FontWeight.bold,
                                    color: Color(0xFF684FA1),
                                  ),
                                ),
                                Text(stores[index].address),
                              ],
                            ),
                            const SizedBox(height: 8), 
                            if (stores[index].distance != null)
                              Text(
                                'Distance: ${stores[index].distance!.toStringAsFixed(2)} km',
                              ),
                          ],
                        ),
                      ),
                    ),
                  ),
                );
              },
            ),
    );
  }
}
