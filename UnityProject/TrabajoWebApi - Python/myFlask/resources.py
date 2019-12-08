from flask_restful import Resource, reqparse
from myFlask.models import UserModel
import json, LLaveAsimetrica
from flask_jwt_extended import (create_access_token, create_refresh_token,
                                jwt_required, jwt_refresh_token_required,
                                get_jwt_identity, get_raw_jwt)

parser = reqparse.RequestParser()
parser.add_argument('msg', help = 'This field cannot be blank', required = True)
privateKey = LLaveAsimetrica.RSAmyPrivateKey()

class UserRegistration(Resource):
    def post(self):
        msgdata = parser.parse_args()
        data = SecurityParser(msgdata['msg'])

        if UserModel.find_by_username(data['username']):
            return {"msg": LLaveAsimetrica.myEncryptExternalPublicKey(json.dumps({"error": "User already exist"}), data['modulus'], data['exponent'])}

        new_user = UserModel(
            username=data['username'],
            password=UserModel.generate_hash(data['password']),
            rank1="",
            level1="",
            rank2="",
            level2="",
            rank3="",
            level3=""
        )

        try:
            new_user.save_to_db()
            list = {"access_token": create_access_token(identity=data['username']),
                    "refresh_token": create_refresh_token(identity=data['username'])}

            return {"msg": LLaveAsimetrica.myEncryptExternalPublicKey(json.dumps(list), data['modulus'], data['exponent'])}
        except:
            return {"msg": LLaveAsimetrica.myEncryptExternalPublicKey(json.dumps({"error": "Something went wrong"}), data['modulus'], data['exponent'])}, 500


class UserLogin(Resource):
    def post(self):
        msgdata = parser.parse_args()
        data = SecurityParser(msgdata['msg'])
        # data = json.loads(msgdata)
        current_user = UserModel.find_by_username(data['username'])

        # existe el usuario
        if not current_user:
            return {"msg": LLaveAsimetrica.myEncryptExternalPublicKey(json.dumps({"error": "Bad credentials"}), data['modulus'], data['exponent'])}

        # su contraseña es correcta
        if UserModel.verify_hash(data['password'], current_user.password):
            list= {"access_token": create_access_token(identity=data['username']),
                   "myrank1": current_user.rank1, "mylevel1": current_user.level1,
                   "myrank2": current_user.rank2, "mylevel2": current_user.level2,
                   "myrank3": current_user.rank3, "mylevel3": current_user.level3}

            # paquete de partidas
            list.update(GetLevel(1))
            list.update(GetLevel(2))
            list.update(GetLevel(3))

            return {"msg": LLaveAsimetrica.myEncryptExternalPublicKey(json.dumps(list), data['modulus'], data['exponent'])}
        else:
            return {"msg": LLaveAsimetrica.myEncryptExternalPublicKey(json.dumps({"error": "Bad credentials"}), data['modulus'], data['exponent'])}

class UserUpdate(Resource):
    @jwt_required
    def post(self):
        msgdata = parser.parse_args()
        data = SecurityParser(msgdata['msg'])
        current_user = UserModel.find_by_username(data['username'])

        if not current_user:
            return {"msg": "", "error": "Bad credentials"}

        if UserModel.verify_hash(data['password'], current_user.password):
            UserModel.updateLevel_by_username(data['username'], data['level'], data['levelstring'], data['rank'])
            return {"msg": "Level updated"}
        else:
            return {"msg": "", "error": "Bad credentials"}

    # def put(self):
    #     msgdata = parser.parse_args()
    #     data = json.loads(msgdata['msg'])
    #
    #     UserModel.updateLevel_by_username(data['username'], data['level'], data['levelstring'], data['rank'])

class GetNewGames(Resource):
    @jwt_required
    def get(self):
        # paquete de partidas
        list = GetLevel(1)
        list.update(GetLevel(2))
        list.update(GetLevel(3))

        return {"msg": json.dumps(list)}

# class UserLogoutAccess(Resource):
#     def post(self):
#         return {'message': 'User logout'}
#
#
# class UserLogoutRefresh(Resource):
#     def post(self):
#         return {'message': 'User logout'}
#
#
# class TokenRefresh(Resource):
#     @jwt_refresh_token_required
#     def post(self):
#         current_user = get_jwt_identity()
#         access_token = create_access_token(identity=current_user)
#         return {'access_token': access_token}
#
#
class AllUsers(Resource):
    def get(self):
        return UserModel.return_all()

    def delete(self):
        return UserModel.delete_all()
#
#
# class SecretResource(Resource):
#     @jwt_required
#     def get(self):
#         return {
#             'answer': 42
#         }
#
# class Rank(Resource):
#     def post(self):
#         msgdata = parser.parse_args()
#         data = SecurityParser(msgdata['msg'])
#
#         return UserModel.getLevel_by_rank(data['rank1'], data['rank2'], data['rank3'])


# metodos
# traduce un mensaje con la llave privada del servidor
def SecurityParser(data):
    myjson = LLaveAsimetrica.myDecrypt(data, privateKey)
    return json.loads(myjson)

# proporciona los paquetes necesarios de una partida
def GetLevel(num):
    levels = ["C", "B", "A", "A+", "S", "S+"]
    if num == 1:
        for rank in levels:
            result = UserModel.getLevel_by_rank(rank1=rank)
    elif num == 2:
        for rank in levels:
            result = UserModel.getLevel_by_rank(rank2=rank)
    elif num == 3:
        for rank in levels:
            result = UserModel.getLevel_by_rank(rank3=rank)
    return {"level"+str(num): result}
