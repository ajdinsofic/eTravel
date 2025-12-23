class PayPalOrder {
  final String id;
  final String approveUrl;

  PayPalOrder({
    required this.id,
    required this.approveUrl,
  });

  factory PayPalOrder.fromJson(Map<String, dynamic> json) {
    return PayPalOrder(
      id: json['id'],
      approveUrl: json['approveUrl'],
    );
  }
}
