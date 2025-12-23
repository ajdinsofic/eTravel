class OfferImageDisplay {
  int? id;                   
  String path;               
  bool isMain;               
  bool isNetwork;            

  OfferImageDisplay({
    this.id,
    required this.path,
    this.isMain = false,
    this.isNetwork = false,
  });
}
