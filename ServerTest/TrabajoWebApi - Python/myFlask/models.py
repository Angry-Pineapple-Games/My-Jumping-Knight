from myFlask import db
from  sqlalchemy.sql.expression import func
import KEY, LLaveSimetrica

# gestiona los datos de la base de datos y proporciona los metodos necesarios
class UserModel(db.Model):
    __tablename__ = 'users'

    id = db.Column(db.Integer, primary_key=True)
    username = db.Column(db.String(30), unique=True, nullable=False)
    password = db.Column(db.String(120), nullable=False)
    rank1 = db.Column(db.String(2), nullable = False)
    level1 = db.Column(db.String(3500), nullable=False)
    rank2 = db.Column(db.String(2), nullable=False)
    level2 = db.Column(db.String(3500), nullable=False)
    rank3 = db.Column(db.String(2), nullable=False)
    level3 = db.Column(db.String(3500), nullable=False)


    def save_to_db(self):
        db.session.add(self)
        db.session.commit()

    @classmethod
    def find_by_username(cls, username):
        return cls.query.filter_by(username=username).first()

    @classmethod
    def updateLevel_by_username(cls, username, level, levelstring, rank):
        cls.query.filter_by(username=username).\
            update({"level"+level: levelstring, "rank"+level: rank}, synchronize_session='evaluate')
        db.session.commit()

    @classmethod
    def getLevel_by_rank(cls, rank1="", rank2="", rank3=""):
        result = ""
        if rank1 != "":
            for l in cls.query.filter_by(rank1=rank1).order_by(func.random()).limit(20).all():
                result += l.level1 + ";"
        elif rank2 != "":
            for l in cls.query.filter_by(rank2=rank2).order_by(func.random()).limit(20).all():
                result += l.level2 + ";"
        elif rank3 != "":
            for l in cls.query.filter_by(rank3=rank3).order_by(func.random()).limit(20).all():
                result += l.level3 + ";"
        # result_encrypted = LLaveAsimetrica.myEncryptExternalPublicKey(json.dumps(result), l.moduluskey, l.exponentkey)
        # return json.dumps({'msg': result_encrypted})
        return result

    @classmethod
    def return_all(cls):
        def to_json(x):
            return {
                'username': x.username,
                'password': x.password,
                'rank1': x.rank1,
                'level1': x.level1,
                'rank2': x.rank2,
                'level2': x.level2,
                'rank3': x.rank3,
                'level3': x.level3
            }

        return {'msg': list(map(lambda x: to_json(x), UserModel.query.all()))}

    @classmethod
    def delete_all(cls):
        try:
            num_rows_deleted = db.session.query(cls).delete()
            db.session.commit()
            return {'message': '{} row(s) deleted'.format(num_rows_deleted)}
        except:
            return {'message': 'Something went wrong'}

    @staticmethod
    def generate_hash(password):
        encrypt_password = LLaveSimetrica.encrypt(KEY.KEYSECRET, password, KEY.IV, KEY.BS)
        return str(encrypt_password)[2:len(str(encrypt_password))-1]

    @staticmethod
    def verify_hash(password, encrypt_password):
        return password == LLaveSimetrica.decrypt(KEY.KEYSECRET, encrypt_password, KEY.IV)
