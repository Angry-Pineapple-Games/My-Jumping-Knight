from myFlask import app

@app.route('/')
def index():
    return {"msg": "Welcome to My Jumping Night"}
