from app import create_app

app = create_app()


@app.route('/')
def index():
    return app


if __name__ == '__main__':
    app.run()
