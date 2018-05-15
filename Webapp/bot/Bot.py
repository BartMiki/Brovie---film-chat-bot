import random
from bot.Movie_search import Movies
import json


class Bot(object):
    BAD_RESPONSE = [
        "I'm sorry :C I couldn't find anything like that",
        "Next time I will know, but not now",
        "Try something else"
    ]

    all_film = []

    def __init__(self):
        self.search = Movies()

    def get_movie_and_img(self, result, resp):
        respond = {}
        movie = random.choice(tuple(result.items()))
        if len(movie) > 0:
            img = self.search.get_img_movie_from_id(movie[0])
            if img:
                path = 'https://image.tmdb.org/t/p/w500/' + img
                message = resp+': ' + movie[1]
                respond['respond'] = message
                respond['path'] = path
                result_json = json.dumps(respond)
                return result_json
            else:
                return self.bad_message()

    def choose_film_by_genres(self, geners, resp):
        result = self.search.serach_movies_by_genres(geners)  # dic with id movies and tittle
        if result:
            return self.get_movie_and_img(result, resp)
        else:
            return self.bad_message()

    def popular_film(self):
        result = self.search.get_popular_movies()
        respond = {}
        help = []
        for k, v in result.items():
            help.append(v)
        respond['respond'] = help
        result_json = json.dumps(respond)
        return result_json

    def greetings(self, resp):
        respond = {'respond': resp}
        result_json = json.dumps(respond)
        return result_json

    def error(self):
        resp = "Sorry, I don't understand you :("
        respond = {'respond': resp}
        result_json = json.dumps(respond)
        return result_json

