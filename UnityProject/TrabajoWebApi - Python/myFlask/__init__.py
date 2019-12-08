#https://codeburst.io/jwt-authorization-in-flask-c63c1acf4eeb
# Inicializa y configura el servicio API
from flask import Flask
from flask_cors import CORS
from flask_restful import Api
from flask_sqlalchemy import SQLAlchemy
from flask_jwt_extended import JWTManager

# inicialización
app = Flask(__name__)
CORS(app) # Evita errores con el cors de chrome
api = Api(app)

# configuración
app.config['SQLALCHEMY_DATABASE_URI'] = 'sqlite:///app.db'
app.config['SQLALCHEMY_TRACK_MODIFICATIONS'] = False
app.config['SECRET_KEY'] = 'some-secret-string'

db = SQLAlchemy(app)

@app.before_first_request
def create_tables():
    db.create_all()


app.config['JWT_SECRET_KEY'] = 'jwt-secret-string'
jwt = JWTManager(app)

# urls disponibles para los consumidores de esta API
from myFlask import views, resources

api.add_resource(resources.UserRegistration, '/registration')
api.add_resource(resources.UserLogin, '/login')
api.add_resource(resources.UserUpdate, '/update')
api.add_resource(resources.GetNewGames, '/games')
api.add_resource(resources.AllUsers, '/admin')