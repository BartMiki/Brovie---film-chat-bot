from bot.Rasa_nlu import Rasa_NLU
import json
from functools import wraps
import warnings


def ignore_warnings(f):
    @wraps(f)
    def inner(*args, **kwargs):
        with warnings.catch_warnings(record=True) as w:
            warnings.simplefilter("ignore")
            response = f(*args, **kwargs)
        return response
    return inner

class Change(object):

    @ignore_warnings
    def __init__(self):
        self.rasa_nlp = Rasa_NLU()


    @ignore_warnings
    def respond(self, jso):
        js = json.loads(jso)
        result = self.rasa_nlp.find_respond(js['respond'])
        return result
