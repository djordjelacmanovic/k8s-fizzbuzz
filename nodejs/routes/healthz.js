const express = require('express');
const router = express.Router();
const HealthController = require('../controllers/health-controller');

router.get('/', (req, res, next) => new HealthController().get(req, res, next));

module.exports = router;
