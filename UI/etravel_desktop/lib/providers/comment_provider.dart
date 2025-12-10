import 'package:etravel_desktop/models/comment.dart';
import 'package:etravel_desktop/providers/base_provider.dart';

class CommentProvider extends BaseProvider<Comment> {
  CommentProvider() : super("Comment");

  @override
  Comment fromJson(item) {
    return Comment.fromJson(item);
  }
}
