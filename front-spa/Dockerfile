FROM node:20

WORKDIR /usr/src/app

COPY package.json package-lock.json ./

COPY ./ ./
RUN chmod -R 755 .
COPY .env.example .env

RUN npm install
RUN npm run build

EXPOSE 3000

CMD ["npm", "run", "dev", "--", "--host"]
