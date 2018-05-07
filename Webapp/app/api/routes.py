from flask import Blueprint, request, json, jsonify
from app import change

api = Blueprint('api', __name__)

@api.route('/do', methods = ['POST'])
def do():
    """Processing JSON file. To check how it works: curl -X POST http://127.0.0.1:5000/do --data @nazwa_jsona.json"""
    data = request.get_json(force=True)
    js = json.dumps(data)
    #print(js)
    new_js = change.respond(js)
    #print(new_js)
    return jsonify(new_js)