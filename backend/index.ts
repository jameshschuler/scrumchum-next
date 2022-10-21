import express, { Express, Request, Response } from 'express';
import dotenv from 'dotenv';
import https from 'https';
import { Server } from 'socket.io';
import fs from 'fs';
import cors from 'cors';

dotenv.config();

const app: Express = express();

app.use(cors(
  {
    origin: "https://127.0.0.1:9001"
  }
));

const port = process.env.PORT;

const httpsOptions : https.ServerOptions = {
  key: fs.readFileSync('./security/cert.key'),
  cert: fs.readFileSync('./security/cert.pem')
}

const server = https.createServer(httpsOptions, app);

const io = new Server(server, {
  cors: {
    origin: "https://127.0.0.1:9001",
    methods: ["GET", "POST"]
  }
});

app.get('/', (req: Request, res: Response) => {
  res.json({ message: 'Welcome!' });
});

io.on('connection', (socket) => {
  console.log('a user connected');
});

io.on('disconnect', (socket) => {
  console.log('a user disconnected');
});

server.listen(port, () => {
  console.log(`⚡️[server]: Server is running at https://localhost:${port}`);
});