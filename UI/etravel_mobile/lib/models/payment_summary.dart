class PaymentSummary {
  // ======================
  // I RATA
  // ======================
  final bool isFirstRateVisible;
  final bool isFirstRateDisabled;
  final bool? isFirstRatePending;

  // ======================
  // II RATA
  // ======================
  final bool isSecondRateVisible;
  final bool isSecondRateDisabled;
  final bool? isSecondRatePending;

  // ======================
  // III RATA
  // ======================
  final bool isThirdRateVisible;
  final bool isThirdRateDisabled;
  final bool? isThirdRatePending;

  // ======================
  // PREOSTALI IZNOS
  // ======================
  final bool isRemainingVisible;
  final bool isRemainingDisabled;
  final bool? isRemainingPending;

  // ======================
  // PUNI IZNOS
  // ======================
  final bool isFullAmountVisible;
  final bool isFullAmountDisabled;
  final bool? isFullAmountPending;

  final String? paymentDeadline;

  PaymentSummary({
    required this.isFirstRateVisible,
    required this.isFirstRateDisabled,
    required this.isFirstRatePending,

    required this.isSecondRateVisible,
    required this.isSecondRateDisabled,
    required this.isSecondRatePending,

    required this.isThirdRateVisible,
    required this.isThirdRateDisabled,
    required this.isThirdRatePending,

    required this.isRemainingVisible,
    required this.isRemainingDisabled,
    required this.isRemainingPending,

    required this.isFullAmountVisible,
    required this.isFullAmountDisabled,
    required this.isFullAmountPending,

    required this.paymentDeadline
  });

  factory PaymentSummary.fromJson(Map<String, dynamic> json) {
    return PaymentSummary(
      isFirstRateVisible: json['isFirstRateVisible'] as bool,
      isFirstRateDisabled: json['isFirstRateDisabled'] as bool,
      isFirstRatePending: json['isFirstRatePending'] as bool?,

      isSecondRateVisible: json['isSecondRateVisible'] as bool,
      isSecondRateDisabled: json['isSecondRateDisabled'] as bool,
      isSecondRatePending: json['isSecondRatePending'] as bool?,

      isThirdRateVisible: json['isThirdRateVisible'] as bool,
      isThirdRateDisabled: json['isThirdRateDisabled'] as bool,
      isThirdRatePending: json['isThirdRatePending'] as bool?,

      isRemainingVisible: json['isRemainingVisible'] as bool,
      isRemainingDisabled: json['isRemainingDisabled'] as bool,
      isRemainingPending: json['isRemainingPending'] as bool?,

      isFullAmountVisible: json['isFullAmountVisible'] as bool,
      isFullAmountDisabled: json['isFullAmountDisabled'] as bool,
      isFullAmountPending: json['isFullAmountPending'] as bool?,

      paymentDeadline: json['paymentDeadline'] as String? 
    );
  }
}
