const express = require('express');
const logger = require('morgan');
const cors = require('cors')
const healthzRouter = require('./routes/healthz');
const fizzbuzzRouter = require('./routes/fizzbuzz');

const app = express();

app.use(logger('dev'));
app.use(express.json());
app.use(cors());

app.use('/healthz', healthzRouter);
app.use('/api/v1/fizzbuzz', fizzbuzzRouter);

module.exports = app;
