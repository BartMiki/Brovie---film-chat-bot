from flask import Blueprint, request, json, jsonify
from app import change
from os import abort

notes = [
    {
        'id': 1,
        'title': u'JUST DO IT',
        'description': u'Test Description',
        'done': False
    },
    {
        'id': 2,
        'title': u'Unit tests',
        'description': u' ;_; ',
        'done': False
    }
]

api = Blueprint('api', __name__)


#   curl -i http://localhost:5000/todo/api/notes
@api.route('/todo/api/notes', methods=['GET'])
def get_notes():
    return jsonify({'tasks': notes})

#curl -i -H "Content-Type: application/json" -X POST -d '{"title":"Test POST method"}' http://localhost:5000/todo/api/notes
@api.route('/todo/api/notes', methods=['POST'])
def create_note():
    if not request.json or not 'title' in request.json:
        abort(400)
    note = {
        'id': notes[-1]['id'] + 1,
        'title': request.json['title'],
        'description': request.json.get('description', ""),
        'done': False
    }
    notes.append(note)
    return jsonify({'note': note}), 201

#curl -i -H "Content-Type: application/json" -X PUT -d '{"done":true}' http://localhost:5000/todo/api/notes/2
@api.route('/todo/api/notes/<int:note_id>', methods=['PUT'])
def update_task(note_id):
    note = [note for note in notes if note['id'] == note_id]
    note[0]['title'] = request.json.get('title', note[0]['title'])
    note[0]['description'] = request.json.get('description', note[0]['description'])
    note[0]['done'] = request.json.get('done', note[0]['done'])
    return jsonify({'task': note[0]})

#curl -i -H "Content-Type: application/json" -X DELETE http://localhost:5000/todo/api/notes/2
@api.route('/todo/api/notes/<int:note_id>', methods=['DELETE'])
def delete_task(note_id):
    note = [note for note in notes if note['id'] == note_id]
    if len(note) == 0:
        abort(404)
    notes.remove(note[0])
    return jsonify({'operation_output': True})


#----------------ACTUAL API-----------------------------------------

@api.route('/do', methods = ['POST'])
def do():
    """Processing JSON file. To check how it works: curl -X POST http://127.0.0.1:5000/do --data @nazwa_jsona.json"""
    data = request.get_json(force=True)
    js = json.dumps(data)
    print(js)
    new_js = change.respond(js)
    print(new_js)
    return  jsonify(new_js)


