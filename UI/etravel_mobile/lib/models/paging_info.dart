class PagingInfo {
  final int totalCount;
  final int pageSize;

  int currentPage;

  PagingInfo({
    required this.totalCount,
    required this.pageSize,
    required this.currentPage,
  });

  int get totalPages {
    if (totalCount == 0) return 1;
    return (totalCount / pageSize).ceil();
  }

  bool get hasNext => currentPage < totalPages - 1;
  bool get hasPrevious => currentPage > 0;

  void next() {
    if (hasNext) currentPage++;
  }

  void previous() {
    if (hasPrevious) currentPage--;
  }

  void goTo(int page) {
    if (page >= 0 && page < totalPages) {
      currentPage = page;
    }
  }

  List<int> get pageNumbers {
    return List.generate(totalPages, (i) => i);
  }
}
