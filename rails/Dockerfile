FROM ruby:2.6.5-alpine

WORKDIR /app

RUN apk update && apk upgrade && apk add --update --no-cache build-base curl-dev git zlib-dev tzdata

RUN gem install bundler

COPY Gemfile Gemfile.lock ./

RUN bundle install

COPY . .

CMD ["rails", "server", "-b", "0.0.0.0"]