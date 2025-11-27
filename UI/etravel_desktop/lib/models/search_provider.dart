class SearchResult<T> {
  int count;
  List<T> items;

  SearchResult({
    this.count = 0,
    List<T>? items,
  }) : items = items ?? [];

  void clear() {
    count = 0;
    items.clear();
  }

  void add(T value) {
    items.add(value);
    count = items.length;
  }
}
