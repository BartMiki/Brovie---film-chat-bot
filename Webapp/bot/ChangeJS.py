from bot.Bot import Bot
from bot.api_ai import find_respond
import json


class Change(object):

    def __init__(self):
        self.Bot = Bot()

    def respond(self, jso):

        js = json.loads(jso)
        action, film, respond = find_respond(js['respond'])
        if action == 'film':
            result = self.Bot.choose_film_by_genres(film, respond)
        elif action == 'hello' or action == 'bye':
            result = self.Bot.greetings(respond)
        else:
            result = self.Bot.error()

        return result


# change = Change()
# jso = {
#
#   "respond": "Bye"
# }
# result = change.respond(jso)
# print(result)


