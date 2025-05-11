# Build stage
FROM node:18-alpine as build

WORKDIR /app/client
COPY webapp/package.json ./
COPY webapp/package-lock.json ./
RUN npm i fsevents@latest -f --save-optional
RUN npm ci --silent
COPY webapp/ ./
RUN npm run build


FROM nginx:stable-alpine

COPY --from=build /app/client/out /usr/share/nginx/html

RUN rm /etc/nginx/conf.d/default.conf -f -r
COPY ./nginx.conf /etc/nginx/conf.d


CMD ["nginx", "-g", "daemon off;"]
