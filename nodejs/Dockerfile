FROM node:13.0.1-alpine as build

WORKDIR /app

COPY package.json yarn.lock ./

RUN yarn install --frozen-lockfile --production

COPY . .

CMD ["yarn", "start"]




