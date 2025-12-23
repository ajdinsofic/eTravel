import 'package:flutter/material.dart';
import 'package:webview_flutter/webview_flutter.dart';

class PayPalWebViewPage extends StatefulWidget {
  final String approveUrl;
  final String orderId;

  const PayPalWebViewPage({
    super.key,
    required this.approveUrl,
    required this.orderId,
  });

  @override
  State<PayPalWebViewPage> createState() => _PayPalWebViewPageState();
}

class _PayPalWebViewPageState extends State<PayPalWebViewPage> {
  late final WebViewController _controller;

  @override
  void initState() {
    super.initState();

    _controller = WebViewController()
      ..setJavaScriptMode(JavaScriptMode.unrestricted)
      ..setNavigationDelegate(
        NavigationDelegate(
          onNavigationRequest: (request) {
            if (request.url.contains("paypal-success")) {
              Navigator.pop(context, true);
              return NavigationDecision.prevent;
            }

            if (request.url.contains("paypal-cancel")) {
              Navigator.pop(context, false);
              return NavigationDecision.prevent;
            }

            return NavigationDecision.navigate;
          },
        ),
      )
      ..loadRequest(Uri.parse(widget.approveUrl));
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text("PayPal plaćanje")),
      body: WebViewWidget(controller: _controller),
    );
  }
}
