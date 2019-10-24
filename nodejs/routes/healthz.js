var express = require('express');
var router = express.Router();

/* GET home page. */
router.get('/', function(req, res, next) {
  return res.json( {
    redis_up: true,
    redis_ping_latency: 0,
    error: null
  });
});

module.exports = router;
