import 'dart:io';
import 'package:dotted_border/dotted_border.dart';
import 'package:etravel_desktop/config/api_config.dart';
import 'package:etravel_desktop/helper/date_picker.dart';
import 'package:etravel_desktop/models/hotel_form_data.dart';
import 'package:etravel_desktop/models/hotel_image_insert.dart';
import 'package:etravel_desktop/models/hotel_room_insert.dart';
import 'package:etravel_desktop/providers/hotel_image_provider.dart';
import 'package:etravel_desktop/screens/hotel_room_mini_popup.dart';
import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';
import 'package:image_picker/image_picker.dart';
import 'package:provider/provider.dart';

class HotelCard extends StatefulWidget {
  final HotelFormData data;
  final VoidCallback onDelete;
  final VoidCallback onChanged;
  final int daysCount;
  final bool isReadOnly;

  const HotelCard({
    super.key,
    required this.data,
    required this.onDelete,
    required this.onChanged,
    required this.daysCount,
    required this.isReadOnly,
  });

  @override
  State<HotelCard> createState() => _HotelCardState();
}

class _HotelCardState extends State<HotelCard> {
  late HotelImageProvider _hotelImageProvider;
  late DatePickerHelper datePicker;

  late TextEditingController _nameCtrl;
  late TextEditingController _addressCtrl;
  late TextEditingController _starsCtrl;
  late TextEditingController _departure;
  late TextEditingController _return;

  HotelImageInsertRequest? previewImage;
  HotelImageInsertRequest? hoveredThumb;

  bool _hoverMainImage = false;
  int _thumbStart = 0;
  final int _thumbCount = 3;

  @override
  void initState() {
    super.initState();

    _hotelImageProvider = Provider.of<HotelImageProvider>(context, listen: false);

    _nameCtrl = TextEditingController(text: widget.data.name);
    _addressCtrl = TextEditingController(text: widget.data.address);
    _starsCtrl = TextEditingController(
      text: widget.data.stars == 0 ? "" : widget.data.stars.toString(),
    );

    _departure = TextEditingController(text: widget.data.departureDate);
    _return = TextEditingController(text: widget.data.returnDate);

    datePicker = DatePickerHelper(
      departureController: _departure,
      returnController: _return,
    );

    if (widget.data.hotelId != null) {
      _loadExistingImages(widget.data.hotelId!);
    }
  }

  // ============================================================
  // MAIN BUILD
  // ============================================================

