import random
from bot.Movie_search import Movies
import json

class Bot(object):

    BAD_RESPONSE = [
        "I'm sorry :C I couldn't find anything like that",
        "Next time I will know, but not now",
        "Try something else"
    ]
    GOOD_RESPONSE = [
        "{} is a great film!",
        '{} would work!',
        '{} is one option, but i know others too :)'
    ]
    RESP_GREET = [
        "Good morning!",
        "Hola!",
        "Hi!",
        "Hey there!",
        "Hello!"
    ]
    RESP_AFFIRM = [
        "greet",
        "great choice",
        "correct"
    ]
    RESP_QUESTION = [
        "any preferences ?",
        "any details?"
    ]
    RESP_GOODBYE = [
        "bye",
        "good bye",
        "bye bye",
        "CU"
    ]
    INTENTS_QUESTION = [
        'when',
        'who',
        'where'
    ]
    FIRST_MESS = [
            'Hello, what type of film do you want to see ?',
            'Hi, what movies you want to search?',
            'Hi, how can i help you?'
        ]
    HELP_MESS = [
        "So you have to give me a genre's film which you want to see",
        "Write a genre's film"
    ]
    last_film = []
    all_film = []
    last_respond_search = {}


    def __init__(self):
        self.search = Movies()


    def first_message(self):
        n = random.randint(0,len(self.FIRST_MESS)-1)
        respond = {}
        respond['respond'] = self.FIRST_MESS[n]
        result_json = json.dumps(respond)
        return result_json


    def help_message(self):
        n = random.randint(0, len(self.HELP_MESS)-1)
        respond = {}
        respond['respond'] = self.HELP_MESS[n]
        result_json = json.dumps(respond)
        return result_json


    def film(self, result):
        respond = {}
        if len(result) > 0:
            movie = random.choice(tuple(result.items()))
            if len(movie) > 0:
                which_id = movie[0]
                if which_id not in self.last_film:
                    self.last_film.append(which_id)
                    img = self.search.get_img_movie_from_id(movie[0])
                    if img :
                        path = 'https://image.tmdb.org/t/p/w500/' + img
                        n = random.randint(0, len(self.GOOD_RESPONSE) - 1)
                        respond['respond'] = self.GOOD_RESPONSE[n].format(movie[1])
                        respond['path'] = path
                        result_json = json.dumps(respond)
                        return result_json
                else:
                    if len(self.last_film) != len(movie):
                        self.film(result)
                    else:
                        return self.bad_message()


    def choose_film_by_genres(self, v):
        self.last_respond_search['genres'] = v # add genres which  movies will be search
        result = self.search.serach_movies_by_genres(v) # dic with id movies and tittle
        if len(result) > 0:
            return self.film(result)


    def popular_film(self):
        result = self.search.get_popular_movies()
        respond = {}
        help = []
        for k,v in result.items():
            help.append(v)
        respond['respond'] = help
        result_json = json.dumps(respond)
        return result_json


    def new_search(self):
        if len(self.last_respond_search) > 0:
            gen = self.last_respond_search['genres']
            return self.choose_film_by_genres(gen)


    def hello_message(self):
        respond = {}
        respond['respond'] = random.choice(self.RESP_GREET)
        result_json = json.dumps(respond)
        return result_json


    def goodbye_message(self):
        respond = {}
        respond['respond'] = random.choice(self.RESP_GOODBYE)
        result_json = json.dumps(respond)
        return result_json


    def bad_message(self):
        respond = {}
        respond['respond'] = random.choice(self.BAD_RESPONSE)
        result_json = json.dumps(respond)
        return result_json


    def aff_message(self):
        respond = {}
        respond['respond'] = random.choice(self.RESP_AFFIRM)
        result_json = json.dumps(respond)
        return result_json
