import 'package:etravel_desktop/models/hotel_room_insert.dart';
import 'package:etravel_desktop/models/room.dart';
import 'package:etravel_desktop/providers/hotel_room_provider.dart';
import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';
import 'package:provider/provider.dart';
import '../providers/room_provider.dart';

class RoomEditorPopup extends StatefulWidget {
  final List<HotelRoomInsertRequest> initialRooms;
  final int? hotelId;
  final bool isReadOnly;

  const RoomEditorPopup({
    super.key,
    required this.initialRooms,
    required this.hotelId,
    required this.isReadOnly,
  });

  @override
  State<RoomEditorPopup> createState() => _RoomEditorPopupState();
}

class _RoomEditorPopupState extends State<RoomEditorPopup> {
  List<HotelRoomInsertRequest> rows = [];
  List<Room> availableRoomTypes = [];
  List<TextEditingController> countControllers = [];

  bool isLoading = true;

  late RoomProvider _roomProvider;
  late HotelRoomProvider _hotelRoomProvider;

  @override
  void initState() {
    super.initState();

    _roomProvider = Provider.of<RoomProvider>(context, listen: false);
    _hotelRoomProvider = Provider.of<HotelRoomProvider>(context, listen: false);

    // copy rows & add errors
    rows =
        widget.initialRooms.map((r) {
          return HotelRoomInsertRequest(
            hotelId: r.hotelId,
            roomId: r.roomId,
            roomsLeft: r.roomsLeft,
            isNew: r.isNew,
            roomTypeError: null,
            roomCountError: null,
          );
        }).toList();

    countControllers =
        rows
            .map((r) => TextEditingController(text: r.roomsLeft.toString()))
            .toList();

    loadRoomTypes();
  }

  Future<void> loadRoomTypes() async {
    try {
      final fetched = await _roomProvider.get();

      setState(() {
        availableRoomTypes = fetched.items;
        isLoading = false;
      });
    } catch (e) {
      debugPrint("❌ Greška pri učitavanju soba: $e");
      setState(() => isLoading = false);
    }
  }

  List<Room> getUnusedTypes(int index) {
    final used =
        rows
            .where((r) => r.roomId != 0 && r.roomId != rows[index].roomId)
            .map((r) => r.roomId)
            .toList();

    return availableRoomTypes.where((rt) => !used.contains(rt.id)).toList();
  }

  void addRow() {
    setState(() {
      rows.add(
        HotelRoomInsertRequest(
          hotelId: widget.hotelId,
          roomId: 0,
          roomsLeft: 0,
          isNew: true,
          roomTypeError: null,
          roomCountError: null,
        ),
      );

      countControllers.add(TextEditingController(text: "0"));
    });
  }

  Future<void> deleteRow(int index) async {
    final row = rows[index];

    if (row.hotelId == null || row.roomId == 0) {
      removeRow(index);
      return;
    }

    try {
      await _hotelRoomProvider.deleteRoom(row.hotelId!, row.roomId);
      removeRow(index);
    } catch (e) {
      ScaffoldMessenger.of(
        context,
      ).showSnackBar(SnackBar(content: Text("Greška: $e")));
    }
  }

  void removeRow(int index) {
    setState(() {
      rows.removeAt(index);
      countControllers.removeAt(index);
    });
  }

