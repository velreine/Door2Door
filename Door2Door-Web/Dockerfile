FROM node:latest as node
WORKDIR /app
COPY . .
RUN npm install
RUN npm run build --prod

FROM nginx:alpine
EXPOSE 80
EXPOSE 443
COPY --from=node /app/dist/Door2Door-Web/ /usr/share/nginx/html
# Copy our NGINX configuration into the container.
COPY --from=node /app/docker/nginx.default.conf /etc/nginx/conf.d/default.conf
COPY --from=node /app/docker/frontend-cert.pem /etc/ssl/frontend-cert.pem
COPY --from=node /app/docker/frontend-key.pem /etc/ssl/frontend-key.pem


# When the container starts,
# replace the env.js with values from environment variables
CMD ["/bin/sh",  "-c",  "envsubst < /usr/share/nginx/html/assets/env.template.js > /usr/share/nginx/html/assets/env.js && exec nginx -g 'daemon off;'"]
