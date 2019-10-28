const express = require('express');
const logger = require('morgan');

const healthzRouter = require('./routes/healthz');
const fizzbuzzRouter = require('./routes/fizzbuzz');

const app = express();

app.use(logger('dev'));
app.use(express.json());

app.use('/healthz', healthzRouter);
app.use('/api/v1/fizzbuzz', fizzbuzzRouter);

module.exports = app;
