import 'package:etravel_app/models/user_token.dart';
import 'package:etravel_app/providers/base_provider.dart';

class UserTokenProvider extends BaseProvider<UserToken> {
  UserTokenProvider() : super("UserToken");

  @override
  UserToken fromJson(dynamic json) {
    return UserToken.fromJson(json);
  }
}
