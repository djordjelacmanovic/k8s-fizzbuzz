var express = require('express');
var logger = require('morgan');

var healthzRouter = require('./routes/healthz');

var app = express();

app.use(logger('dev'));
app.use(express.json());

app.use('/healthz', healthzRouter);

module.exports = app;
