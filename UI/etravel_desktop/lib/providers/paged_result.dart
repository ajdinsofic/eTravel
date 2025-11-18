class PagedResult<T> {
  final List<T> items;
  final int totalCount;

  PagedResult({required this.items, required this.totalCount});

  factory PagedResult.fromJson(
    Map<String, dynamic> json,
    T Function(Map<String, dynamic>) create,
  ) {
    final rawList = json["items"] as List<dynamic>? ?? [];

    return PagedResult(
      items: rawList
          .where((e) => e != null && e is Map<String, dynamic>)
          .map((e) => create(e as Map<String, dynamic>))
          .toList(),
      totalCount: json["totalCount"] ?? 0,
    );
  }
}
