import 'package:flutter/material.dart';

class routingPars extends StatefulWidget {
  final String title;
  final List<Widget> children;

  const routingPars({
    super.key,
    required this.title,
    required this.children,
  });

  @override
  State<routingPars> createState() => _RoutingPartState();
}

class _RoutingPartState extends State<routingPars> {
  bool isExpanded = false;

  void _toggleExpansion() {
    setState(() {
      isExpanded = !isExpanded;
    });
  }

  @override
  Widget build(BuildContext context) {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        ListTile(
          title: Text(
            widget.title,
            style: const TextStyle(fontWeight: FontWeight.bold),
          ),
          trailing: Icon(
            isExpanded ? Icons.keyboard_arrow_up : Icons.keyboard_arrow_down,
          ),
          onTap: _toggleExpansion,
        ),
        if (isExpanded)
          ...widget.children.map(
            (child) => Column(
              children: [
                Padding(
                  padding: const EdgeInsets.only(left: 32.0),
                  child: ListTile(
                    title: child,
                    onTap: () {
                      // Klik na podstavku
                    },
                  ),
                ),
                const Divider(height: 1),
              ],
            ),
          ),
        const Divider(height: 1),
      ],
    );
  }
}
