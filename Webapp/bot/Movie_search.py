import tmdbsimple as tmdb

class Movies(object):

    GENRES = {}


    def __init__(self):
        self.key = '22b40f97df8bc00c134496381488c5d8'
        tmdb.API_KEY = self.key


    def add_genres(self):
        genres = tmdb.Genres()
        all = genres.movie_list()
        all_gen = []
        if len(all) > 0:
            all_gen = all['genres']
        id_list = []
        name_list = []

        if len(all_gen) > 0:
            for x in all_gen:
                id_list.append(x['id'])
                name_list.append(x['name'].lower())

            for x in range(0,len(id_list)):
                self.GENRES[id_list[x]] = name_list[x]


    def search_id_genres(self, genres):
        if len(self.GENRES) > 0:
            for k,v in self.GENRES.items():
                if v == genres:
                    return k


    def serach_movies_by_genres(self, genres):
        self.add_genres()
        ID = self.search_id_genres(genres)
        if ID != 0:
            search = tmdb.Genres(ID)
            response = search.movies()
            all_title = []
            all_id = []
            result = {}

            if len(response['results']) > 0:
                for x in response['results']:
                    all_title.append(x['original_title'])
                    all_id.append(x['id'])
                if len(all_title) > 0 and len(all_id) > 0:
                    for x in range(0,len(all_title)):
                        result[all_id[x]] = all_title[x]
                    return result


    def get_img_movie_from_id(self, ID):
        mov = tmdb.Movies(ID)
        result = mov.images()
        if len(result['backdrops']) > 0:
            dic = result['backdrops']
            return dic[0]['file_path']


    def get_popular_movies(self):
        mov = tmdb.Movies()
        popular = mov.popular()
        all_title = []
        all_id = []
        result = {}
        if len(popular['results']) > 0:
            for x in popular['results']:
                all_title.append(x['original_title'])
                all_id.append(x['id'])
            if len(all_title) > 0 and len(all_id) > 0:
                for x in range(0, len(all_title)):
                    result[all_id[x]] = all_title[x]

                return result

