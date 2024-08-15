ARG FRONTEND_DIR=/var/www

FROM node:lts-alpine3.18 as builder

LABEL auther="Smit Shah"

ARG CONFIGURATION="test"

ARG FRONTEND_DIR=/var/www

ENV NODE_ENV=${CONFIGURATION}
ENV PATH=${FRONTEND_DIR}/node_modules/.bin:$PATH

WORKDIR ${FRONTEND_DIR}
COPY ./package*.json .
run npm install -g @angular/cli
run npm install
copy . .
# copy ./app/environments/environment.${NODE_ENV}.ts ./app/environments/environment.ts
run ng build --configuration ${NODE_ENV} --verbose

from nginx:latest

env PORT=3000
ARG FRONTEND_DIR=/var/www

copy --from=builder ${FRONTEND_DIR}/dist/frontend/browser/ /usr/share/nginx/html
copy ./nginx/nginx.conf /etc/nginx/conf.d/default.conf

EXPOSE ${PORT}

cmd nginx -g "daemon off;"