  @override
  Widget build(BuildContext context) {
    return Container(
      margin: const EdgeInsets.only(bottom: 25),
      padding: const EdgeInsets.all(20),
      decoration: BoxDecoration(
        color: const Color(0xFFD9D9D9),
        borderRadius: BorderRadius.circular(14),
        boxShadow: const [BoxShadow(blurRadius: 6, color: Colors.black12)],
      ),
      child: Row(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          // LEFT SIDE — IMAGES + ROOMS
          Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              widget.data.images.isEmpty ? _emptyUploadBox() : _imagesSection(),

              if (widget.data.imagesError != null)
                Padding(
                  padding: const EdgeInsets.only(top: 6),
                  child: Text(widget.data.imagesError!,
                      style: const TextStyle(color: Colors.red)),
                ),

              const SizedBox(height: 20),

              _editRoomsButton(),

              if (widget.data.roomsError != null)
                Padding(
                  padding: const EdgeInsets.only(top: 6),
                  child: Text(widget.data.roomsError!,
                      style: const TextStyle(color: Colors.red)),
                ),
            ],
          ),

          const SizedBox(width: 30),

          // RIGHT SIDE — FORM
          Expanded(
            child: AbsorbPointer(
              absorbing: widget.isReadOnly,
              child: Opacity(
                opacity: widget.isReadOnly ? 0.7 : 1,
                child: _formSection(),
              ),
            ),
          ),
        ],
      ),
    );
  }

  // ============================================================
  // FORM SECTION
  // ============================================================

  Widget _formSection() {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        Align(
          alignment: Alignment.topRight,
          child: IconButton(
            onPressed: widget.onDelete,
            icon: const Icon(Icons.close),
          ),
        ),

        // IME HOTELA
        _label("Ime hotela"),
        TextField(
          controller: _nameCtrl,
          onChanged: (v) {
            widget.data.name = v;
            widget.data.nameError = null;
            widget.onChanged();
            setState(() {});
          },
          decoration: _input("unesite ime hotela", widget.data.nameError),
        ),
        if (widget.data.nameError != null) _error(widget.data.nameError!),

        const SizedBox(height: 15),

        // ADRESA
        _label("Adresa"),
        TextField(
          controller: _addressCtrl,
          onChanged: (v) {
            widget.data.address = v;
            widget.data.addressError = null;
            widget.onChanged();
            setState(() {});
          },
          decoration: _input("unesite adresu hotela", widget.data.addressError),
        ),
        if (widget.data.addressError != null) _error(widget.data.addressError!),

        const SizedBox(height: 15),

        // BROJ ZVIJEZDA
        _label("Broj zvijezda"),
        TextField(
          controller: _starsCtrl,
          keyboardType: TextInputType.number,
          onChanged: (v) {
            widget.data.stars = int.tryParse(v) ?? 0;
            widget.data.starsError = null;
            widget.onChanged();
            setState(() {});
          },
          decoration: _input("unesite broj zvijezda", widget.data.starsError),
        ),
        if (widget.data.starsError != null) _error(widget.data.starsError!),

        const SizedBox(height: 15),

        // DATUM POLASKA
        _label("Datum polaska"),
        TextField(
          controller: _departure,
          readOnly: true,
          onTap: () async {
            await datePicker.pickDate(
              context: context,
              isDeparture: true,
              daysCount: widget.daysCount,
            );

            widget.data.departureDate = _departure.text;
            widget.data.returnDate = _return.text;
            widget.data.dateError = null;

            widget.onChanged();
            setState(() {});
          },
          decoration: _input("unesite datum polaska", null),
        ),

        const SizedBox(height: 5),

        // DATUM VRAĆANJA (auto)
        _label("Datum vraćanja"),
        TextField(
          controller: _return,
          readOnly: true,
          decoration: _input("datum vraćanja (auto)", null),
        ),

        if (widget.data.dateError != null) _error(widget.data.dateError!),
      ],
    );
  }

  // ============================================================
  // LABEL + ERROR + INPUT
  // ============================================================

  Widget _label(String text) => Padding(
        padding: const EdgeInsets.only(bottom: 5),
        child: Text(text,
            style: GoogleFonts.openSans(
                fontSize: 20, fontWeight: FontWeight.w700)),
      );

  Widget _error(String text) => Padding(
        padding: const EdgeInsets.only(top: 4),
        child: Text(text, style: const TextStyle(color: Colors.red)),
      );

  InputDecoration _input(String hint, String? error) {
    return InputDecoration(
      hintText: hint,
      filled: true,
      fillColor: Colors.white,
      border: OutlineInputBorder(
        borderRadius: BorderRadius.circular(10),
        borderSide: BorderSide(
          color: error != null ? Colors.red : Colors.grey,
          width: error != null ? 2 : 1,
        ),
      ),
    );
  }

  // ============================================================
  // IMAGE UPLOAD — EMPTY
  // ============================================================

  Widget _emptyUploadBox() {
    return GestureDetector(
      onTap: widget.isReadOnly ? null : _pickImage,
      child: Container(
        width: 300,
        height: 350,
        margin: const EdgeInsets.only(top: 40),
        decoration: BoxDecoration(
          color: const Color(0xFFF2F2F2),
          borderRadius: BorderRadius.circular(12),
        ),
        child: DottedBorder(
          dashPattern: const [6, 4],
          borderType: BorderType.RRect,
          radius: const Radius.circular(12),
          color: Colors.black,
          child: Center(
            child: Column(
              mainAxisSize: MainAxisSize.min,
              children: [
                Text("dodajte slike hotela",
                    style: GoogleFonts.openSans(
                        fontSize: 20, fontWeight: FontWeight.bold)),
                const SizedBox(height: 15),
                const CircleAvatar(
                  radius: 30,
                  backgroundColor: Color(0xFFD9D9D9),
                  child: Icon(Icons.add, color: Colors.white, size: 34),
                ),
              ],
            ),
          ),
        ),
      ),
    );
  }

  // ============================================================
  // IMAGE SECTION — MAIN IMAGE + THUMBNAILS
  // ============================================================

  Widget _imagesSection() {
    final images = widget.data.images;
    final mainImage =
        previewImage ??
            images.firstWhere((img) => img.isMain, orElse: () => images.first);

    return Column(
      children: [
        MouseRegion(
          onEnter: widget.isReadOnly ? null : (_) => setState(() => _hoverMainImage = true),
          onExit: widget.isReadOnly ? null : (_) => setState(() => _hoverMainImage = false),
          child: Stack(
            children: [
              Container(
                width: 300,
                height: 320,
                margin: const EdgeInsets.only(top: 40),
                decoration: BoxDecoration(
                  borderRadius: BorderRadius.circular(12),
                  image: DecorationImage(
                    image: _getImageProvider(mainImage),
                    fit: BoxFit.cover,
                  ),
                ),
              ),

              if (mainImage.isMain)
                Positioned(
                  top: 50,
                  right: 10,
                  child: Container(
                    padding:
                        const EdgeInsets.symmetric(horizontal: 10, vertical: 6),
                    decoration: BoxDecoration(
                      color: Colors.blueAccent,
                      borderRadius: BorderRadius.circular(20),
                    ),
                    child: const Text("Glavna slika",
                        style: TextStyle(color: Colors.white)),
                  ),
                ),

              if (_hoverMainImage && !widget.isReadOnly)
                Positioned.fill(
                  child: Container(
                    margin: const EdgeInsets.only(top: 40),
                    decoration: BoxDecoration(
                      color: Colors.black45,
                      borderRadius: BorderRadius.circular(12),
                    ),
                    child: Column(
                      mainAxisAlignment: MainAxisAlignment.center,
                      children: [
                        ElevatedButton(
                          onPressed: () async {
                            await setMainImage(mainImage);
                          },
                          child: const Text("označi kao glavnu sliku"),
                        ),
                        const SizedBox(height: 10),

                        ElevatedButton(
                          onPressed: _pickImage,
                          style: ElevatedButton.styleFrom(
                            backgroundColor: const Color(0xFF64B5F6),
                          ),
                          child: const Text("dodaj dodatnu sliku",
                              style: TextStyle(color: Colors.white)),
                        ),

                        const SizedBox(height: 10),

                        ElevatedButton(
                          onPressed: () async {
                            if (mainImage.id != null) {
                              await _hotelImageProvider.delete(mainImage.id!);
                            }
                            _deleteLocal(mainImage);
                          },
                          style: ElevatedButton.styleFrom(
                              backgroundColor: Colors.redAccent),
                          child: const Text("izbriši trenutnu sliku",
                              style: TextStyle(color: Colors.white)),
                        ),
                      ],
                    ),
                  ),
                ),
            ],
          ),
        ),

        const SizedBox(height: 12),
        _thumbnails(),
      ],
    );
  }

  // ============================================================
  // THUMBNAILS
  // ============================================================

  Widget _thumbnails() {
    final images = widget.data.images;

    if (images.length <= 1) return const SizedBox();

    final end = (_thumbStart + _thumbCount).clamp(0, images.length);
    final visible = images.sublist(_thumbStart, end);

    return SizedBox(
      width: 300,
      child: Row(
        mainAxisAlignment: MainAxisAlignment.center,
        children: [
          if (_thumbStart > 0)
            IconButton(
                onPressed: () => setState(() => _thumbStart--),
                icon: const Icon(Icons.arrow_back_ios, size: 18))
          else
            const SizedBox(width: 40),

          Row(
            children: visible.map((img) {
              final isHovered = hoveredThumb == img;

              return MouseRegion(
                onEnter: (_) => setState(() => hoveredThumb = img),
                onExit: (_) => setState(() => hoveredThumb = null),
                child: GestureDetector(
                  onTap: () {
                    setState(() {
                      previewImage = img;
                    });
                  },
                  child: Container(
                    margin: const EdgeInsets.symmetric(horizontal: 3),
                    width: 65,
                    height: 65,
                    decoration: BoxDecoration(
                      borderRadius: BorderRadius.circular(8),
                      border: Border.all(
                        color: isHovered ? Colors.blueAccent : Colors.transparent,
                        width: 2,
                      ),
                      image: DecorationImage(
                        image: _getImageProvider(img),
                        fit: BoxFit.cover,
                      ),
                    ),
                  ),
                ),
              );
            }).toList(),
          ),

          if (_thumbStart + _thumbCount < images.length)
            IconButton(
                onPressed: () => setState(() => _thumbStart++),
                icon: const Icon(Icons.arrow_forward_ios, size: 18))
          else
            const SizedBox(width: 40),
        ],
      ),
    );
  }

  // ============================================================
  // PICK IMAGE
  // ============================================================

  Future<void> _pickImage() async {
    final picker = ImagePicker();
    final XFile? file = await picker.pickImage(source: ImageSource.gallery);
    if (file == null) return;

    // spriječiti duplikate
    if (widget.data.images.any((img) => img.image?.path == file.path)) {
      ScaffoldMessenger.of(context)
          .showSnackBar(const SnackBar(content: Text("Ova slika je već dodana.")));
      return;
    }

    setState(() {
      widget.data.images.add(
        HotelImageInsertRequest(
          hotelId: widget.data.hotelId ?? 0,
          isMain: false,
          image: File(file.path),
        ),
      );
    });

    widget.data.imagesError = null;
    widget.onChanged();
  }

  // ============================================================
  // DELETE LOCAL IMAGE
  // ============================================================

  void _deleteLocal(HotelImageInsertRequest img) {
    final wasMain = img.isMain;

    widget.data.images.remove(img);

    if (widget.data.images.isEmpty) {
      previewImage = null;
      widget.onChanged();
      setState(() {});
      return;
    }

    previewImage = widget.data.images.first;

    if (wasMain) {
      for (var i in widget.data.images) {
        i.isMain = false;
      }
    }

    widget.onChanged();
    setState(() {});
  }

  // ============================================================
  // LOAD EXISTING IMAGES FROM BACKEND
  // ============================================================

  Future<void> _loadExistingImages(int hotelId) async {
    final result = await _hotelImageProvider.get(filter: {"hotelId": hotelId});

    widget.data.images = result.items.map((remote) {
      return HotelImageInsertRequest(
        id: remote.imageId,
        hotelId: hotelId,
        isMain: remote.isMain ?? false,
        imageUrl: "${ApiConfig.imagesHotels}/${remote.imageUrl}",
        image: File(""),
      );
    }).toList();

    if (!widget.data.images.any((i) => i.isMain) &&
        widget.data.images.isNotEmpty) {
      widget.data.images.first.isMain = true;
    }

    setState(() {});
  }

  // ============================================================
  // SET MAIN IMAGE
  // ============================================================

  Future<void> setMainImage(HotelImageInsertRequest img) async {
    final hotelId = widget.data.hotelId;

    for (var i in widget.data.images) {
      i.isMain = false;
    }

    img.isMain = true;
    previewImage = null;
    setState(() {});
    widget.onChanged();

    if (hotelId == null) return;

    try {
      for (var other in widget.data.images) {
        if (other.id == null) continue;

        final bool shouldBeMain = (other == img);

        await _hotelImageProvider.update(other.id!, {
          "hotelId": hotelId,
          "isMain": shouldBeMain,
        });
      }
    } catch (e) {
      debugPrint("❌ setMainImage error: $e");
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(content: Text("Greška pri postavljanju glavne slike.")),
      );
    }
  }

  // ============================================================
  // ROOM EDITOR BUTTON
  // ============================================================

  Widget _editRoomsButton() {
    return SizedBox(
      width: 300,
      child: ElevatedButton(
        onPressed: () async {
          final result = await showDialog(
            context: context,
            builder: (_) => RoomEditorPopup(
              hotelId: widget.data.hotelId,
              initialRooms: widget.data.selectedRooms,
              isReadOnly: widget.isReadOnly,
            ),
          );

          if (result != null) {
            setState(() {
              widget.data.selectedRooms =
                  List<HotelRoomInsertRequest>.from(result);
            });

            widget.data.roomsError = null;
            widget.onChanged();
          }
        },
        style: ElevatedButton.styleFrom(
          backgroundColor: const Color(0xFF64B5F6),
          foregroundColor: Colors.white,
          padding: const EdgeInsets.symmetric(vertical: 14),
          shape: RoundedRectangleBorder(
            borderRadius: BorderRadius.circular(10),
          ),
        ),
        child: const Text("Uredite tipove i broj soba"),
      ),
    );
  }

  // ============================================================
  // IMAGE PROVIDER
  // ============================================================

  ImageProvider _getImageProvider(HotelImageInsertRequest img) {
    if (img.imageUrl != null && img.imageUrl!.isNotEmpty) {
      return NetworkImage(img.imageUrl!);
    }
    return FileImage(img.image!);
  }
}
