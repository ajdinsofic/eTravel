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

  const RoomEditorPopup({
    super.key,
    required this.initialRooms,
    required this.hotelId,
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

    // PREUZMI POČETNE SOBE (copy lista)
    rows =
        widget.initialRooms
            .map(
              (r) => HotelRoomInsertRequest(
                hotelId: r.hotelId,
                roomId: r.roomId,
                roomsLeft: r.roomsLeft,
              ),
            )
            .toList();

    // kontroleri za roomsLeft
    countControllers =
        rows
            .map((r) => TextEditingController(text: r.roomsLeft.toString()))
            .toList();

    loadRoomTypes();
  }

  Future<void> deleteRow(int index) async {
    final room = rows[index];

    if (room.hotelId == null || room.roomId == 0) {
      removeRow(index);
      return;
    }

    try {
      await _hotelRoomProvider.deleteRoom(room.hotelId!, room.roomId);

      setState(() {
        rows.removeAt(index);
        countControllers.removeAt(index);
      });
    } catch (e) {
      ScaffoldMessenger.of(
        context,
      ).showSnackBar(SnackBar(content: Text("Greška pri brisanju sobe: $e")));
    }
  }

  Future<void> loadRoomTypes() async {
    try {
      final fetched = await _roomProvider.get();

      setState(() {
        availableRoomTypes = fetched.items;
        isLoading = false;
      });
    } catch (e) {
      debugPrint("❌ Greška pri učitavanju room type: $e");
      setState(() => isLoading = false);
    }
  }

  List<Room> getUnusedTypes(int index) {
    final usedIds =
        rows
            .where((r) => r.roomId != 0 && r.roomId != rows[index].roomId)
            .map((r) => r.roomId)
            .toList();

    return availableRoomTypes
        .where((room) => !usedIds.contains(room.id))
        .toList();
  }

  void addRow() {
    if (rows.length >= availableRoomTypes.length) {
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(content: Text("Nema više dostupnih tipova soba.")),
      );
      return;
    }

    setState(() {
      rows.add(
        HotelRoomInsertRequest(
          hotelId: widget.hotelId,
          roomId: 0,
          roomsLeft: 0,
          isNew: true,
        ),
      );

      countControllers.add(TextEditingController(text: "0"));
    });
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
            padding: const EdgeInsets.all(40),
            decoration: BoxDecoration(
              color: Colors.white,
              borderRadius: BorderRadius.circular(14),
            ),
            child: const CircularProgressIndicator(),
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
            borderRadius: BorderRadius.circular(20),
          ),
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

              Align(
                alignment: Alignment.centerLeft,
                child: ElevatedButton(
                  onPressed: addRow,
                  style: ElevatedButton.styleFrom(
                    backgroundColor: const Color(0xFF64B5F6),
                    padding: const EdgeInsets.symmetric(
                      vertical: 10,
                      horizontal: 18,
                    ),
                    shape: RoundedRectangleBorder(
                      borderRadius: BorderRadius.circular(10),
                    ),
                  ),
                  child: const Text(
                    "dodaj red  +",
                    style: TextStyle(color: Colors.white),
                  ),
                ),
              ),

              const SizedBox(height: 15),

              // TABLE HEADER
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

              // ROWS
              Column(
                children: List.generate(rows.length, (index) {
                  final unused = getUnusedTypes(index);
                  final isNew = rows[index].isNew;

                  return Container(
                    margin: const EdgeInsets.symmetric(vertical: 6),
                    padding: const EdgeInsets.symmetric(
                      horizontal: 12,
                      vertical: 10,
                    ),
                    decoration: BoxDecoration(
                      color: const Color(0xFFD9D9D9),
                      borderRadius: BorderRadius.circular(14),
                    ),
                    child: Row(
                      children: [
                        // ---------------------------------------
                        // ROOM TYPE (dropdown)
                        // ---------------------------------------
                        Expanded(
                          flex: 4,
                          child: AbsorbPointer(
                            absorbing: !isNew, // 🔥 disable ako nije new
                            child: DropdownButtonFormField<int>(
                              value:
                                  rows[index].roomId == 0
                                      ? null
                                      : rows[index].roomId,
                              items:
                                  unused.map((room) {
                                    return DropdownMenuItem(
                                      value: room.id,
                                      child: Text(
                                        room.roomType,
                                        style: TextStyle(
                                          color:
                                              isNew
                                                  ? Colors.black
                                                  : Colors.grey, // 🔥 posivi
                                        ),
                                      ),
                                    );
                                  }).toList(),
                              onChanged: (roomId) {
                                if (roomId == null) return;

                                final room = availableRoomTypes.firstWhere(
                                  (r) => r.id == roomId,
                                );

                                setState(() {
                                  rows[index] = HotelRoomInsertRequest(
                                    hotelId: rows[index].hotelId,
                                    roomId: room.id,
                                    roomsLeft: rows[index].roomsLeft,
                                    isNew: rows[index].isNew,
                                  );
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

                        // ---------------------------------------
                        // ROOMS LEFT (number input)
                        // ---------------------------------------
                        Expanded(
                          flex: 3,
                          child: TextField(
                            controller: countControllers[index],
                            keyboardType: TextInputType.number,
                            onChanged: (v) {
                              final newCount = int.tryParse(v) ?? 0;

                              setState(() {
                                rows[index] = HotelRoomInsertRequest(
                                  hotelId: rows[index].hotelId,
                                  roomId: rows[index].roomId,
                                  roomsLeft: newCount,
                                  isNew: rows[index].isNew,
                                );
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

                        const SizedBox(width: 10),

                        // ---------------------------------------
                        // DELETE BUTTON
                        // ---------------------------------------
                        Expanded(
                          flex: 2,
                          child: Center(
                            child: IconButton(
                              onPressed:
                                  isNew
                                      ? () => removeRow(index)
                                      : () =>
                                          deleteRow(index), // 🔥 disable za postojeće
                              icon: Icon(
                                Icons.close,
                                color: Colors.red, // 🔥 posivi
                                size: 26,
                              ),
                            ),
                          ),
                        ),
                      ],
                    ),
                  );
                }),
              ),

              const SizedBox(height: 25),

              // SAVE
              SizedBox(
                width: 160,
                child: ElevatedButton(
                  onPressed: () {
                    for (var r in rows) {
                      if (r.roomId == 0) {
                        ScaffoldMessenger.of(context).showSnackBar(
                          const SnackBar(content: Text("Odaberite tip sobe.")),
                        );
                        return;
                      }
                      if (r.roomsLeft <= 0) {
                        ScaffoldMessenger.of(context).showSnackBar(
                          const SnackBar(content: Text("Unesite broj soba.")),
                        );
                        return;
                      }
                    }

                    Navigator.pop(context, rows);
                  },
                  style: ElevatedButton.styleFrom(
                    backgroundColor: const Color(0xFF64B5F6),
                    padding: const EdgeInsets.symmetric(vertical: 12),
                    shape: RoundedRectangleBorder(
                      borderRadius: BorderRadius.circular(12),
                    ),
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
    );
  }
}
