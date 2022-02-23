FROM node:latest as node
WORKDIR /app
COPY . .
RUN npm install
RUN npm run build --prod

FROM nginx:alpine
EXPOSE 80
EXPOSE 443
COPY --from=node /app/dist/Door2Door-Web/ /usr/share/nginx/html