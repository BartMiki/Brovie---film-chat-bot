

class Config:
    # needed for RegistrationForm and LoginForm -> protect against modyfing cookies, cross site request forgery attack
    SECRET_KEY = '55d102a68a11bdb91578610ca74640a3'
    SQLALCHEMY_DATABASE_URI = 'postgres://ddlotpgl:62z66ApgTiJXDhajQl7GhyAgaku933yo@horton.elephantsql.com:5432/ddlotpgl'
    MAIL_SERVER = 'smtp.gmail.com'
    MAIL_PORT = 587
    MAIL_USE_TLS = True
    MAIL_USE_SSL = False
    # REMEMBER TO CHANGE
    MAIL_USERNAME = 'brovie.filmchatbot@gmail.com'
    MAIL_PASSWORD = 'botpassword'