import apiai
import json
import re
CLIENT_ACCESS_TOKEN = '839b3d8872b54b29a20bec843c2115b0'


def send_message_to_api_ai(message):
    ai = apiai.ApiAI(CLIENT_ACCESS_TOKEN)

    request = ai.text_request()

    request.lang = 'en'  # optional, default value equal 'en'

    request.session_id = '1' #
    request.query = message

    response = request.getresponse()
    response_json = json.loads(response.read())
    #print(response_json)

    return response_json


def action_get_film_by_geners(result):
    action = 'film'
    parameters = result['parameters']
    film = parameters['film']
    fulfillment = result['fulfillment']
    respond = fulfillment['speech']
    return action, film, respond


def action_greetings_hello(result):
    action = 'hello'
    film = ''
    fulfillment = result['fulfillment']
    respond = fulfillment['speech']
    return action, film, respond


def action_greetings_bye(result):
    action = 'bye'
    film = ''
    fulfillment = result['fulfillment']
    respond = fulfillment['speech']
    return action, film, respond

def action_greetings_how_are_you(result):
    action = 'bye'
    film = ''
    fulfillment = result['fulfillment']
    respond = fulfillment['speech']
    return action, film, respond

def preprocessing_data(response_json):

    if response_json is not None:
        print(response_json)
        result = response_json['result']
        action = result['action']
        if action == 'getfilmbygeners':
            act, film, respond = action_get_film_by_geners(result)
        elif action == 'smalltalk.greetings.hello':
            act, film, respond = action_greetings_hello(result)
        elif action == 'smalltalk.greetings.bye':
            act, film, respond = action_greetings_bye(result)
        elif action == 'smalltalk.greetings.how_are_you':
            act, film, respond = action_greetings_how_are_you(result)
        else:
            film = ''
            respond = ''
            act = 'error'
        return act, film, respond


def find_respond(message):

    js = send_message_to_api_ai(message)
    action, film, speech = preprocessing_data(js)
    if film:
        film = film.lower()
        if re.search('film', film):
            film = film.replace(' film', '')
        elif re.search('movie', film):
            film = film.replace(' movie', '')

    return action, film, speech


# if __name__ == '__main__':
#     message = 'i want see comedy film'
#     js = main(message)
#     preprocessing_data(js)