  @override
  Widget build(BuildContext context) {
    if (isLoading) {
  return Dialog(
    backgroundColor: Colors.transparent,
    child: Center(
      child: Container(
        padding: const EdgeInsets.all(30),
        decoration: BoxDecoration(
          color: Colors.white,
          borderRadius: BorderRadius.circular(16),
        ),
        child: const SizedBox(
          width: 40,
          height: 40,
          child: CircularProgressIndicator(
            strokeWidth: 4,
          ),
        ),
      ),
    ),
  );
}


    return Dialog(
      backgroundColor: Colors.transparent,
      child: Center(
        child: Container(
          width: 600,
          padding: const EdgeInsets.all(25),
          decoration: BoxDecoration(
            color: const Color(0xFFE4E4E4),
            borderRadius: BorderRadius.circular(18),
          ),
          child: SingleChildScrollView(
            child: Column(
              mainAxisSize: MainAxisSize.min,
              children: [
                // HEADER
                Row(
                  mainAxisAlignment: MainAxisAlignment.spaceBetween,
                  children: [
                    Text(
                      "Informacije o sobama",
                      style: GoogleFonts.openSans(
                        fontSize: 22,
                        fontWeight: FontWeight.bold,
                      ),
                    ),
                    GestureDetector(
                      onTap: () => Navigator.pop(context),
                      child: const Icon(Icons.close),
                    ),
                  ],
                ),

                const SizedBox(height: 20),

                // ADD ROW BUTTON
                Align(
                  alignment: Alignment.centerLeft,
                  child: ElevatedButton(
                    onPressed:
                        (widget.isReadOnly ||
                                rows.length >= availableRoomTypes.length)
                            ? null // 🔴 DISABLED (SIVO, NEKLIKABILNO)
                            : addRow, // 🟢 ENABLED (AKTIVNO)
                    style: ElevatedButton.styleFrom(
                      backgroundColor: const Color(0xFF64B5F6),
                      disabledBackgroundColor:
                          Colors.grey, // 🔥 siva boja kad je off
                      disabledForegroundColor:
                          Colors.white70, // da ne bude pretamno
                      padding: const EdgeInsets.symmetric(
                        vertical: 10,
                        horizontal: 18,
                      ),
                    ),
                    child: const Text(
                      "dodaj red  +",
                      style: TextStyle(color: Colors.white),
                    ),
                  ),
                ),

                const SizedBox(height: 15),

                // TABLE COLUMNS
                Row(
                  children: [
                    Expanded(
                      flex: 4,
                      child: Text(
                        "Tip sobe",
                        style: GoogleFonts.openSans(
                          fontSize: 18,
                          fontWeight: FontWeight.bold,
                        ),
                      ),
                    ),
                    Expanded(
                      flex: 3,
                      child: Text(
                        "Broj soba",
                        style: GoogleFonts.openSans(
                          fontSize: 18,
                          fontWeight: FontWeight.bold,
                        ),
                      ),
                    ),
                    Expanded(
                      flex: 2,
                      child: Center(
                        child: Text(
                          "Akcija",
                          style: GoogleFonts.openSans(
                            fontSize: 18,
                            fontWeight: FontWeight.bold,
                          ),
                        ),
                      ),
                    ),
                  ],
                ),

                const SizedBox(height: 10),

                // ROW LIST
                Column(
                  children: List.generate(rows.length, (index) {
                    final isNew = rows[index].isNew;
                    final unused = getUnusedTypes(index);

                    return Container(
                      margin: const EdgeInsets.symmetric(vertical: 6),
                      padding: const EdgeInsets.symmetric(
                        horizontal: 12,
                        vertical: 12,
                      ),
                      decoration: BoxDecoration(
                        color: const Color(0xFFD9D9D9),
                        borderRadius: BorderRadius.circular(14),
                      ),
                      child: Column(
                        crossAxisAlignment: CrossAxisAlignment.start,
                        children: [
                          Row(
                            children: [
                              // DROPDOWN
                              Expanded(
                                flex: 4,
                                child: AbsorbPointer(
                                  absorbing: widget.isReadOnly || !isNew,
                                  child: DropdownButtonFormField<int>(
                                    value:
                                        rows[index].roomId == 0
                                            ? null
                                            : rows[index].roomId,
                                    items:
                                        unused
                                            .map(
                                              (room) => DropdownMenuItem(
                                                value: room.id,
                                                child: Text(room.roomType),
                                              ),
                                            )
                                            .toList(),
                                    onChanged:
                                        widget.isReadOnly
                                            ? null
                                            : (val) {
                                              setState(() {
                                                rows[index].roomId = val!;
                                                rows[index].roomTypeError =
                                                    null;
                                              });
                                            },
                                    decoration: InputDecoration(
                                      filled: true,
                                      fillColor: Colors.white,
                                      border: OutlineInputBorder(
                                        borderRadius: BorderRadius.circular(10),
                                      ),
                                    ),
                                  ),
                                ),
                              ),

                              const SizedBox(width: 10),

                              // NUMBER
                              Expanded(
                                flex: 3,
                                child: TextField(
                                  controller: countControllers[index],
                                  enabled: !widget.isReadOnly,
                                  keyboardType: TextInputType.number,
                                  onChanged: (v) {
                                    rows[index].roomsLeft =
                                        int.tryParse(v) ?? 0;
                                    rows[index].roomCountError = null;
                                  },
                                  decoration: InputDecoration(
                                    filled: true,
                                    fillColor: Colors.white,
                                    border: OutlineInputBorder(
                                      borderRadius: BorderRadius.circular(10),
                                    ),
                                  ),
                                ),
                              ),

                              const SizedBox(width: 10),

                              // DELETE
                              Expanded(
                                flex: 2,
                                child: Center(
                                  child: IconButton(
                                    onPressed:
                                        widget.isReadOnly
                                            ? null
                                            : (isNew
                                                ? () => removeRow(index)
                                                : () => deleteRow(index)),
                                    icon: Icon(
                                      Icons.close,
                                      color:
                                          widget.isReadOnly
                                              ? Colors.grey
                                              : Colors.red,
                                    ),
                                  ),
                                ),
                              ),
                            ],
                          ),

                          // 🔥 ERRORS IN ONE ROW
                          if (rows[index].roomTypeError != null ||
                              rows[index].roomCountError != null)
                            Padding(
                              padding: const EdgeInsets.only(top: 6),
                              child: Row(
                                children: [
                                  // TYPE ERROR
                                  Expanded(
                                    flex: 4,
                                    child:
                                        rows[index].roomTypeError == null
                                            ? const SizedBox()
                                            : Text(
                                              rows[index].roomTypeError!,
                                              style: const TextStyle(
                                                color: Colors.red,
                                                fontSize: 13,
                                              ),
                                            ),
                                  ),

                                  const SizedBox(width: 10),

                                  // COUNT ERROR
                                  Expanded(
                                    flex: 3,
                                    child:
                                        rows[index].roomCountError == null
                                            ? const SizedBox()
                                            : Text(
                                              rows[index].roomCountError!,
                                              style: const TextStyle(
                                                color: Colors.red,
                                                fontSize: 13,
                                              ),
                                            ),
                                  ),

                                  const Expanded(flex: 2, child: SizedBox()),
                                ],
                              ),
                            ),
                        ],
                      ),
                    );
                  }),
                ),

                const SizedBox(height: 25),

                // SAVE BUTTON
                SizedBox(
                  width: 160,
                  child: ElevatedButton(
                    onPressed:
                        widget.isReadOnly
                            ? null
                            : () {
                              bool hasError = false;

                              // RESET ERRORS
                              for (var r in rows) {
                                r.roomTypeError = null;
                                r.roomCountError = null;
                              }

                              // VALIDATE TYPE
                              for (var r in rows) {
                                if (r.roomId == 0) {
                                  r.roomTypeError = "Odaberite tip sobe.";
                                  hasError = true;
                                }
                              }

                              // VALIDATE COUNT
                              for (var r in rows) {
                                if (r.roomsLeft <= 0) {
                                  r.roomCountError =
                                      "Broj soba mora biti veći od 0.";
                                  hasError = true;
                                }
                              }

                              setState(() {});

                              if (hasError) return;

                              Navigator.pop(context, rows);
                            },
                    style: ElevatedButton.styleFrom(
                      backgroundColor: const Color(0xFF64B5F6),
                      padding: const EdgeInsets.symmetric(vertical: 12),
                    ),
                    child: const Text(
                      "Spasi",
                      style: TextStyle(color: Colors.white),
                    ),
                  ),
                ),
              ],
            ),
          ),
        ),
      ),
    );
  }
}
